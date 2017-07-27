using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

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

        public void SaveFile(IEnumerable<string> data)
        {
            FileStream InputStream;

            InputStream = File.OpenWrite(TextPath);

            foreach (var description in data)
            {
                
                AddText(InputStream, description);
                
            }
            InputStream.Close();
        }
      
        public void SaveFile(IEnumerable<string[]> data)
        {
            FileStream InputStream;

            InputStream = File.OpenWrite(TextPath);

            foreach (var description in data)
            {
                if (1 < description.Length)
                {
                    for (int i = 0; i < description.Length; i++)
                    {
                        AddText(InputStream, description[i]);
                    }
                }
                else AddText(InputStream, description[0]);
            }
            InputStream.Close();
        }
        

        private static void AddText(FileStream fs, string val){
            
            byte[] info = new UTF8Encoding(true).GetBytes(val);
            fs.Write(info, 0, info.Length);
           
        }
        

    }
}
