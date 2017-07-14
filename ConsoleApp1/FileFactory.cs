using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextRecognition.Factory
{
    class FileFactory
    {
        string FOLDER;
        

        string TextPath;

        public FileFactory(){
            FOLDER = Environment.CurrentDirectory;
        }

        public string GetOutputPath()
        {
            return TextPath;
        }

        public void SetImagePath()
        {

        }


        public void SetTextPath(string InputFileName)
        {
            TextPath = FOLDER +@"\text\"+ InputFileName.Replace('.','(')+")"+".txt";
            TextPath.ToLower();
        }

        public string GetFolder(){

            return FOLDER;
        }
        public string GetImageFolder()
        {

            return FOLDER+@"\imgs\";
        }

        public void SaveFile(List<string> data)
        {
            FileStream InputStream;

            InputStream = File.OpenWrite(TextPath);

            foreach (var description in data)
            {
                AddText(InputStream, description);
            }
            /*
            try
            {
                    InputStream = File.OpenWrite(FullPath);

                    foreach (var description in data)
                    {
                        AddText(InputStream,description);
                    }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return;
            }
            */
        }
        private static void AddText(FileStream fs, string val){
            
            byte[] info = new UTF8Encoding(true).GetBytes(val);
            fs.Write(info, 0, info.Length);
           
        }
        

    }
}
