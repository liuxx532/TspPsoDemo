namespace TspPsoDemo
{
    using System.Collections.Generic;

   

    public interface ITspManager
    {
        int CityCount { get; }

        int MaxInformers { get; }

        PsoAttributes PsoAttributes { get; }

        int SwarmSize { get; }

        double CalculateTotalDistance(int[] route);

        double GetBestPossibleDistance();

        IEnumerable<int> GetBestPossibleRoute();

        void SetRouteDistanceTable(IRoute route);
    }
}