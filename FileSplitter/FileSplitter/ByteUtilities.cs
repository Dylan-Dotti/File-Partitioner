using System;

namespace FileSplitter
{
    public static class ByteUtilities
    {
        public static double ToKb(long byteCount)
        {
            return byteCount / 1024;
        }

        public static double ToMb(long byteCount)
        {
            return byteCount / Math.Pow(1024, 2);
        }

        public static double ToGb(long byteCount)
        {
            return byteCount / Math.Pow(1024, 3);
        }
    }
}
