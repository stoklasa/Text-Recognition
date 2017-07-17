using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Google.Cloud.Vision.V1;
using Google.Cloud.Language.V1;

namespace TextRecognition.Objects
{
    class VisionQuery
    {
        public List<string> GoogleVisionQuery(Image img)
        {
            List<string> data = new List<string>();

            var image = img;

            var client = ImageAnnotatorClient.Create();

            var response = client.DetectText(image);


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
