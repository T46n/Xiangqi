using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Xiangqi.ChessTable;

namespace Xiangqi
{
    public class ChessItem
    {
                //INT SIDE : BLACK = 0  ;  RED = 1
                //INT TYPE
        public int img_locX {  get; set; }
        public int img_locY { get; set; }
        public int type { get; set; }
        public int side { get; set; }
        public int form_locX { get; set; }
        public int form_locY { get; set; }
        public Bitmap bitmap {  get; set; }

        public ChessItem(int img_locX, int img_locY, int type, int side)
        {
            this.type = type;
            this.side = side;
            this.img_locX = img_locX;
            this.img_locY = img_locY;
            this.form_locX = (int)(253 + (img_locX) * 399 / 8 - 419 / 16);
            this.form_locY = (int)(527 - (9 - img_locY) * 449 / 9 - 469 / 18);
        }

        public static Point IMG_TO_FORM(int img_locX, int img_locY)
        {
            Point temp=new Point();
            temp.X =(int)(253 + (img_locX) * 399 / 8 - 419 / 16);
            temp.Y = (int)(527 - (9-img_locY) * 449 / 9 - 469 / 18);
            return temp;
        }

        public void IMG_FORM()
        {
            this.form_locX = (int)(253 + (img_locX) * 399 / 8 - 419 / 16);
            this.form_locY = (int)(527 - (9 - img_locY) * 449 / 9 - 469 / 18);
        }

        public bool CheckAvailable(int x, int y)
        {
            if (img_locX == x && img_locY == y) return true;
            return false;
        }

        public void Paint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(bitmap,form_locX,form_locY, 50, 50);
        }
            
        public void PaintG(Graphics g)
        {
            g.DrawImage(bitmap,form_locX, form_locY, 50, 50);
        }
  

        public ChessItem(ChessItem x)
        {
            this.type = x.type;
            this.side = x.side;
            this.img_locX = x.img_locX;
            this.img_locY = x.img_locY;
            this.form_locX = x.form_locX;
            this.form_locY= x.form_locY;
            this.bitmap= x.bitmap;
        }


        public virtual ChessItem Copy()
        {
            return new ChessItem(this);
        }

        //public ChessItem Copy()
        //{
        //    if(side == 0)
        //    {
        //        switch (type)
        //        {
        //            case 1:
        //                return new Advisor(this.img_locX, this.img_locY, 1, 0);
        //            case 2:
        //                return new Cannon(this.img_locX, this.img_locY, 2, 0);
        //            case 3:
        //                return new Elephant(this.img_locX, this.img_locY, 3, 0);
        //            case 4:
        //                return new General(this.img_locX, this.img_locY, 4, 0);
        //            case 5:
        //                return new Horse(this.img_locX, this.img_locY, 4, 0);
        //            case 6:
        //                return new Rook(this.img_locX, this.img_locY, 4, 0);
        //            case 7:
        //                return new Soldier(this.img_locX, this.img_locY, 4, 0);

        //            default:
        //                return new Rook(this.img_locX, this.img_locY, 4, 0); ;
        //        }
        //    }
        //    if (side == 1)
        //    {
        //        switch (type)
        //        {
        //            case 1:
        //                return new Advisor(this.img_locX, this.img_locY, 1, 1);
        //            case 2:
        //                return new Cannon(this.img_locX, this.img_locY, 2, 1);
        //            case 3:
        //                return new Elephant(this.img_locX, this.img_locY, 3, 1);
        //            case 4:
        //                return new General(this.img_locX, this.img_locY, 4, 1);
        //            case 5:
        //                return new Horse(this.img_locX, this.img_locY, 4, 1);
        //            case 6:
        //                return new Rook(this.img_locX, this.img_locY, 4, 1);
        //            case 7:
        //                return new Soldier(this.img_locX, this.img_locY, 4, 1);

        //            default:
        //                return new Rook(this.img_locX, this.img_locY, 4, 0); ;
        //        }
        //    }
        //    else return new Rook(this.img_locX, this.img_locY, 4, 0); ;
        //}

    }
}
