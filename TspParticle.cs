namespace TspPsoDemo
{
    using System.Collections.Generic;

   

    public class TspParticle : ITspParticle
    {
        #region Constants and Fields

        private readonly TspParticleOptimizer tspParticleOptimiser;

        #endregion

        #region Constructors and Destructors

        public TspParticle(IRoute route, TspParticleOptimizer tspParticleOptimizer)
        {
            this.CurrentRoute = route;
            this.tspParticleOptimiser = tspParticleOptimizer;
            this.PersonalBestRoute = this.CurrentRoute.Clone();
            this.InformersList = new List<ITspParticle>();
        }

        #endregion

        #region Public Properties

        public IRoute CurrentRoute { get; set; }

        public List<ITspParticle> InformersList { get; set; }

        public IRoute LocalBestRoute { get; set; }

        public IRoute PersonalBestRoute { get; set; }

        #endregion

        #region Public Methods and Operators

        public double Optimize(PsoAttributes psoAttributes)
        {
            this.LocalBestRoute = this.GetLocalBestRoute();
            this.CurrentRoute.DestinationIndex =
                this.tspParticleOptimiser.GetOptimizedDestinationIndex(
                    this.CurrentRoute,
                    this.PersonalBestRoute,
                    this.LocalBestRoute,
                    psoAttributes);

            double currentDistance = this.CurrentRoute.CalculateTotalDistance();
            if (currentDistance < this.PersonalBestRoute.CalculateTotalDistance())
            {
                this.CurrentRoute.DestinationIndex.CopyTo(this.PersonalBestRoute.DestinationIndex, 0);
            }
            return currentDistance;
        }

        #endregion

        #region Methods

        private IRoute GetLocalBestRoute()
        {
            IRoute localBestRoute = this.PersonalBestRoute;
            foreach (ITspParticle particle in this.InformersList)
            {
                if (localBestRoute.TourDistance > particle.PersonalBestRoute.TourDistance)
                {
                    localBestRoute = particle.PersonalBestRoute;
                }
            }
            return localBestRoute;
        }

        #endregion

    }
}