using System.Collections.Generic;

namespace FileSplitter
{
    public class Partition
    {
        private readonly HashSet<string> sourceFilePaths;

        public IReadOnlyCollection<string> SrcFilePaths => sourceFilePaths;
        public string DstPath { get; set; }
        public long SizeInBytes { get; private set; }

        public Partition()
        {
            sourceFilePaths = new HashSet<string>();
        }

        public bool AddSourceFile(string path)
        {
            if (sourceFilePaths.Add(path))
            {
                if (FileUtils.FileExists(path)) SizeInBytes += FileUtils.GetFileSize(path);
                else if (FileUtils.DirectoryExists(path)) SizeInBytes += FileUtils.GetDirectorySize(path);
                return true;
            }
            return false;
        }
    }
}
