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

        public FileFactory(){        }

        public string GetOutputPath()
        {
            return FullPath;
        }

        public string GetImagePath(string file_name)
        {
            FileName = @"data_from_" + file_name.Replace('.', '_');
            FOLDER = Environment.CurrentDirectory;
            FullPath = FOLDER + @"\\text\\" + FileName + ".txt";
            return FOLDER +@"\\imgs\\"+ file_name;
        }

        public void SaveFile(List<string> data)
        {
            FileStream InputStream;
            
            try
            {
                Console.Write(FullPath);

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
                
            
        }
        private static void AddText(FileStream fs, string val){
            
            byte[] info = new UTF8Encoding(true).GetBytes(val);
            fs.Write(info, 0, info.Length);
           
        }
        

    }
}
