using System;

namespace TspPsoDemo
{
    

    using Utilities;

    public class RouteManager
    {
        private readonly IRandomFactory randomFactory;
        private readonly IRouteUpdater routeUpdater;
        public RouteManager(IRandomFactory randomFactory, IRouteUpdater routeUpdater)
        {
            this.randomFactory = randomFactory;
            this.routeUpdater = routeUpdater;
        }

        public int[] AddSections(IRoute[] sections)
        {
            if (sections == null || sections.Length == 0)
            {
                throw new ArgumentException("Array cannot be null or empty", "sections");
            }
            if (!routeUpdater.Isinitialised)
            {
                routeUpdater.Initialise(sections[0].DestinationIndex.Length);
            }
            foreach (var routeSection in sections)
            {
                routeUpdater.AddSection(this.randomFactory.NextRandom(0, routeSection.DestinationIndex.Length), routeSection, randomFactory.NextBool());
            }
            return routeUpdater.FinalizeDestinationIndex(sections[0]);

        }
        //updates a particle's velocity. The shorter the total distance the greater the velocity
        public double UpdateVelocity(IRoute particleItinery, double weighting, double randomDouble)
        {
            return (1 / particleItinery.TourDistance) * randomDouble * weighting;
        }

        //Selects a section of the route with a length  proportional to the particle's
        // relative velocity.
        public int GetSectionSize(IRoute particleItinery, double segmentVelocity, double totalVelocity)
        {
            int length = particleItinery.DestinationIndex.Length;
            return Convert.ToInt32(Math.Floor((segmentVelocity / totalVelocity) * length));

        }
    }
}
