using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//using Google.Apis.Services;
//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Oauth2.v2;

//using Google.Cloud.Vision.V1;
//using Google.Cloud.Language.V1;


using TextRecognition.Factory;
using TextRecognition.DirectoryObject;
using TextRecognition.GoogleQueries;
using Google.Cloud.Vision.V1;
using Google.Apis.Auth.OAuth2;

namespace GoogleApi
{
    class TextRecognition
    {

        static void Main(string[] args) {

            Console.WriteLine();

            FileFactory fact = new FileFactory();
            
            DirectoryObject files = new DirectoryObject(
                Directory.GetFiles(fact.GetImageFolder())

            );
            try
            {
                Queries.Run().Wait();
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
                catch (Exception e)
                {
                    Console.WriteLine("IS_NOT_IMAGE: " + e.StackTrace);
                    continue;
                }
                
                Console.WriteLine("END_OF_API_REQUEST\nOutput file saved in: " + fact.GetOutputPath());

            }
            Console.WriteLine("OCR done.");
        }

       
      
    }
}
