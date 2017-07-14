using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;

using Google.Cloud.Vision.V1;
using Google.Cloud.Language.V1;

namespace TextRecognition.GoogleQueries
{
    class Queries
    {

        public static async Task Run()
        {

            string CRED_PATH = new Factory.FileFactory().GetFolder() + @"\api_key\My First Project-ca909aeb1219.json";
            

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", CRED_PATH);

            GoogleCredential credential = await GoogleCredential.GetApplicationDefaultAsync();

            var service = new Oauth2Service(new BaseClientService.Initializer(){

                    HttpClientInitializer = credential,
                    ApplicationName = "Oauth2 Sample",
                }
            );

        }

        public List<string[]> GetEntities(string text)
        {


            List<string[]> entites = new List<string[]>();
            

            var client = LanguageServiceClient.Create();
            var response = client.AnalyzeEntities(new Document()
            {
                Content = text,
                Type = Document.Types.Type.PlainText
            });
            
            foreach(var entity in response.Entities)
            {
                string[] val = new string[5];
                val[0] = entity.Name;
                val[1] = entity.Type.ToString();
                val[2] = entity.Metadata.Values.ToString();
                val[3] = entity.Salience.ToString();
                val[4] = entity.Mentions.ToString();

                entites.Add(val);
            }
            return entites;
            
        }


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
