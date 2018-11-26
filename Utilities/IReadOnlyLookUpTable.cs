namespace Utilities
{
    public interface ILookUpTable<out T>
    {
        T this[int r, int c] { get; }
        int RowCount { get; }
        int ColCount { get; }
    }
}
