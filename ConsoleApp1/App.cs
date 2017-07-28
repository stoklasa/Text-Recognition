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

            Console.WriteLine();

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
                List<List<string>> ResponsesAsStringList = new List<List<string>>();
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
                /*
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
                
                Console.WriteLine("END_OF_API_REQUEST\nOutput file saved in: " + fact.GetOutputPath());


                fact.SetTextPath(CurrentFilename + "-Entites");

                foreach (var response in UnderstoodText)
                {
                    ResponsesAsStringList.Add(response.ResponsesToString(UnderstoodText));
                }
                fact.SaveFile(ResponsesAsStringList);
                */
            }
        }

       
      
    }
}
