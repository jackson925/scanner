using Amazon.Rekognition.Model;
using CVNAPDF417.Contracts.Documents;
using CVNAPDF417.Contracts.Scanning.ParsingStrategies;
using CVNAPDF417.Contracts.Scanning.ScanningStrategies;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace CVNAPDF417.Contracts.Scanning
{
    public class Scanner : IScan
    {
        private IScanStrategy _scanStrategy;
        private IScanStrategy FallbackStrategy;
        private bool IsFallback = true;



        public Scanner(IScanStrategy scanStrategy, IScanStrategy fallbackStrategy)
        {
            _scanStrategy = scanStrategy;
            FallbackStrategy = fallbackStrategy;
        }
        private void SetScanStrategy(IScanStrategy strategy)
        {
            _scanStrategy = strategy;
        }

        private async Task<IDocument> ScanDocument(Bitmap image)
        {
            if (IsFallback)
            {
                SetScanStrategy(FallbackStrategy);
            }

            return await _scanStrategy.Scan(image);
        }

        public async Task<IDocument> Scan(Bitmap image)
        {
            return await ScanDocument(image);
        }


        public void SetStrategy(IScanStrategy scanStrategy)
        {
            _scanStrategy = scanStrategy;
        }
    }
}
