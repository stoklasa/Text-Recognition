using System;
using System.Collections.Generic;
using System.IO;

using Google.Cloud.Vision.V1;

using TextRecognition.Factory;
using TextRecognition.Objects;
using TextRecognition.Queries;

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
                     Auth.Run().Wait();
                    
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

                List<Response> UnderstoodText;
                List<string> ResponsesAsStringList = new List<string>();
                List<string> RecognizedText; 
                Console.WriteLine(CurrentFilename);

                VisionQuery vision = new VisionQuery();
                NLPQuery nlp = new NLPQuery();

                Image img;

                try
                {
                    img = Image.FromFile(CurrentFilepath);

                }
                catch (Exception e)
                {
                    Console.WriteLine(CurrentFilename + "_IS_NOT_IMAGE: " + e.StackTrace);
                    continue;
                }

                try
                {
                    RecognizedText = vision.GoogleVisionQuery(img);

                }
                catch (Exception e)
                {
                    Console.WriteLine(CurrentFilename + "_VISION_API_ERROR: " + e.StackTrace);
                    continue;
                }
                
                fact.SetTextPath(CurrentFilename);
                fact.SaveFile(RecognizedText);

                Console.WriteLine("OCR done.");

                foreach (var text in RecognizedText) {

                   ReadableText =  ReadableText.Insert(ReadableText.Length, text);
                }

                try
                {
                   UnderstoodText = nlp.GetEntities(ReadableText);
                }
                catch (Exception e)
                {
                    Console.WriteLine(CurrentFilename + "_LANGUAGE_API_ERROR: " + e.StackTrace);
                    continue;
                }
                
                Console.WriteLine("END_OF_API_REQUEST, Output file saved in: " + fact.GetOutputPath());


                
                fact.SetTextPath(CurrentFilename + "-Entites");

                ResponsesAsStringList = Response.ResponsesToString(UnderstoodText);

                fact.SaveFile(ResponsesAsStringList);

            }
        }

       
      
    }
}
