using System.Collections.Generic;
using Utilities;

namespace TspPsoDemo
{
    public class TspDataFactory : ITspDataFactory
    {
       public TspData GetTspData(LookUpTable<double>lookupTable, IEnumerable<int> itinery)
        {
            return new TspData(lookupTable,itinery);
        }
    }
}
