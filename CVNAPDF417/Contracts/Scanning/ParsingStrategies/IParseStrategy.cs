using CVNAPDF417.Contracts.Documents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CVNAPDF417.Contracts.Scanning.ParsingStrategies
{
    public interface IParseStrategy
    {
        Task<IDocument> Parse(string document);
    }
}
