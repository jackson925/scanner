using Amazon.Rekognition.Model;
using CVNAPDF417.Contracts.Documents;
using System;
using System.Collections.Generic;
using System.Text;
using ZXing;

namespace CVNAPDF417.Contracts.Scanning
{
    public class ScanResponse
    {
        public string Document { get; set; }
        public string BarcodeType { get; set; }
        public DateTime DecodeTime { get; set; }

    }
}
