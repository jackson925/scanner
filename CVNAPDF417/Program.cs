using Amazon.Rekognition.Model;
using CVNAPDF417.Contracts.Documents;
using CVNAPDF417.Contracts.Scanning;
using CVNAPDF417.Contracts.Scanning.ParsingStrategies;
using CVNAPDF417.Contracts.Scanning.ScanningStrategies;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZXing;

namespace CVNAPDF417
{
    
    
    class Program
    {

        
        static void Main(string[] args)
        {
          
            var filePath = @"";

            var rekognitionStrategy = new RekognizeScanning(new RekognitionParser<List<TextDetection>>());
            var pdf417Strategy = new ZxingScanning(new PDF417<string>());

            var scanner = new Scanner(pdf417Strategy, rekognitionStrategy);

            var barcodeBitmap = (Bitmap)System.Drawing.Image.FromFile(filePath);

            var result = scanner.Scan(barcodeBitmap).GetAwaiter().GetResult();

            result.PrintFields();

            Console.ReadLine();
        }
    }

}
