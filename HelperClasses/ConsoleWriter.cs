namespace TspPsoDemo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using log4net;

    public class ConsoleWriter : IConsoleWriter
    {
        #region Constants and Fields

        private readonly ILog log;

        private const string separator = ",";

        #endregion

        #region Constructors and Destructors

        public ConsoleWriter(ILog4netFactory log4NetFactory)
        {
            this.log = log4NetFactory.GetLogger<ConsoleWriter>();
        }

        #endregion

        #region Public Methods and Operators

        public double CalculateError(double bestPossibleDistance, double actualDistance)
        {
            var result = ((bestPossibleDistance - actualDistance) / bestPossibleDistance) * 100;
            return  Math.Abs(result); 

        }

        public void WritePsoAttributes(PsoAttributes psoAttributes)
        {
            var sb = new StringBuilder();
            string fileName = Path.GetFileName(psoAttributes.FileName);
            sb.Append(string.Format("Data file is {0}\r\n", fileName))
                .Append(string.Format("  Swarm size is {0}\r\n", psoAttributes.SwarmSize))
                .Append(string.Format("  Number of epochs ={0}\r\n", psoAttributes.MaxEpochs))
                .Append(string.Format("  w ={0}\r\n", psoAttributes.W))
                .Append(string.Format("  c1 ={0}\r\n", psoAttributes.C1))
                .Append(string.Format("  c2 ={0}\r\n", psoAttributes.C2))
                .Append(string.Format("  MaxInformers ={0}\r\n", psoAttributes.MaxInformers))
                .Append(string.Format("  MaxStaticEpochs ={0}\r\n", psoAttributes.MaxStaticEpochs));
            this.log.Debug(sb.ToString());
        }

       
        public void PrintOrderedRoute(IEnumerable<int> route)
        {
            //convert to '1-based' index
            var cities = route.Select(c=>c+1).ToList();
            var tail=cities.TakeWhile (n => n !=1);
            var head=cities.SkipWhile (n => n != 1);
            var ordered = head.Concat(tail);
            //places a ',' between each city in the sequence
            Console.WriteLine(string.Join(",", ordered));
         
        }

        public void WriteDistance(double distance)
        {
            Console.Write(distance+separator);
        }
        public void PrintStats(ResultManager results, int iterations)
        {
            this.log.InfoFormat("Correct Results {0} ", results.CorrectResultFound);
            double highestError = this.CalculateError(results.BestPossibleDistance, results.HighestDistanceFound);
           this.log.InfoFormat("Highest Error {0} ", highestError.ToString("F4"));
            double averageError = results.TotalError / iterations;
            this.log.InfoFormat("Average Error {0} ", averageError.ToString("F4"));
        }

        public void WriteResults(
            SwarmManager swarmManager,TspData tspData)
        {
            SwarmOptimizer swarmOptimizer = swarmManager.GetSwarmOptimizer();
            this.log.InfoFormat(
                "Best Distance found is {0} ",
                swarmOptimizer.BestGlobalItinery.TourDistance.ToString("F0"));
            int[] route = swarmOptimizer.BestGlobalItinery.DestinationIndex;
            double bestPossibleDistance = swarmManager.GetBestPossibleDistance(tspData);

            this.log.InfoFormat("Best Possible Distance is {0}", bestPossibleDistance);
            this.log.InfoFormat(
                "Error is {0} %",
                this.CalculateError(bestPossibleDistance, swarmOptimizer.BestGlobalItinery.TourDistance).ToString("F4"));
            this.log.Info("Best route found is ");
            this.PrintOrderedRoute(route);
            this.log.Info("Best possible route is ");
            this.PrintOrderedRoute(swarmManager.GetBestPossibleRoute(tspData));
        }

        #endregion
    }
}