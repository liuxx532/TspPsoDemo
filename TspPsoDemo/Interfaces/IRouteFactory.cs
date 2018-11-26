namespace TspPsoDemo
{
    using Utilities;

    public interface IRouteFactory
    {
        IRoute GetRoute(LookUpTable<double> distanceTable, int[] destinationIndex);
    }
}