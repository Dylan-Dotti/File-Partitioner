using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace FileSplitter
{
    public static class FileUtilities
    {
        public static bool IsFile(string path) => File.Exists(path);
        public static bool IsDirectory(string path) => Directory.Exists(path);

        public static byte[] GetFileBytes(string path) => File.ReadAllBytes(path);

        public static long GetFileSize(string path) => GetFileBytes(path).LongLength;

        public static long GetDirectorySize(string path)
        {
            if (!IsDirectory(path)) throw new ArgumentException("Path is not a directory");
            long totalSize = 0;
            foreach (string filePath in Directory.GetFiles(path))
            {
                totalSize += GetFileSize(filePath);
            }
            foreach (string dirPath in Directory.GetDirectories(path))
            {
                totalSize += GetDirectorySize(dirPath);
            }
            return totalSize;
        }

        public static void MoveFile(string srcPath, string dstPath) => File.Move(srcPath, dstPath);

        public static void CreateDirectory(string path) => Directory.CreateDirectory(path);

        public static ZipArchive CreateZipArchive(string path) => ZipFile.Open(path, ZipArchiveMode.Create);

        public static ZipArchive ZipFiles(string archivePath, IEnumerable<string> filePaths,
            Action<ZipArchiveEntry> OnEntryCreated = null)
        {
            using (ZipArchive archive = CreateZipArchive(archivePath))
            {
                foreach (string path in filePaths)
                {
                    var entry = archive.CreateEntryFromFile(path, Path.GetFileName(path));
                    OnEntryCreated?.Invoke(entry);
                }
                return archive;
            }
        }
    }
}
