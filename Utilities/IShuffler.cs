namespace Utilities
{
    public interface IShuffler<in T>
    {
        /// <summary>
        /// Shuffles a List
        /// </summary>
        /// <param name="arr">The Array to be shuffled</param>
        void Shuffle(T[] arr);
    }
}
