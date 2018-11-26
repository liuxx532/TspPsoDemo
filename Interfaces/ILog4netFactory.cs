namespace TspPsoDemo
{
    using System;

    using log4net;

    public interface ILog4netFactory
    {
        ILog GetLogger<T>();

        ILog GetLogger(Type type);

        ILog GetLogger(string name);
    }
}