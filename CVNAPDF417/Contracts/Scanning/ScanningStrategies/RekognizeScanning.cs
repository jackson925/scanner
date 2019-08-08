using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Amazon.Rekognition.Model;
using Amazon.Rekognition;
using Amazon.Runtime;
using System.IO;
using System.Drawing.Imaging;
using System.Linq;
using CVNAPDF417.Contracts.Documents;
using CVNAPDF417.Contracts.Scanning.ParsingStrategies;

namespace CVNAPDF417.Contracts.Scanning.ScanningStrategies
{
    public class RekognizeScanning : IScanStrategy
    {
        private AmazonRekognitionClient rekognizeClient;
        private readonly string awsAccessKey = "";
        private readonly string awsSecretKey = "";
        private IParser<List<TextDetection>> _parser;
        public RekognizeScanning(IParser<List<TextDetection>> parser)
        {
            _parser = parser;
            rekognizeClient = new AmazonRekognitionClient(awsAccessKey, awsSecretKey, Amazon.RegionEndpoint.USWest1);
        }
        public async Task<IDocument> Scan(Bitmap image)
        {
            var rekImage = new Amazon.Rekognition.Model.Image();

            rekImage.Bytes = new MemoryStream();

            image.Save(rekImage.Bytes, ImageFormat.Png);

            rekImage.Bytes.Position = 0;

            var detectionRequest = new DetectTextRequest()
            {
                Image = rekImage
            };

            List<TextDetection> detections = new List<TextDetection>();

            try
            {
                DetectTextResponse response = await rekognizeClient.DetectTextAsync(detectionRequest);
                if(response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    detections = response.TextDetections;
                }

            }
            catch(Exception e)
            {
                Console.WriteLine($"Encountered Error While trying to scan image: {e.Message}");
            }

            return await Parse(detections);
            
        }

        public async Task<IDocument> Parse(List<TextDetection> detection)
        {
            return await _parser.Parse(detection);
        }
    }
}
