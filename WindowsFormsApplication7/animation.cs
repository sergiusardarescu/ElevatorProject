using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication7
{
    public class animation
    {
        Bitmap[] images;

        int place = 0;

        public animation(Bitmap[] Frames)
        {
            images = Frames;
        }

        public Bitmap GiveNextImage()
        {
            Bitmap b = null;
            if (place < images.Length)
            {
                b = images[place++];
            }
            else
            {
                place = 0;
                b = images[place++];
            }

            return b;
        }
    }

}
