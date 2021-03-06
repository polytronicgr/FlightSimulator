// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
// 11/05/19 19:45    
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
namespace Jp.Maker1.Util.Gui
{

    using System;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class Caption
    {
        public static double lifeTime;
        public static int x;
        public static int y;
        public static Color col;
        public static Font fon;
        public static String caption;

        public Caption()
        {
            lifeTime = 0.0D;
            x = 0;
            y = 0;
            col = Color.Gray;
            fon = new Font("SansSerif",12);
            caption = "";
        }

        public Caption(int xIn, int yIn, Color colIn, Font fonIn)
        {
            lifeTime = 0.0D;
            SetPos(xIn, yIn);
            SetFont(fonIn);
            SetColor(colIn);
            caption = "";
        }

        public static void Initial(int xIn, int yIn, Color colIn, Font fonIn)
        {
            lifeTime = 0.0D;
            SetPos(xIn, yIn);
            SetFont(fonIn);
            SetColor(colIn);
            caption = "";
        }

        public static void Draw(Graphics g)
        {
            if (lifeTime > 0.0D)
            {
                //g.SetColor(col);
               // g.SetFont(fon);
                g.DrawString(caption, fon,new SolidBrush(col), x - caption.Length / 2 * 6, y);
            }
        }

        public static void Update(double dt)
        {
            lifeTime -= dt;
            if (lifeTime < 0.0D)
                lifeTime = 0.0D;
        }

        public static void SetPos(int xIn, int yIn)
        {
            x = xIn;
            y = yIn;
        }

        public static void SetColor(Color colIn)
        {
            col = colIn;
        }

        public static void SetFont(Font fonIn)
        {
            fon = fonIn;
        }

        public static void SetCaption(String captionIn, double lifeTimeIn)
        {
            caption = captionIn;
            lifeTime = lifeTimeIn;
        }
    }
}