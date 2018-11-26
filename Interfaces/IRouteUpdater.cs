namespace TspPsoDemo
{
    using System.Collections;

   

    public interface IRouteUpdater
    {
        BitArray AvailabilityMask { get; set; }

        bool Isinitialised { get; }

        BitArray SelectedMask { get; set; }

        void Initialise(int cityCount);

        void AddSection(int pointer, IRoute section, bool isReversed);

        int[] FinalizeDestinationIndex(IRoute route);

        int[] GetDestinationIndex();

        void Reset();

        void SetDestinationIndex(int startPosition, IRoute section);

        void SetSelectedMask(int pointer, IRoute section);
    }
}
