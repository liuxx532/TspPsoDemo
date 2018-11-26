namespace TspPsoDemo
{
    using System.Collections.Generic;

    public interface IConsoleWriter
    {
        double CalculateError(double bestPossibleDistance, double actualDistance);

        void WritePsoAttributes(PsoAttributes psoAttributes);

        void PrintOrderedRoute(IEnumerable<int> route);

        void WriteDistance(double distance);

        void PrintStats(ResultManager results, int iterations);

        void WriteResults(
            SwarmManager swarmManager,TspData tspData);
    }
}