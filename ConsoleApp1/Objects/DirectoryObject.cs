
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
