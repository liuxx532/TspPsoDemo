namespace Utilities
{
  
    public class Shuffler<T> : IShuffler<T>
    {
       private readonly IRandomFactory randomFactory;
       public Shuffler(IRandomFactory randomFactory)
       {
           this.randomFactory = randomFactory;
       }
    
       //Fisher-Yates shuffle method 
       public void Shuffle(T[] arr)
       {

           int i = arr.Length - 1;
           while (i > 0)
           {
               int k = this.randomFactory.NextRandom(0, i + 1);
               T temp = arr[i];
               arr[i] = arr[k];
               arr[k] = temp;
               i--;
           }


       }
    }
}
