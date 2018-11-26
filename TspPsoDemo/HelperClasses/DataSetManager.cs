namespace TspPsoDemo
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;

    using log4net;

    using Utilities;

    public class DataSetManager
    {
        #region Constants and Fields

        private readonly ILog log;

        private readonly LookUpTableFactory lookUpTableFactory;

        private readonly ITspDataFactory dataFactory;

        #endregion

        #region Constructors and Destructors

        public DataSetManager(ILog4netFactory log4NetFactory, LookUpTableFactory lookUpTableFactory,ITspDataFactory dataFactory)
        {
            this.log = log4NetFactory.GetLogger<DataSetManager>();
            this.lookUpTableFactory = lookUpTableFactory;
            this.dataFactory = dataFactory;
        }

        #endregion

        #region Public Methods and Operators

        public TspData GetTspDataFromDataSet(DataSet dataSet)
        {
            DataTable bestRouteTable = dataSet.Tables["BestRoute"];
            var bestRoute = this.GetBestRoute(bestRouteTable);
            DataTable distanceDataTable = dataSet.Tables["DistanceLookup"];
            var distanceLookup = this.DataTableToLookUpTable(distanceDataTable);
            return dataFactory.GetTspData(distanceLookup, bestRoute);
           
        }

        public TspData ReadTspDataFromFile(string xmlFilename)
        {
            DataSet tspDataSet = this.ReadTspDataSetFromFile(xmlFilename);
            return this.GetTspDataFromDataSet(tspDataSet);
        }

        public DataSet ReadTspDataSetFromFile(string xmlFilename)
        {
            string dataSetName = Path.GetFileNameWithoutExtension(xmlFilename);
            string fileName = Path.GetFileName(xmlFilename);
            var tspDataSet = new DataSet(dataSetName);
            this.log.InfoFormat("Reading Data from {0} ", fileName);
            // Read the XML document back in. 
            // Create new FileStream to read schema with.
            using (var streamRead = new FileStream(xmlFilename, FileMode.Open))
            {


                tspDataSet.ReadXml(streamRead);
              
            }
          
        
            return tspDataSet;
        }

        #endregion

        #region Methods

        private IEnumerable<int> GetBestRoute(DataTable bestRouteDataTable)
        {
           return   bestRouteDataTable.Rows[0].ItemArray.Select(Convert.ToInt32);
          
        }

      
        private LookUpTable<double> DataTableToLookUpTable(DataTable distanceDataTable)
        {
            int rowsCount = distanceDataTable.Rows.Count;
            int colsCount = distanceDataTable.Columns.Count;
            var distanceArray = new double[rowsCount, colsCount];
            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = 0; col < colsCount; col++)
                {
                    distanceArray[row, col] = Convert.ToDouble(distanceDataTable.Rows[row][col]);
                }
            }
            return this.lookUpTableFactory.Create( distanceArray);
        }

        #endregion
    }
}