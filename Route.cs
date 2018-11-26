namespace TspPsoDemo
{
    using Utilities;

    public  class Route : IRoute
  {
      public int[] DestinationIndex { get; set; }

      private  LookUpTable<double> lookUpTable;
     

      public double TourDistance { get; set; }
      public Route(LookUpTable<double> distanceTable, int[] destinationIndex)
      {
          this.lookUpTable = distanceTable;
          this.DestinationIndex = destinationIndex;
          this.CalculateTotalDistance();
          SegmentSize = -1;
      }
     public void SetDistanceTable(LookUpTable<double> distTable)
      {
          this.lookUpTable = distTable;
      }
      public void CopyTo(IRoute targetItinery)
      {
          targetItinery.SetDistanceTable(this.lookUpTable);
          targetItinery.DestinationIndex = new int[this.DestinationIndex.Length];
          targetItinery.TourDistance = this.TourDistance;
          this.DestinationIndex.CopyTo(targetItinery.DestinationIndex, 0);
      }
      public Route Clone()
      {
          var destinationIndex = new int[this.DestinationIndex.Length];
          this.DestinationIndex.CopyTo(destinationIndex, 0);
          return new Route(this.lookUpTable, destinationIndex);
      }

      public double CalculateTotalDistance()
      {
          double total = 0;
          for (int i = 0; i < this.DestinationIndex.Length; i++)
          {
              if (i == this.DestinationIndex.Length - 1)
              {
                    //Don't forget to add in the distance between the
                    //last city in the array and the first city
                  total += this.lookUpTable[this.DestinationIndex[i], this.DestinationIndex[0]];
              }
              else
              {
                  total += this.lookUpTable[this.DestinationIndex[i], this.DestinationIndex[i + 1]];
              }
             
          }
          this.TourDistance = total;
          return total;
      }

        public int SegmentSize { get; set; }
  }
}
