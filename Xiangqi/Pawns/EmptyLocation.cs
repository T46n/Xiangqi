using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiangqi
{
    public class EmptyLocation:ChessItem
    {
        public EmptyLocation(int locX, int locY, int type, int side) : base(locX, locY, type, side)
        {
            bitmap = new Bitmap(Xiangqi.Properties.Resources.empty_Item);
        }

        //public override ChessItem Copy()
        //{
        //    return new EmptyLocation(this.img_locX, this.img_locY, this.type, this.side);
        //}

    }
}
