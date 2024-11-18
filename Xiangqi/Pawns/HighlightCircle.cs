using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiangqi
{
    public class HighlightCircle:ChessItem
    {
        public HighlightCircle(int locX, int locY, int type, int side) : base(locX, locY, type, side)
        {
            bitmap = new Bitmap(Xiangqi.Properties.Resources.chosen_circle);
        }
    }
}
