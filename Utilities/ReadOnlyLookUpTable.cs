namespace Utilities
{
   

    public class LookUpTable<T> : ILookUpTable<T>
    {
        public LookUpTable(T[,] arr)
        {
            this.arr = arr;
        }

        private readonly T[,] arr;

        // The indexer allows the use of [,] operator.
        public T this[int r, int c]
        {
            get
            {
                return arr[r, c];
            }
        }

        public int RowCount
        {
            get
            {
                return arr.GetLength(0);
            }
        }
        public int ColCount
        {
            get
            {
                return arr.GetLength(1);
            }
        }
    }

  

   
}
