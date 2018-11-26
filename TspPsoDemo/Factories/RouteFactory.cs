namespace TspPsoDemo
{
    using Utilities;

    public class RouteFactory : IRouteFactory
   {
       public IRoute GetRoute(LookUpTable<double> distanceTable, int[] destinationIndex)
       {
           return new Route(distanceTable, destinationIndex);
       }
    }
}
