namespace TspPsoDemo
{
  

    using Utilities;

    public class TspParticleOptimizer : IParticleOptimizer
    {
        #region Constants and Fields


       // private readonly RouteBuilderFactory routeBuilderFactory;

        private readonly IRandomFactory randomFactory;

    //    private readonly RouteSectionBuilder routeSectionBuilder;
        private readonly RouteManager routeManager;
        #endregion

        #region Constructors and Destructors

     

        public TspParticleOptimizer(
            IRandomFactory randomFactory,
            RouteManager routeManager)
        {
            this.randomFactory = randomFactory;
            this.routeManager = routeManager;
        }



        #endregion

      

        #region Public Methods and Operators
 
        public int[] GetOptimizedDestinationIndex(
          IRoute currRoute,
          IRoute pBRoute,
          IRoute lBRoute,
          PsoAttributes psoAttribs)
        {
            //update all the velocities using the appropriate PSO constants
            double currV = routeManager.UpdateVelocity(currRoute, psoAttribs.W, 1);
            double pBV = routeManager.UpdateVelocity(pBRoute, psoAttribs.C1, randomFactory.NextRandomDouble());
            double lBV = routeManager.UpdateVelocity(lBRoute, psoAttribs.C2, randomFactory.NextRandomDouble());
            double totalVelocity = currV + pBV + lBV;

            //update the Segment size for each Route
            currRoute.SegmentSize = routeManager.GetSectionSize(currRoute, currV, totalVelocity);
            pBRoute.SegmentSize = routeManager.GetSectionSize(pBRoute, pBV, totalVelocity);
            lBRoute.SegmentSize = routeManager.GetSectionSize(lBRoute, lBV, totalVelocity);
            return routeManager.AddSections(new[] { lBRoute, pBRoute, currRoute });
        }

      

        #endregion

      
    }
}