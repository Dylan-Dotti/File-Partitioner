using System;

namespace FilePartitioner
{
    public static class ByteUtils
    {
        public static double ToKb(long byteCount)
        {
            return byteCount / 1024d;
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
