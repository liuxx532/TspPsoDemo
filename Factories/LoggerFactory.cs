namespace TspPsoDemo
{
    using System;
   
    using log4net;

  

    class LoggerFactory : ILog4netFactory
    {
        public ILog GetLogger<T>()
        {
            return LogManager.GetLogger(typeof(T));
        }
        public ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type); 
        }

        public ILog GetLogger(string name)
        {
            return LogManager.GetLogger(name);
        }

     
    }
}
