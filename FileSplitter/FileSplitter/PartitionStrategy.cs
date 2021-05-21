using System;
using System.Collections.Generic;

namespace FileSplitter
{
    public abstract class PartitionStrategy : ICancelableOperation
    {
        public bool Canceled { get; private set; }

        public virtual void Cancel() => Canceled = true;

        public abstract List<Partition> Partition(string srcDir,
            Action<int, int> PartitionProgressChanged = null);
    }
}
