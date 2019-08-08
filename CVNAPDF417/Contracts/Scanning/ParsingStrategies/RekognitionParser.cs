using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.Rekognition.Model;
using CVNAPDF417.Contracts.Documents;

namespace CVNAPDF417.Contracts.Scanning.ParsingStrategies
{
    public class RekognitionParser<T> : Parser<T> where T : List<TextDetection>
    {
        public override Task<IDocument> Parse(T textDetections)
        {
            IDocument scanDocument = new DriversLicense();

            return Task.Run(() =>
            {
                var builder = new StringBuilder("");
                textDetections.ForEach(text => {
                    builder.Append(text.DetectedText);
                });

                var driversLicenseString = builder.ToString();
                if (driversLicenseString.Contains("Driver", StringComparison.InvariantCultureIgnoreCase)){
                    Console.WriteLine("Pretty sure this thing is a drivers license..?");
                }
                    

                Console.Write(builder.ToString());
                scanDocument = new DriversLicense();
                return scanDocument;
            });
        }
    }
}
