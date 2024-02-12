using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Comp208WPF
{
    internal static class Util
    {
        internal static Icon MakeIcon(Bitmap bitmap)
        {
            IntPtr ptr = bitmap.GetHicon();
            return Icon.FromHandle(ptr);
        }
        internal static ImageSource MakeIcon1(Bitmap bitmap)
        {
            ImageSourceConverter converter = new ImageSourceConverter();
            return (ImageSource)converter.ConvertFrom(bitmap);
        }
    }
}
