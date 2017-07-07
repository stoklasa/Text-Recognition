using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Cloud.Vision.V1;


namespace GoogleApi
{
    class TextRecognition
    {
        static void Main(string[] args)
        {
            Console.Write(GoogleVisionQuery(Environment.CurrentDirectory+
                @"\imgs\text_wiki.PNG"));
        }
        public static string GoogleVisionQuery(string filePath)
        {
            string textVal = "";
            var image = Image.FromFile(filePath);
            var client = ImageAnnotatorClient.Create();
            var response = client.DetectText(image);
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                    Console.WriteLine(annotation.Description);
            }
            return textVal;
        }
    }
}
