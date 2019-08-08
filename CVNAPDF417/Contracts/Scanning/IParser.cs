using CVNAPDF417.Contracts.Documents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CVNAPDF417.Contracts.Scanning
{
    public interface IParser<T>
    {
        Task<IDocument> Parse(T rawDocument);
    }
}
