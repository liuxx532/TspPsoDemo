namespace TspPsoDemo
{
    using System;
    using System.ComponentModel;
    using System.Configuration;

    using log4net;

    public class PsoAttributesModel
    {
        #region Constants and Fields

        public double C1;

        public double C2;

        public string FileName;

        public int MaxEpochs;

        public int MaxInformers;

        public int MaxStaticEpochs;

        public int SwarmSize;

        public double W;

        #endregion
    }

    public class PsoAttributes
    {
        #region Constants and Fields

        private readonly ILog log;

        private readonly PsoAttributesModel psoAttributesModel;

        #endregion

        #region Constructors and Destructors

        public PsoAttributes(ILog4netFactory log4NetFactory, PsoAttributesModel psoAttributesModel)
        {
            this.log = log4NetFactory.GetLogger<PsoAttributes>();
            this.psoAttributesModel = psoAttributesModel;
        }

        #endregion

        #region Public Properties

        public double C1
        {
            get
            {
                return this.psoAttributesModel.C1;
            }
            private set
            {
                this.psoAttributesModel.C1 = value;
            }
        }

        public double C2
        {
            get
            {
                return this.psoAttributesModel.C2;
            }
            private set
            {
                this.psoAttributesModel.C2 = value;
            }
        }

        public string FileName
        {
            get
            {
                return this.psoAttributesModel.FileName;
            }
            private set
            {
                this.psoAttributesModel.FileName = value;
            }
        }

        public int MaxEpochs
        {
            get
            {
                return this.psoAttributesModel.MaxEpochs;
            }
            private set
            {
                this.psoAttributesModel.MaxEpochs = value;
            }
        }

        public int MaxInformers
        {
            get
            {
                return this.psoAttributesModel.MaxInformers;
            }
            private set
            {
                this.psoAttributesModel.MaxInformers = value;
            }
        }

        public int MaxStaticEpochs
        {
            get
            {
                return this.psoAttributesModel.MaxStaticEpochs;
            }
            private set
            {
                this.psoAttributesModel.MaxStaticEpochs = value;
            }
        }

        public int SwarmSize
        {
            get
            {
                return this.psoAttributesModel.SwarmSize;
            }
            private set
            {
                this.psoAttributesModel.SwarmSize = value;
            }
        }

        public double W
        {
            get
            {
                return this.psoAttributesModel.W;
            }
            private set
            {
                this.psoAttributesModel.W = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void ReadPsoAttributesFromAppConfig()
        {
            this.FileName = this.GetSetting<string>("FileName");
            this.W = this.GetSetting<double>("W");
            this.C1 = this.GetSetting<double>("C1");
            this.C2 = this.GetSetting<double>("C2");
            this.MaxEpochs = this.GetSetting<int>("MaxEpochs");
            this.MaxStaticEpochs = this.GetSetting<int>("MaxStaticEpochs");
            this.SwarmSize = this.GetSetting<int>("SwarmSize");
            this.MaxInformers = this.GetSetting<int>("MaxInformers");
            if (this.IsLarger(this.psoAttributesModel.MaxInformers, this.psoAttributesModel.SwarmSize))
            {
                this.log.Error("Swarm size must be greater than the number of Informers");
                throw new Exception("Error Reading Attributes");
            }
            if (this.IsLarger(1, this.psoAttributesModel.MaxEpochs))
            {
                this.log.Error("MaxEpochs must be greater than zero");
                throw new Exception("Error Reading Attributes");
            }
        }

        #endregion

        #region Methods

        private T GetSetting<T>(string key)
        {
            string appSetting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(appSetting))
            {
                this.log.FatalFormat("App setting {0} not found", key);
                throw new Exception(string.Format("App setting {0} not found", key));
            }

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        }

        private bool IsLarger(double first, double second)
        {
            return first > second;
        }

        #endregion
    }
}