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

        public static void NLPQuery(string text)
        {
            var client = LanguageServiceClient.Create();
            var  response = client.AnalyzeEntities(new Document()
            {
                Content = text,
                Type = Document.Types.Type.PlainText
            });
            WriteEntities(response.Entities);

        }

        private static void WriteEntities(IEnumerable<Entity> entities)
        {
            Console.WriteLine("Entities:");
            foreach (var entity in entities)
            {
                Console.WriteLine($"\tName: {entity.Name}");
                Console.WriteLine($"\tType: {entity.Type}");
                Console.WriteLine($"\tSalience: {entity.Salience}");
                Console.WriteLine("\tMentions:");
                foreach (var mention in entity.Mentions)
                    Console.WriteLine($"\t\t{mention.Text.BeginOffset}: {mention.Text.Content}");
                Console.WriteLine("\tMetadata:");
                foreach (var keyval in entity.Metadata)
                {
                    Console.WriteLine($"\t\t{keyval.Key}: {keyval.Value}");
                }
            }
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
