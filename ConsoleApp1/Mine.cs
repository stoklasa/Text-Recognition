using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using System.Net;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Drawing;

namespace MyApi

{
    public class Program
    {
       
    }

    class Recognizer
    {

        Recognizer(string fontName)
        {
        }
        public char[] getChar(Bitmap bmp)
        {
            char[] text = { 'a','b' };



            return text;

        }
        private void ConstructFontWithString(PrintEventArgs e)
        {
            Font font1 = new Font("Arial", 20);
        }
        
    }
   
 }
