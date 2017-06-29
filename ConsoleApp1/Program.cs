using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Recognizer
    {

        Recognizer(string fontName)
        {
            Font usedFont = new Font(fontName, FontStyle.Regular);

            string str = "hello world";
        }
        public char[] getChar(Bitmap bmp)
        {
            char[] text;



            return text;

        }
        private void ConstructFontWithString(PaintEventArgs e)
        {
            Font font1 = new Font("Arial", 20);
            e.Graphics.DrawString("Arial Font", font1, Brushes.Red, new PointF(10, 10));
        }
    }
   
        }
