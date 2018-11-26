using System;

namespace TspPsoDemo
{
    using System.Diagnostics;

    using log4net;

    using MatchBoxCore;

   

    using Utilities;

    public  class Demo
    {

         private readonly ILog tspDemoLog;

        private readonly MatchBox ioc;

        private readonly PsoAttributes psoAttributes;
        private readonly IConsoleWriter consoleWriter;

        private readonly SwarmManager swarmManager;
        private readonly DataSetManager dataSetManager;

        private readonly ResultManager resultManager;


        public Demo()
        {
            this.ioc = new MatchBox();
            this.SetCommonBindings();
            this.SetOptimizerBindings();
            var loggerFactory = this.ioc.Get<ILog4netFactory>();
            this.tspDemoLog = loggerFactory.GetLogger<Demo>();
            this.psoAttributes = this.ioc.Get<PsoAttributes>();
            this.consoleWriter = this.ioc.Get<IConsoleWriter>();
            this.dataSetManager = this.ioc.Get<DataSetManager>();
            this.swarmManager = this.ioc.Get<SwarmManager>();
            this.resultManager = this.ioc.Get<ResultManager>();
        }

        private void SetCommonBindings()
        {
            this.ioc.RegisterSingleton<ILog4netFactory, LoggerFactory>();
            this.ioc.RegisterSingleton<IRandomFactory, RandomFactory>();
            this.ioc.Register<IShuffler<int>, Shuffler<int>>();
        }

        private void SetOptimizerBindings()
        {
            this.ioc.RegisterSingleton<IConsoleWriter,ConsoleWriter>();
            this.ioc.RegisterSingleton<IRouteFactory, RouteFactory>();
            this.ioc.Register<PsoAttributesModel>();
            this.ioc.Register<PsoAttributes>();
            this.ioc.Register<LookUpTableFactory>();
            this.ioc.Register<ITspDataFactory, TspDataFactory>();
            this.ioc.Register<DataSetManager>();
          
            this.ioc.Register<IRouteUpdater, RouteUpdater>();
            this.ioc.Register<RouteManager>();
            this.ioc.RegisterSingleton<TspParticleOptimizer>();
            this.ioc.Register<ITspParticleFactory, TspParticleFactory>();
            this.ioc.Register<SwarmOptimizer>();
            this.ioc.Register<SwarmManager>();
            this.ioc.Register<ResultManager>();
           
          
        }


      public void Run()
      {
          this.tspDemoLog.Info("Reading Pso attributes from app.config");
          this.psoAttributes.ReadPsoAttributesFromAppConfig();
          this.consoleWriter.WritePsoAttributes(this.psoAttributes);
          TspData tspData= dataSetManager.ReadTspDataFromFile(psoAttributes.FileName);
         ITspParticle[] swarm= this.swarmManager.BuildSwarm(this.psoAttributes,tspData);
        this.tspDemoLog.Info("Starting Optimizer. Listing shortest trips found...");
          var stopwatch = new Stopwatch();
          this.resultManager.Reset((int)swarmManager.GetBestPossibleDistance(tspData));
          int iterations = 1;
          stopwatch.Start();
          for (int i = 0; i < iterations; i++)
          {
              int distance = this.swarmManager.Optimize(swarm,psoAttributes);
              this.resultManager.UpdateResults(distance);
          }
          stopwatch.Stop();
          TimeSpan completionTime = stopwatch.Elapsed;
          this.tspDemoLog.InfoFormat(
              "\r\n  Optimizer completed in {0} minutes",
              completionTime.TotalMinutes.ToString("F2"));
          this.consoleWriter.WriteResults(this.swarmManager,tspData);
          Console.WriteLine();
          this.tspDemoLog.Warn("Press return to end.");
          Console.ReadLine();
      }
    }
}
