using System;
using System.Collections.Generic;

using Google.Cloud.Vision.V1;

namespace TextRecognition.Queries
{
    class VisionQuery   {

        public List<string> GoogleVisionQuery(Image img)
        {
            List<string> data = new List<string>();
            
            var client = ImageAnnotatorClient.Create();

            var response = client.DetectText(img);

            foreach (var annotation in response)
            {
                if ((null != annotation.Description))
                {
                    if (annotation.Description.Contains("\n")){
                        data.Add(annotation.Description);
                    }

                    else if (!annotation.Description.Contains("\n"))
                    {
                        Console.WriteLine(annotation.Description);
                    }
                }
            }

            return data;
        }
    }
}
