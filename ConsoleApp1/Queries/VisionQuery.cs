using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Google.Cloud.Vision.V1;

namespace TextRecognition.Queries
{
    class VisionQuery   {

        public List<string> GoogleVisionQueryAsList(Image img)
        {
            List<string> data = new List<string>();
            
            var client = ImageAnnotatorClient.Create();

            var response = client.DetectText(img);

            var deserialzed = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.ToString());

            return data;
        }

        public string GoogleVisionQueryAsText(Image img)
        {
            string data;
            
            var client = ImageAnnotatorClient.Create();

            var response = client.DetectText(img);


            foreach (var annotation in response)
            {
                if ((null != annotation.Description))
                {
                    if (annotation.Description.Contains("\n"))
                    {
                        data = annotation.Description;
                    }
                    else data = "";

                }
                else throw new System.FormatException("Image does not contain any text");
            }

            return data;
        }
    }
}
