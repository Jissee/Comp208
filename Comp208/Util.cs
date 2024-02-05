using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp208
{
    internal static class Util
    {
        internal static Icon MakeIcon(Bitmap bitmap)
        {
            IntPtr ptr = bitmap.GetHicon();
            return Icon.FromHandle(ptr);
        }
    }
}
