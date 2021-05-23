using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace FilePartitioner
{
    public class DotNetZipOperation : ICancelableOperation
    {
        public event Action<ZipEntryCreatedEventArgs> OnEntryCreated;

        private List<string> srcPaths;

        public string SrcDir { get; private set; }
        public string ZipPath { get; private set; }
        public IEnumerable<string> SrcPaths => srcPaths;
        public bool Canceled { get; private set; }

        public DotNetZipOperation(string srcDir, string zipPath,
            IEnumerable<string> srcPaths, Action<ZipEntryCreatedEventArgs> OnEntryCreated = null)
        {
            SrcDir = srcDir;
            ZipPath = zipPath;
            this.srcPaths = srcPaths.ToList();
            if (OnEntryCreated != null) this.OnEntryCreated += OnEntryCreated;
        }

        public void Cancel()
        {
            Canceled = true;
        }

        public void Execute()
        {
            using (ZipFile zip = new ZipFile(ZipPath))
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                int totalEntries = SrcPaths.Count();
                int totalZipped = 0;
                ZipEntry entry = null;
                foreach (string path in SrcPaths)
                {
                    if (FileUtils.DirectoryExists(path))
                    {
                        string relativePath = FileUtils.GetRelativePath(SrcDir, path);
                        entry = zip.AddDirectory(path, relativePath);
                    }
                    else if (FileUtils.FileExists(path)) entry = zip.AddFile(path, "");
                    else throw new ArgumentException("Invalid path: " + path);
                    zip.Save();
                    totalZipped += 1;
                    OnEntryCreated?.Invoke(
                        new ZipEntryCreatedEventArgs(entry, totalZipped, totalEntries));
                }
            }
        }
    }

    public class ZipEntryCreatedEventArgs
    {
        public ZipEntry Entry { get; private set; }
        public int NumZipped { get; private set; }
        public int NumTotal { get; private set; }

        public ZipEntryCreatedEventArgs(ZipEntry entry, int numZipped, int numTotal)
        {
            Entry = entry;
            NumZipped = numZipped;
            NumTotal = numTotal;
        }
    }
}
