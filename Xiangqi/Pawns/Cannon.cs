using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiangqi
{
    public class Cannon : ChessItem
    {
        public Cannon(int locX, int locY, int type, int side):base(locX,locY,type,side) 
        {
            if (side == 0) bitmap = new Bitmap(Xiangqi.Properties.Resources.black_cannon);
            else bitmap = new Bitmap(Xiangqi.Properties.Resources.red_cannon);
        }

        public Cannon(Cannon x) : base(x.img_locX, x.img_locY, x.type, x.side)
        {
            this.bitmap = x.bitmap;
        }

        public override ChessItem Copy()
        {
            //return new Cannon(this.img_locX, this.img_locY, this.type, this.side);

            ChessItem copy = new Cannon(this.img_locX, this.img_locY, this.type, this.side);
            copy.bitmap = this.bitmap;
            return copy;
        }

    }
}
