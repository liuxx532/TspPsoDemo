namespace TspPsoDemo
{
    using Utilities;

    public class LookUpTableFactory
    {
        public LookUpTable<T> Create<T>(T[,] arr)
        {
            return new LookUpTable<T>(arr);
        }
    }
}
