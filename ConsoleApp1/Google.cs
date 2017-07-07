using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;


using Google.Cloud.Vision.V1;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Newtonsoft.Json;

namespace GoogleApi
{
    class TextRecognition
    {
        static void Main(string[] args)
        {
            bool stop = false;
            try
            {
                new TextRecognition().Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var err in ex.InnerExceptions)
                {
                    Console.WriteLine("ERROR: " + err.Message);
                }

            }
            do
            {

                string imagePath = @"C:\Users\vojtech.stoklasa\Documents\Visual Studio 2017\Projects\TextRecognition\ConsoleApp1\imgs\smlouva_2.jpg";
            //    GoogleVisionQuery(imagePath);
                Console.WriteLine("end of query");
                string cont = "y";
                Console.WriteLine("repeat: y/n "+cont);
                
                cont = Console.ReadLine();
                while (true) {
                    
                    
                    if((cont == "n") || (cont == "y")){
                        break;
                    }

                    Console.WriteLine("You entered Invalid key('" + cont + "'), Try again.");

                    if (cont.Length > 1)
                        Console.Write("Also enter single letter please.");

                    cont = Console.ReadLine();
                }
                
                if (cont == "n")
                    stop = true;
                                
            } while (!stop);
        }

        public static void GoogleVisionQuery(string filePath)
        {
            var image = Image.FromFile(filePath);
            var client = ImageAnnotatorClient.Create();
            var response = client.DetectText(image);
            //  var inImage = client.DetectLabels(image);
            foreach (var annotation in response)
            {
                if (annotation.Description.Contains("\n"))
                    File.AppendAllText(@"C:\Users\vojtech.stoklasa\Documents\Visual Studio 2017\Projects\TextRecognition\ConsoleApp1\text\mined_asString.txt", annotation.Description);

                if ((annotation.Description != null)&&(!annotation.Description.Contains("\n"))) { 
                    Console.WriteLine(annotation.Description);
                    File.AppendAllText(@"C:\Users\vojtech.stoklasa\Documents\Visual Studio 2017\Projects\TextRecognition\ConsoleApp1\text\mined_asWords.txt", annotation.Description);

                    File.AppendAllText(@"C:\Users\vojtech.stoklasa\Documents\Visual Studio 2017\Projects\TextRecognition\ConsoleApp1\text\mined_asWordsToString.txt", annotation.Description+" ");

                }
            }

        }
        
        public async Task Run()
        {
            string CRED_PATH = @"C:\Users\vojtech.stoklasa\Documents\Visual Studio 2017\Projects\TextRecognition\ConsoleApp1\api_key\My First Project-ca909aeb1219.json";

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", CRED_PATH);

            GoogleCredential credential = await GoogleCredential.GetApplicationDefaultAsync();

            var service = new Oauth2Service(new BaseClientService.Initializer()

                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Oauth2 Sample",
                }
            );
        

        }
    }
}
