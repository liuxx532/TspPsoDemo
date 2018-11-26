namespace TspPsoDemo
{
    using System.Collections.Generic;

   

    using Utilities;

    public class SwarmOptimizer
    {
        #region Constants and Fields

        public IRoute BestGlobalItinery;

        private readonly IConsoleWriter consoleWriter;

        private readonly ITspParticleFactory particleFactory;

        private readonly IRouteFactory routeFactory;

        private readonly IShuffler<int> shuffler;

        #endregion

        #region Constructors and Destructors

        public SwarmOptimizer(
            IConsoleWriter consoleWriter,
            IShuffler<int> shuffler,
            IRouteFactory routeFactory,
            ITspParticleFactory particleFactory)
        {
            this.consoleWriter = consoleWriter;
            this.shuffler = shuffler;
            this.routeFactory = routeFactory;
            this.particleFactory = particleFactory;
        }

        #endregion

        #region Public Methods and Operators

        public void AddInformersToParticle(List<ITspParticle> informers)
        {
            foreach (ITspParticle tspParticle in informers)
            {
                tspParticle.InformersList.Clear();
                tspParticle.InformersList.AddRange(informers);
                tspParticle.InformersList.Remove(tspParticle);
            }
        }

        public ITspParticle[] BuildSwarm(PsoAttributes psoAttributes, TspData tspData)
        {
            var swarm = new ITspParticle[psoAttributes.SwarmSize];
            for (int i = 0; i < psoAttributes.SwarmSize; ++i)
            {
                swarm[i] = this.particleFactory.GetTspParticle(this.GetNewRoute(tspData));
            }
            int[] particleIndex = this.InitArray(psoAttributes.SwarmSize);
            this.UpdateInformers(swarm, particleIndex, psoAttributes.MaxInformers);
            return swarm;
        }

        public IRoute GetNewRoute(TspData tspData)
        {
            int numberOfDestinations = tspData.CityCount;
            int[] destinationIndex = this.InitArray(numberOfDestinations);
            this.shuffler.Shuffle(destinationIndex);
            IRoute route = this.routeFactory.GetRoute(tspData.DistanceLookup, destinationIndex);
            return route;
        }

        public int Optimize(ITspParticle[] swarm, PsoAttributes psoAttributes, bool isPrintBestDistance = true)
        {
            this.BestGlobalItinery = swarm[0].CurrentRoute.Clone();
            int[] particleIndex = this.InitArray(psoAttributes.SwarmSize);
            int epoch = 0;
            int staticEpochs = 0;
            while (epoch < psoAttributes.MaxEpochs)
            {
                bool isDistanceImproved = false;
                foreach (ITspParticle particle in swarm)
                {
                    double distance = particle.Optimize(psoAttributes);
                    if (distance < this.BestGlobalItinery.TourDistance)
                    {
                        particle.CurrentRoute.CopyTo(this.BestGlobalItinery);
                        isDistanceImproved = true;
                        if (isPrintBestDistance)
                        {
                            this.consoleWriter.WriteDistance(distance);
                        }
                    }
                }
                if (!isDistanceImproved)
                {
                    staticEpochs++;
                    if (staticEpochs == psoAttributes.MaxStaticEpochs)
                    {
                        this.UpdateInformers(swarm, particleIndex, psoAttributes.MaxInformers);
                        staticEpochs = 0;
                    }
                }
                epoch++;
            }
            return (int)this.BestGlobalItinery.TourDistance;
        }

        public void UpdateInformers(ITspParticle[] swarm, int[] particleIndex, int maxInformers)
        {
            this.shuffler.Shuffle(particleIndex);
            var informers = new List<ITspParticle>();
            int informersCount = maxInformers + 1;
            for (int i = 1; i < particleIndex.Length + 1; i++)
            {
                informers.Add(swarm[particleIndex[i - 1]]);
                if (i % informersCount == 0)
                {
                    this.AddInformersToParticle(informers);
                    informers.Clear();
                }
            }
            //the number of informers added here
            //will be less than the informer count
            this.AddInformersToParticle(informers);
        }

        #endregion

        #region Methods

        private int[] InitArray(int size)
        {
            var arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = i;
            }
            return arr;
        }

        #endregion
    }
}