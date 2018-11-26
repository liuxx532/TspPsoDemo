namespace TspPsoDemo
{
    using System.Linq;

    public class SwarmManager
    {
        #region Constants and Fields

        private readonly SwarmOptimizer swarmOptimizer;

        #endregion

        #region Constructors and Destructors

        public SwarmManager(SwarmOptimizer swarmOptimizer)
        {
            this.swarmOptimizer = swarmOptimizer;
        }

        #endregion

        #region Public Methods and Operators

        public ITspParticle[] BuildSwarm(PsoAttributes psoAttributes, TspData tspData)
        {
            return this.swarmOptimizer.BuildSwarm(psoAttributes, tspData);
        }

        public int Optimize(ITspParticle[] swarm, PsoAttributes psoAttributes, bool isPrintBestDistance = true)
        {
            return this.swarmOptimizer.Optimize(swarm, psoAttributes, isPrintBestDistance);
        }
        public double GetBestPossibleDistance(TspData tspData)
        {
            return this.CalculateBestDistance(tspData);
        }

        public SwarmOptimizer GetSwarmOptimizer()
        {
            return this.swarmOptimizer;
        }

        public double CalculateBestDistance(TspData tspData)
        {
            int[] route = tspData.BestRoute.ToArray();
            double totalDistance = 0;
            for (int i = 0; i < route.Length - 1; i++)
            {
                totalDistance += tspData.DistanceLookup[route[i], route[i + 1]];
            }
            totalDistance += tspData.DistanceLookup[route[route.Length - 1], route[0]];
            return totalDistance;
        }

        public int[] GetBestPossibleRoute(TspData tspData)
        {
          return tspData.BestRoute.ToArray();
        }
        #endregion
    }
}