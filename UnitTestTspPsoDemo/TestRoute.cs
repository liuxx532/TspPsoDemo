using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestTspPsoDemo
{
    using System;
    using System.Linq;

    using TspPsoDemo;

    using Utilities;

    [TestClass]
    public class TestRoute
    {

        LookUpTable<double> DistanceTable;

        [TestInitialize]
        public void TestInitialise()
        {

            var arr = new double[,]
            {
                { 0, 4727, 1205, 6363, 3657, 3130, 2414, 563 }, { 4727, 0, 3588, 2012, 1842, 6977, 6501, 5187 },
                { 1205, 3588, 0, 5163, 2458, 3678, 3071, 1742 }, { 6363, 2012, 5163, 0, 2799, 8064, 7727, 6878 },
                { 3657, 1842, 2458, 2799, 0, 5330, 4946, 4200 }, { 3130, 6977, 3678, 8064, 5330, 0, 743, 3209 },
                { 2414, 6501, 3071, 7727, 4946, 743, 0, 2468 }, { 563, 5187, 1742, 6878, 4200, 3209, 2468, 0 }
            };
            this.DistanceTable=new LookUpTable<double>(arr);
        }

        [TestMethod]
        public void TestClone()
        {
            var destinations = new[] { 7, 6, 5, 4, 3, 2, 1, 0 };
            var route = new Route(this.DistanceTable, destinations);
           Route clone = route.Clone();

            Assert.IsTrue(clone.DestinationIndex.SequenceEqual(destinations));
        }
        [TestMethod]
        public void TestCalculateDistance()
        {
            var destinations = new[] { 7, 6, 5, 4, 3, 2, 1, 0 };
            var route = new Route(this.DistanceTable, destinations);
            double distance = route.CalculateTotalDistance();
            Assert.AreEqual(distance, 25381);
        }

        [TestMethod]
        public void RouteFactoryGetNewRouteReturnsCorrectlyConfiguredRoute()
        {
            int[] index = { 0, 1, 2, 3, 4, 5, 6, 7 };
            var routeFactory = new RouteFactory();
            IRoute route = routeFactory.GetRoute(this.DistanceTable, index);
            Assert.IsTrue(Math.Abs(Math.Floor(route.TourDistance) - 25381) < 1E-6
               && index.SequenceEqual(route.DestinationIndex));
        }
    }
}
