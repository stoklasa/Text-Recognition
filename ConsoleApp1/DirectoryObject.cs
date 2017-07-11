using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRecognition.DirectoryObject;
{
    class DirectoryObject
    {
        string path;
        string name;
        public DirectoryObject(string inPath,string inName)
        {
            name = inName;
            path = inPath;
        }
    }
}
