
namespace FilePartitioner
{
    interface ICancelableOperation
    {
        bool Canceled { get; }
        void Cancel();
    }
}
