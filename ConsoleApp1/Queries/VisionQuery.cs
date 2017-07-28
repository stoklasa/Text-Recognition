using System;
using System.Collections.Generic;

using Google.Cloud.Vision.V1;

namespace TextRecognition.Queries
{
    class VisionQuery   {

        public List<string> GoogleVisionQueryAsList(Image img)
        {
            List<string> data = new List<string>();

            var image = img;

            var client = ImageAnnotatorClient.Create();

            var response = client.DetectText(image);


            foreach (var annotation in response)
            {
                if ((null != annotation.Description))
                {
                   
                    if (!annotation.Description.Contains("\n"))
                    {
                        data.Add(annotation.Description);
                    }
                }
            }

            return data;
        }

        public string GoogleVisionQueryAsText(Image img)
        {
            string data;

            var image = img;

            var client = ImageAnnotatorClient.Create();

            var response = client.DetectText(image);


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
