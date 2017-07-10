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

        string FileName;

        string FullPath;

        public FileFactory(){
            FileName = @"data" + DateTime.Today;
            FOLDER = Environment.CurrentDirectory;
            FullPath = FOLDER + @"\\text\\" + FileName+".txt";
        }

        public string GetOutputPath()
        {
            return FullPath;
        }

        public string GetImagePath(string file_name)
        {
            return FOLDER +@"\\imgs\\"+ file_name;
        }

        public void SaveFile(List<string> data)
        {
            FileStream InputStream;
            try
            {
                InputStream = File.Create(FullPath);
                File.OpenWrite(FullPath);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return;
            }
                foreach (var description in data)
                {
                    AddText(InputStream, description);
                }
            
        }
        private static void AddText(FileStream fs, string val){
            
            byte[] info = new UTF8Encoding(true).GetBytes(val);
            fs.Write(info, 0, info.Length);
           
        }
    }
}
