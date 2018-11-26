namespace Utilities
{
    using System;

   

    public    class RandomFactory : IRandomFactory
    {
    private readonly Random random= new Random();
    public double NextRandomDouble()
    {
        return this.random.NextDouble();
    }

        public int NextRandom(int min, int max)
        {
            return this.random.Next(min, max);
          //  return Convert.ToInt32(this.random.NextDouble() * (max - min) + min);
        }

        public bool NextBool()
        {
            return (this.random.Next()%2==0);
            //  return Convert.ToInt32(this.random.NextDouble() * (max - min) + min);
        }
    }
}
