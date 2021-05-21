using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace FileSplitter
{
    public class ZipOperation : ICancelableOperation
    {
        public event Action<ZipArchiveEntry> OnEntryCreated;

        public string ZipPath { get; private set; }
        public IEnumerable<string> SrcPaths { get; private set; }
        public bool Canceled { get; private set; }

        public ZipOperation(string zipPath, IEnumerable<string> srcPaths,
            Action<ZipArchiveEntry> OnEntryCreated = null)
        {
            ZipPath = zipPath;
            SrcPaths = srcPaths;
            if (OnEntryCreated != null) this.OnEntryCreated += OnEntryCreated;
        }

        public void Execute()
        {
            Canceled = false;
            using (ZipArchive archive = FileUtils.OpenZipArchive(ZipPath))
            {
                foreach (string path in SrcPaths)
                {
                    if (Canceled) return;
                    var entry = archive.CreateEntryFromFile(
                        path, Path.GetFileName(path));
                    OnEntryCreated?.Invoke(entry);
                }
            }
        }

        public void Cancel() => Canceled = true;
    }
}
