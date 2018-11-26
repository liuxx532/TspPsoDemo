using System.Collections.Generic;
using Utilities;

namespace TspPsoDemo
{
    public interface ITspDataFactory
    {
        TspData GetTspData(LookUpTable<System.Double> lookupTable, IEnumerable<System.Int32> itinery);
    }
}