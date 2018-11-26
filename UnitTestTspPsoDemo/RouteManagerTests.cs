namespace UnitTestTspPsoDemo
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NSubstitute;
   
   

    using TspPsoDemo;

    using Utilities;

    public class TspParticleStub : ITspParticle
    {
        #region Constructors and Destructors

        public TspParticleStub(int index)
        {
            this.IndexNo = index;
            this.InformersList = new List<ITspParticle>();
        }

        #endregion

        #region Public Properties

        public IRoute CurrentRoute { get; set; }

        public int IndexNo { get; set; }

        public List<ITspParticle> InformersList { get; set; }

        public IRoute LocalBestRoute { get; set; }

        public IRoute PersonalBestRoute { get; set; }

        #endregion

        #region Public Methods and Operators

        public double Optimize(PsoAttributes psoAttributes)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [TestClass]
    public class RouteManagerTests
    {
        #region Constants and Fields

        private LookUpTable<double> DistanceTable;

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void RouteBuilderGetSegmentSizeCalculatesCorrectSegmentSize()
        {
            var routeUpdater = Substitute.For <IRouteUpdater>();
            var randomFactory = Substitute.For<IRandomFactory>();
            var routeManager = new RouteManager(randomFactory, routeUpdater);
            var destinations = new[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            var route = new Route(this.DistanceTable, destinations);
           var sectionSize= routeManager.GetSectionSize(route, 8.0, 32.0);
            Assert.IsTrue(sectionSize==2);
        }

        [TestMethod]
        public void RouteBuilderUpdateVelocityCalculatesCorrectVelocity()
        {
            var routeUpdater = Substitute.For<IRouteUpdater>();
            var randomFactory = Substitute.For<IRandomFactory>();
            var routeManager = new RouteManager(randomFactory, routeUpdater);
           var destinations = new[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            var route = new Route(this.DistanceTable, destinations);
            double velocity = routeManager.UpdateVelocity(route, 0.5, 0.2);
            Assert.AreEqual(3.94E-6, velocity,1E-8);
        }

        [TestInitialize]
        public void TestInitialise()
        {
            var arr = new double[ , ]
            {
                { 0, 4727, 1205, 6363, 3657, 3130, 2414, 563 }, { 4727, 0, 3588, 2012, 1842, 6977, 6501, 5187 },
                { 1205, 3588, 0, 5163, 2458, 3678, 3071, 1742 }, { 6363, 2012, 5163, 0, 2799, 8064, 7727, 6878 },
                { 3657, 1842, 2458, 2799, 0, 5330, 4946, 4200 }, { 3130, 6977, 3678, 8064, 5330, 0, 743, 3209 },
                { 2414, 6501, 3071, 7727, 4946, 743, 0, 2468 }, { 563, 5187, 1742, 6878, 4200, 3209, 2468, 0 }
            };
            this.DistanceTable=new LookUpTable<double>(arr);
        }

        #endregion
    }
}
