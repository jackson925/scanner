using CVNAPDF417.Contracts.Documents;
using CVNAPDF417.Contracts.Scanning.ParsingStrategies;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace CVNAPDF417.Contracts.Scanning.ScanningStrategies
{
    public class ZxingScanning : IScanStrategy
    {
        private readonly BarcodeReader _scanner;
        private readonly IParser<string> _parser;

        public ZxingScanning(IParser<string> parser)
        {
            _parser = parser;
            _scanner = new BarcodeReader();
        }

        public Task<IDocument> Scan(Bitmap image)
        {
            return Task.Run(async () =>
            {
                var result = _scanner.Decode(image);
                return await Parse(result.Text);
            });
        }


        public async Task<IDocument> Parse(string document)
        {
            return await _parser.Parse(document);
        }

    }
}
