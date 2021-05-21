using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSplitter
{
    public class PartitionExecutor : ICancelableOperation
    {
        public event Action<string, float?> StatusChanged;

        private readonly string srcPath;
        private readonly string dstDir;
        private readonly PartitionStrategy strategy;
        private readonly string splitDirBaseName;
        private ICancelableOperation currentOperation;

        public bool Canceled { get; private set; }

        public PartitionExecutor(string srcPath, string dstDir,
            PartitionStrategy strategy)
        {
            this.srcPath = srcPath;
            this.dstDir = dstDir;
            this.strategy = strategy;
            if (FileUtils.ZipArchiveExists(srcPath))
            {
                splitDirBaseName = FileUtils.GetFileName(srcPath, false);
            }
            else if (FileUtils.DirectoryExists(srcPath))
            {
                splitDirBaseName = FileUtils.GetDirectoryName(srcPath);
            }
            else
            {
                throw new ArgumentException("Invalid src path format");
            }
        }

        public void Cancel()
        {
            Canceled = true;
            currentOperation?.Cancel();
        }

        public void Execute()
        {
            Canceled = false;
            OnStatusChanged("Initializing partition...");
            // Generate transitional directory

            if (Canceled) return;
            // Extract files to transitional directory
            string transDir = null;
            if (FileUtils.ZipArchiveExists(srcPath))
            {
                transDir = Path.Combine(dstDir, $"{splitDirBaseName}_trans");
                FileUtils.CreateDirectory(transDir, deleteExisting: true);
                OnStatusChanged("Extracting source files...");
                FileUtils.ExtractZipToDir(srcPath, transDir);
            }


            if (Canceled) return;
            // get partition from strategy implementation
            currentOperation = strategy;
            OnStatusChanged("Partitioning files...", 0);
            List<Partition> partitions = strategy.Partition(transDir ?? srcPath,
                (numDone, numTotal) => OnStatusChanged(
                    "Partitioning files...", numDone / (float)numTotal));
            currentOperation = null;
            if (Canceled) return;

            for (int i = 0; i < partitions.Count; i++)
            {
                Console.WriteLine($"Partition {i + 1}: {ByteUtils.ToMb(partitions[i].SizeInBytes)}Mb)");
            }

            // Generate zip file names
            List<string> dstPaths = Enumerable.Range(1, partitions.Count)
                .Select(i => Path.Combine(dstDir, $"{splitDirBaseName}_{i}.zip"))
                .ToList();
            for (int i = 0; i < partitions.Count; i++)
            {
                partitions[i].DstPath = dstPaths[i];
            }
            List<DotNetZipOperation> zipOperations = partitions
                .Select(p => FileUtils.CreateZipOperation(srcPath, p.DstPath, p.SrcFilePaths))
                .ToList();

            OnStatusChanged("Zipping destination directories...", 0);
            for (int i = 0; i < partitions.Count; i++)
            {
                if (Canceled) return;
                Partition partition = partitions[i];
                var zipOp = FileUtils.CreateZipOperation(srcPath,
                    partition.DstPath, partition.SrcFilePaths);
                currentOperation = zipOp;
                zipOp.Execute();
                OnStatusChanged("Zipping destination directories...",
                    (i + 1) / (float)partitions.Count);
            }

            if (transDir != null) FileUtils.DeleteDirectory(transDir);
        }

        private void OnStatusChanged(
            string newStatus, float? percentageProgress = null)
        {
            StatusChanged?.Invoke(newStatus, percentageProgress);
        }
    }
}
