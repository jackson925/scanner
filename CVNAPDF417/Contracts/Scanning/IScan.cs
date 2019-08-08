using CVNAPDF417.Contracts.Documents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace CVNAPDF417.Contracts.Scanning
{
    public interface IScan
    {
        Task<IDocument> Scan(Bitmap image);

        void SetStrategy(IScanStrategy strategyKey);
    }
}
