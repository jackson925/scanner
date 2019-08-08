using CVNAPDF417.Contracts.Documents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace CVNAPDF417.Contracts.Scanning
{
    public interface IScanStrategy
    {
        Task<IDocument> Scan(Bitmap image);
    }
}
