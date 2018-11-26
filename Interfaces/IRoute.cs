namespace TspPsoDemo
{
    using Utilities;

    public interface IRoute
    {
        int[] DestinationIndex { get; set; }

        void SetDistanceTable(LookUpTable<double> distTable);
        double TourDistance { get; set; }

        void CopyTo(IRoute targetItinery);

        Route Clone();

        double CalculateTotalDistance();
        int SegmentSize { get; set; }
    }
}