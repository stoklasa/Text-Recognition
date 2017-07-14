using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Google.Cloud.Vision.V1;

using TextRecognition.Factory;
using TextRecognition.DirectoryObject;
using TextRecognition.GoogleQueries;

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

                string ReadableText = "";

                List<string[]> UnderstoodText;
                List<string> RecognizedText; 
                Console.WriteLine(CurrentFilename);

                Queries gQueries = new Queries();
                try
                {
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(CurrentFilename + "_IS_NOT_IMAGE: " + e.StackTrace);
                    continue;
                }

                Image img = Image.FromFile(CurrentFilepath);
                RecognizedText = gQueries.GoogleVisionQuery(img);

                fact.SetTextPath(CurrentFilename);
                fact.SaveFile(RecognizedText);
                foreach (var text in RecognizedText)
                {
                    ReadableText.Insert(ReadableText.Length, text);

                    UnderstoodText = gQueries.GetEntities(text);
                }

                Console.WriteLine("END_OF_API_REQUEST\nOutput file saved in: " + fact.GetOutputPath());
                foreach(var val in UnderstoodText)
                {
                    Console.WriteLine(val[0]);
                    fact.SetTextPath(CurrentFilename + "-Entities");
                }

            }
            Console.WriteLine("OCR done.");
        }

       
      
    }
}
