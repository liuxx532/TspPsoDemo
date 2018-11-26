namespace TspPsoDemo
{
    public interface IParticleOptimizer
    {
        int[] GetOptimizedDestinationIndex(
            IRoute currRoute,
            IRoute pBRoute,
            IRoute lBRoute,
            PsoAttributes psoAttribs);
    }
}