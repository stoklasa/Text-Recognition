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

            

            DirectoryObject files = new DirectoryObject(
                Directory.GetFiles(fact.GetImageFolder())

            );

            
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

            for (int i = 0; i < files.GetSize(); i++)
            {
                string CurrentFilename
                  = Path.GetFileName(files.GetFileInfo(i));
                string CurrentFilepath = files.GetFileInfo(i);

                
                Console.WriteLine(CurrentFilename);
                try
                {
                    Image img = Image.FromFile(CurrentFilepath);
                    List<string> results = GoogleVisionQuery(img);

                    fact.SetTextPath(CurrentFilename);
                    fact.SaveFile(results);
                }
                catch(Exception e)
                {
                    Console.WriteLine("IS_NOT_IMAGE: "+e.StackTrace);
                    continue;
                }
                
                

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
                if ((null != annotation.Description))
                {
                    if (annotation.Description.Contains("\n"))
                    {

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
        
        public async Task Run()
        {
            string CRED_PATH = @"C:\Users\vojtech.stoklasa\Documents\Visual Studio 2017\Projects\TextRecognition\ConsoleApp1\bin\Debug\api_key\My First Project-ca909aeb1219.json";

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
