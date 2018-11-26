namespace Utilities
{
    public interface IRandomFactory
    {
        double NextRandomDouble();

        int NextRandom(int min, int max);

        bool NextBool();
    }
}
