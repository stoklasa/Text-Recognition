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
            string imagePath = @"C:\Users\vojtech.stoklasa\Documents\Visual Studio 2017\Projects\TextRecognition\ConsoleApp1\imgs\smlouva_3.jpg";

            Image img = Image.FromFile(imagePath);

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
            do{
                
                List<string> results = GoogleVisionQuery(img);
                
                Console.WriteLine("END_OF_API_REQUEST");
                string cont = "y";
                Console.WriteLine("repeat: y/n "+cont);
                
                cont = Console.ReadLine();
                while (true) {
                    
                    if((cont == "n") || (cont == "y")){
                        break;
                    }

                    Console.Write("You entered Invalid key('" + cont + "'), try again. ");

                    if (cont.Length > 1)
                        Console.Write("Also enter single letter please. ");
                    Console.WriteLine();
                    cont = Console.ReadLine();
                }
                
                if (cont == "n")
                    stop = true;
                                
            } while (!stop);
        }

        public static List<string> GoogleVisionQuery(Image img)
        {
            List<string> data = new List<string>();

            var image = img;
            var client = ImageAnnotatorClient.Create();
            var response = client.DetectText(image);

            foreach (var annotation in response)
            {   
                if (null!=annotation.Description)
                    data.Add(annotation.Description);

            }
            return data;
        }
        
        public async Task Run()
        {
            string CRED_PATH = @"C:\Users\vojtech.stoklasa\Documents\Visual Studio 2017\Projects\TextRecognition\ConsoleApp1\api_key\My First Project-ca909aeb1219.json";

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", CRED_PATH);

            GoogleCredential credential = await GoogleCredential.GetApplicationDefaultAsync();

            var service = new Oauth2Service(new BaseClientService.Initializer(){

                    HttpClientInitializer = credential,
                    ApplicationName = "Oauth2 Sample",
                }
            );
        

        }
    }
}
