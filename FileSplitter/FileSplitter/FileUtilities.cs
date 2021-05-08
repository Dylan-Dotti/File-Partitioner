using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSplitter
{
    static class FileUtilities
    {
        public static bool IsFile(string path) => File.Exists(path);
        public static bool IsDirectory(string path) => Directory.Exists(path);

        public static long GetFileSize(string path) => File.ReadAllBytes(path).LongLength;

        public static void MoveFile(string srcPath, string dstPath) => File.Move(srcPath, dstPath);

        public static void CreateDirectory(string path) => Directory.CreateDirectory(path);
    }
}
