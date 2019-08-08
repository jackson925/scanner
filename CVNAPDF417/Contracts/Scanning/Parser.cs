using CVNAPDF417.Contracts.Documents;
using CVNAPDF417.Contracts.Scanning.ParsingStrategies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CVNAPDF417.Contracts.Scanning
{
    public abstract class Parser<T> : IParser<T>
    {
        public virtual Task<IDocument> Parse(T rawDocument)
        {
            return null;
        }
    }
}
