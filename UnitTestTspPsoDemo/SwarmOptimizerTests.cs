using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestTspPsoDemo
{
    using System.Diagnostics;
    using System.Linq;

    using NSubstitute;
    using TspPsoDemo;

    using Utilities;

   

    [TestClass]
    public class SwarmOptimizerTests
    {
     
        private ITspParticle[] swarm;
        private int[,] expectedLookup;
        //https://stackoverflow.com/questions/12446770/how-to-compare-multidimensional-arrays-in-c-sharp
        private bool CompareArrays<T>(Array firstArray, Array secondArray)
        {
            return firstArray.Rank == secondArray.Rank
                   && Enumerable.Range(0, firstArray.Rank)
                                 .All(d => firstArray.GetLength(d) == secondArray.GetLength(d))
                  && firstArray.Cast<T>().SequenceEqual(secondArray.Cast<T>());
        }
        [TestInitialize]
        public void TestInitialise()
        {
           this.expectedLookup = new[, ] { { 1, 2 }, { 0, 2 }, { 0, 1 }, { 4, 5 }, { 3, 5 }, { 3, 4 }, { 7, 0 }, { 6, 0 } };
            var particles = new ITspParticle[8];
            for (int i = 0; i < 8; i++)
            {
                particles[i] = new TspParticleStub(i);
            }
            this.swarm = particles;
        }

      

        [TestMethod]
        public void SwarmOptimierUpdateInformersSetsParticleInformersListToCorrectCount()
        {
            int[] index = { 0, 1, 2, 3, 4, 5, 6, 7 };
            var shuffler = Substitute.For<IShuffler<int>>();
            var routeFactory = Substitute.For<IRouteFactory>();
            var particleFactory = Substitute.For<ITspParticleFactory>();
              var consoleWriter = Substitute.For<IConsoleWriter>();
            var swarmOptimizer = new SwarmOptimizer(consoleWriter,shuffler, routeFactory, particleFactory);
            swarmOptimizer.UpdateInformers(this.swarm, index, 2);
            Assert.IsTrue(this.swarm[0].InformersList.Count == 2 && this.swarm[6].InformersList.Count == 1);
        }
        [TestMethod]
        public void SwarmOptimizerUpdateInformersSetsParticleInformersListToCorrectItems()
        {
            int[] index = { 0, 1, 2, 3, 4, 5, 6, 7 };
            var shuffler = Substitute.For<IShuffler<int>>();
            var routeFactory = Substitute.For<IRouteFactory>();
            var particleFactory = Substitute.For<ITspParticleFactory>();
            var consoleWriter = Substitute.For<IConsoleWriter>();
            var swarmOptimizer = new SwarmOptimizer(consoleWriter, shuffler, routeFactory, particleFactory);
            const int maxInformers = 2;
            swarmOptimizer.UpdateInformers(this.swarm, index, maxInformers);
            var Lookup = new int[this.swarm.Length, maxInformers];
            for (int i = 0; i < this.swarm.Length; i++)
            {
                for (int p = 0; p < this.swarm[i].InformersList.Count; p++)
                {
                    var particleStub = this.swarm[i].InformersList[p] as TspParticleStub;
                    Debug.Assert(particleStub != null, "particleStub != null");
                    Lookup[i, p] = particleStub.IndexNo;
                }
            }
            Assert.IsTrue(this.CompareArrays<int>(Lookup, this.expectedLookup));
        }


    }
}
