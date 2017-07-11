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
using TextRecognition.Factory;
using TextRecognition.DirectoryObject;

namespace GoogleApi
{
    class TextRecognition
    {

        static void Main(string[] args) {

            FileFactory fact = new FileFactory();
            string[][] files =  new string[2][Directory.GetFiles(fact.GetFolder(), "*.jpg", SearchOption.AllDirectories).Length];
            files[0] = DirectoryObject(Directory.GetFiles(fact.GetFolder(), "*.jpg", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++) {
                files[i] = new DirectoryObject(Directory.GetFiles(fact.GetFolder(), "*.jpg", SearchOption.AllDirectories), Directory.GetFiles(fact.GetFolder()));
            }
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

            foreach (var file in files)
            {
                
                Console.WriteLine(path);
                Image img = Image.FromFile(file[0]);
                
                List<string> results = GoogleVisionQuery(img);

                fact.SaveFile(results);

                Console.WriteLine("END_OF_API_REQUEST\nOutput file saved in: " + fact.GetOutputPath());
               
            }
            Console.WriteLine("OCR done.");
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
