using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiangqi
{
    public class Elephant : ChessItem
    {
        public Elephant(int locX, int locY, int type, int side) : base(locX, locY, type, side)
        {
            if (side == 0) bitmap = new Bitmap(Xiangqi.Properties.Resources.black_bishop);
            else bitmap = new Bitmap(Xiangqi.Properties.Resources.red_bishop);
        }

        public Elephant(Elephant x) : base(x.img_locX, x.img_locY, x.type, x.side)
        {
            this.bitmap = x.bitmap;
        }

        public override ChessItem Copy()
        {
            //return new Elephant(this.img_locX, this.img_locY, this.type, this.side);

            ChessItem copy = new Elephant(this.img_locX, this.img_locY, this.type, this.side);
            copy.bitmap = this.bitmap;
            return copy;

        }

    }
}
