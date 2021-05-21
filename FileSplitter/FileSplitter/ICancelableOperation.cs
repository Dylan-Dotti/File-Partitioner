
namespace FileSplitter
{
    interface ICancelableOperation
    {
        bool Canceled { get; }
        void Cancel();
    }
}
