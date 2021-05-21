using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace FileSplitter
{
    public static class FileUtils
    {
        public static bool FileExists(string path) => File.Exists(path);
        public static bool DirectoryExists(string path) => Directory.Exists(path);
        public static bool ZipArchiveExists(string path) => 
            FileExists(path) && Path.GetExtension(path).ToLower() == ".zip";

        public static string GetRelativePath(string relativeTo, string path)
        {
            var uri = new Uri(relativeTo);
            var rel = Uri.UnescapeDataString(uri.MakeRelativeUri(
                new Uri(path)).ToString()).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            if (rel.Contains(Path.DirectorySeparatorChar.ToString()) == false)
            {
                rel = $".{ Path.DirectorySeparatorChar }{ rel }";
            }
            return rel;
        }

        public static string GetFileName(string path, bool includeExtension)
        {
            return includeExtension ?
                Path.GetFileName(path) : Path.GetFileNameWithoutExtension(path);
        }

        public static string GetDirectoryName(string path)
        {
            if (!DirectoryExists(path)) throw new ArgumentException("Path is not a directory");
            return GetDirectoryInfo(path).Name;
        }

        public static FileInfo GetFileInfo(string path) => new FileInfo(path);
        public static DirectoryInfo GetDirectoryInfo(string path) => new DirectoryInfo(path);

        public static byte[] GetFileBytes(string path) => File.ReadAllBytes(path);

        public static long GetFileSize(string path) => GetFileBytes(path).LongLength;

        public static void MoveFile(string srcPath, string dstPath) => File.Move(srcPath, dstPath);

        public static long GetDirectorySize(string path)
        {
            if (!DirectoryExists(path)) throw new ArgumentException("Path is not a directory");
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

        public static Dictionary<string, long> GetPathSizeMap(string srcDir)
        {
            DirectoryInfo directory = new DirectoryInfo(srcDir);
            if (!directory.Exists) throw new ArgumentException("Path is not a directory");
            Dictionary<string, long> pathSizeMap = new Dictionary<string, long>();
            foreach (FileInfo file in directory.GetFiles())
            {
                pathSizeMap.Add(file.FullName, file.Length);
            }
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                pathSizeMap.Add(dir.FullName, GetDirectorySize(dir.FullName));
            }
            return pathSizeMap;
        }

        public static DirectoryInfo CreateDirectory(string path, bool deleteExisting = false)
        {
            if (deleteExisting && DirectoryExists(path)) DeleteDirectory(path);
            Directory.CreateDirectory(path);
            return new DirectoryInfo(path);
        }

        public static void DeleteDirectory(string path) => Directory.Delete(path, true);

        public static ZipArchive OpenZipArchive(string path) => ZipFile.Open(path, ZipArchiveMode.Create);

        public static DotNetZipOperation CreateZipOperation(
            string srcPath, string archivePath, IEnumerable<string> srcFilePaths,
            Action<ZipEntryCreatedEventArgs> OnEntryCreated = null)
        {
            return new DotNetZipOperation(srcPath, archivePath, srcFilePaths, OnEntryCreated);
        }

        public static void ZipFiles(
            string srcPath, string archivePath, IEnumerable<string> filePaths,
            Action<ZipEntryCreatedEventArgs> OnEntryCreated = null)
        {
            var operation = CreateZipOperation(
                srcPath, archivePath, filePaths, OnEntryCreated);
            operation.Execute();
        }

        public static void CreateZipFromDir(string srcPath, string archivePath)
        {
            ZipFile.CreateFromDirectory(srcPath, archivePath);
        }

        public static void ExtractZipToDir(string archivePath, string dstPath)
        {
            ZipFile.ExtractToDirectory(archivePath, dstPath);
        }
    }
}
