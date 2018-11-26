namespace UnitTestTspPsoDemo
{
    using System.Collections;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

   

    using TspPsoDemo;

    using Utilities;

    [TestClass]
    public class TestRouteBuilder
    {
        #region Constants and Fields

        private LookUpTable<double> DistanceTable;

      

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void FinalizeDestinationAllowsOnlyLocationsWithTheCorrespondingMaskBitSet()
        {
            var destinations = new[] { 7, 6, 5, 4, 3, 2, 1, 0 };
            var route = new Route(this.DistanceTable, destinations);
            var routeUpdater = new RouteUpdater();
            routeUpdater.Initialise(8);
            var mask = new BitArray(8, false);
            mask.Set(1, true);
            mask.Set(7, true);
            routeUpdater.AvailabilityMask = mask;
            int[] result = routeUpdater.FinalizeDestinationIndex(route);
            int[] testIndex = { 7, 1, 0, 0, 0, 0, 0, 0 };
            Assert.IsTrue(result.SequenceEqual(testIndex));
        }

        [TestMethod]
        public void RouteBuilderAddsASegmentToDestinationIndex()
        {
            var destinations = new[] { 7, 6, 5, 4, 3, 2, 1, 0 };
            int[] testIndex = { 4, 3, 2, 0, 0, 0, 0, 0 };
            IRoute section = new Route(this.DistanceTable, destinations);
            section.SegmentSize = 3;
            var routeUpdater = new RouteUpdater();
            routeUpdater.Initialise(8);
            routeUpdater.AddSection(3, section, false);
            Assert.IsTrue(routeUpdater.GetDestinationIndex().SequenceEqual(testIndex));
        }

        [TestMethod]
        public void RouteBuilderAddsTwoSectionsToDestinationIndex()
        {
            var destinations = new[] { 7, 6, 5, 4, 3, 2, 1, 0 };
            int[] testIndex = { 4, 3, 2, 7, 6, 0, 0, 0 };
            IRoute sectionOne = new Route(this.DistanceTable, destinations);
            sectionOne.SegmentSize = 3;
            var routeUpdater = new RouteUpdater();
            routeUpdater.Initialise(8);
            routeUpdater.AddSection(3, sectionOne, false);
            IRoute sectionTwo = new Route(this.DistanceTable, destinations);
            sectionTwo.SegmentSize = 2;
            routeUpdater.AddSection(0, sectionTwo, false);
            Assert.IsTrue(routeUpdater.GetDestinationIndex().SequenceEqual(testIndex));
        }

        [TestMethod]
        public void RouteBuilderDoesNotAddDuplicateDestinations()
        {
            var destinations = new[] { 7, 6, 5, 4, 3, 2, 1, 0 };
            int[] testIndex = { 4, 3, 2, 6, 5, 0, 0, 0 };
            IRoute sectionOne = new Route(this.DistanceTable, destinations);
            sectionOne.SegmentSize = 3;
            IRoute sectionTwo = new Route(this.DistanceTable, destinations);
            sectionTwo.SegmentSize = 3;
            var routeUpdater = new RouteUpdater();
            routeUpdater.Initialise(8);
            routeUpdater.AddSection(3, sectionOne, false);
            routeUpdater.AddSection(1, sectionTwo, false);
            Assert.IsTrue(routeUpdater.GetDestinationIndex().SequenceEqual(testIndex));
        }

        [TestMethod]
        public void RouteBuilderFinalizeAddsAllMissingLocationsInCorrectOrder()
        {
            var destinations = new[] { 7, 6, 5, 4, 3, 2, 1, 0 };
            int[] testIndex = { 4, 3, 2, 6, 5, 7, 1, 0 };
            IRoute sectionOne = new Route(this.DistanceTable, destinations);
            sectionOne.SegmentSize = 3;
            IRoute sectionTwo = new Route(this.DistanceTable, destinations);
            sectionTwo.SegmentSize = 3;
            var routeUpdater = new RouteUpdater();
            routeUpdater.Initialise(8);
            routeUpdater.AddSection(3, sectionOne, false);
            routeUpdater.AddSection(1, sectionTwo, false);
            IRoute route = new Route(this.DistanceTable, destinations);
            routeUpdater.FinalizeDestinationIndex(route);
            Assert.IsTrue(routeUpdater.GetDestinationIndex().SequenceEqual(testIndex));
        }

        [TestMethod]
        public void RouteBuilderMaskMasksCorrectBitInFlagArray()
        {
            var distances = new[] { 4, 6, 5, 7, 1, 2, 3, 0 };
            var testArray = new BitArray(8, false);
            testArray.Set(5, true);
            testArray.Set(7, true);
            testArray.Set(1, true);
            var routeUpdater = new RouteUpdater();
            routeUpdater.Initialise(8);
            var mask = new BitArray(8, true);
            mask.Set(6, false);
            routeUpdater.AvailabilityMask = mask;
            IRoute section = new Route(this.DistanceTable, distances);
            section.SegmentSize = 4;
            routeUpdater.SetSelectedMask(1, section);

            Assert.IsTrue(this.CompareBitArrays(routeUpdater.SelectedMask, testArray));
        }

        [TestMethod]
        public void RouteBuilderSetFlagArraySetsCorrectBits()
        {
            var destinations = new[] { 4, 6, 5, 7, 1, 2, 3, 0 };
            var section = new Route(this.DistanceTable, destinations) { SegmentSize = 4 };
            var testArray = new BitArray(8, false);
            testArray.Set(6, true);
            testArray.Set(5, true);
            testArray.Set(7, true);
            testArray.Set(1, true);
            var routeUpdater = new RouteUpdater();
            routeUpdater.Initialise(8);

            routeUpdater.SetSelectedMask(1, section);

            Assert.IsTrue(this.CompareBitArrays(routeUpdater.SelectedMask, testArray));
        }

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
            this.DistanceTable = new LookUpTable<double>(arr);
           
        }

        #endregion

        #region Methods

        private bool CompareBitArrays(BitArray x, BitArray y)
        {
            if (x.Count != y.Count)
            {
                return false;
            }
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}