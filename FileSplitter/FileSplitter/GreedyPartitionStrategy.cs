using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSplitter
{
    // Splits the contents of srcDir into N directories at dstDir
    public sealed class GreedyPartitionStrategy : PartitionStrategy
    {
        private readonly int nPartitions;

        public GreedyPartitionStrategy(int nPartitions)
        {
            this.nPartitions = nPartitions;
        }

        public override List<Partition> Partition(string srcDir,
            Action<int, int> PartitionProgressChanged = null)
        {
            Dictionary<string, long> srcPathSizeMap = FileUtils.GetPathSizeMap(srcDir);
            var partitions = new List<Partition>(nPartitions);
            for (int i = 0; i < nPartitions; i++) partitions.Add(new Partition());
            int totalFileCount = srcPathSizeMap.Count;
            //OnStatusChanged("Partitioning files...", 0);
            PartitionProgressChanged?.Invoke(0, srcPathSizeMap.Count);
            while (srcPathSizeMap.Count > 0)
            {
                if (Canceled) return null;
                // Get smallest partition
                Partition smallestPartition = partitions
                    .MinBy(p => p.SizeInBytes).First();
                // Get largest source file
                string largestSrcPath = srcPathSizeMap.GetMaxKeyByValue();
                // Add largest source file
                smallestPartition.AddSourceFile(largestSrcPath);
                srcPathSizeMap.Remove(largestSrcPath);
                PartitionProgressChanged?.Invoke(
                    totalFileCount - srcPathSizeMap.Count, totalFileCount);
                //OnStatusChanged("Partitioning files...",
                //    (totalFileCount - srcPathSizeMap.Count) / (float)totalFileCount);
            }
            return partitions;
        }
    }
}
