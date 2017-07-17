using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRecognition.Objects

{
    class DirectoryObject
    {
        
        string[] name;
        int size;
        public DirectoryObject(string[] inName)
        {
            name = inName;
            size = inName.Length;
        }
        public string GetFileInfo(int index)
        {
            
            return name[index];
        }

        public int GetSize()
        {
            return size;
        }
    }
}
