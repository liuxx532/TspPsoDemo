namespace TspPsoDemo
{
    using System.Collections.Generic;

    using Utilities;

    public  class TspData
     
    {
        public TspData(LookUpTable<double> distanceLookup, IEnumerable<int> bestRoute)
        {
            DistanceLookup = distanceLookup;
            BestRoute = bestRoute;
            CityCount = DistanceLookup.RowCount;
        }
        //The distance look up table is arranged like a fixture list
        // city   1   2   3   4
        // 1      0   10  20  30
        // 2      10   0  15  40
        // 3      20  15  0   60 
        public LookUpTable<double> DistanceLookup { get; private set; }
        public IEnumerable<int> BestRoute { get; private set; }
        public int CityCount { get; private set; }
    }
}
