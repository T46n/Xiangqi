using System.CodeDom.Compiler;
using System.Windows.Forms;
using static Xiangqi.ChessTable;
using static Xiangqi.GameManager;

namespace Xiangqi
{
    public partial class MatchGame : Form
    {
        private bool isClicked = false;

        private GameManager manager;

        public Rectangle chessLocation;

        public readonly Bitmap banCo = new Bitmap(Xiangqi.Properties.Resources.JanggiBrown);

        private ChessTable chessTable;

        //tạo "bàn cờ" để xuất hiện hình tròn highlight quân cờ
        private int[] stackLocation;
        public MatchGame()
        {
            InitializeComponent();

            chessLocation.Width = 451;
            chessLocation.Height = 501;
            chessLocation.Location = new Point(225, 50);

            manager = new GameManager(this);
            chessTable = new ChessTable();
            chessTable.CreateTable();

            label3.Text = isClicked.ToString();

            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            stackLocation = new int[2]; //x, y ảo, lưu trữ tọa độ sau khi click (nếu isClicked = true)
            UpdateStack(stackLocation, 0, 0);
        }

        //hàm cập nhật lưu trữ tọa độ đã click
        public void UpdateStack(int[] stack, int locX, int locY)
        {
            stack[0] = locX;
            stack[1] = locY;
        }

        private void MatchGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(banCo, chessLocation);
            manager.PlaceDefaultChessItems(e);
        }

        private void testRestart_Click(object sender, EventArgs e)
        {
            chessTable.CreateTable();
            UpdateStack(stackLocation, 0, 0);
            isClicked = false;
            Invalidate();
        }





        private Point FORM_TO_IMG(int form_locX,int form_locY)
        {
            Point temp=new Point();
            temp.X = (form_locX + 419 / 16 - 253) * 8 / 399;
            temp.Y = ((form_locY + 469 / 18 - 527) * 9 / 499)+9;
            return temp;
        }


        private void MatchGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                int[,] result;
                result = chessTable.CellIsClicked(e);
                if (result[0, 0] == -1)
                {
                    //xóa bỏ hình tròn cũ tại tọa độ stackLocation (nếu có)
                    if (isClicked == true)
                    {
                        CleanAt(stackLocation[1], stackLocation[0]);
                        CircleTable[stackLocation[0], stackLocation[1]] = new EmptyLocation(stackLocation[1], stackLocation[0], 0, -1);
                        CircleTable[stackLocation[0], stackLocation[1]].PaintG(this.CreateGraphics());
                        CleanAt(stackLocation[1], stackLocation[0]);

                    }

                    isClicked = false;
                    label3.Text = isClicked.ToString();
                    label4.Text = "Canceled";

                    UpdateStack(stackLocation, 0, 0);
                    return;
                }
                else
                {
                    label1.Text = ("You clicked on grid (" + result[0, 1] + ", " + result[0, 0] + ").");
                    if (manager.CheckAvailable(result[0, 1], result[0, 0]) == true)
                    {
                        //xóa bỏ hình tròn cũ tại tọa độ stackLocation (nếu có)
                        if (isClicked == true)
                        {
                            CleanAt(stackLocation[1], stackLocation[0]);
                            CircleTable[stackLocation[0], stackLocation[1]] = new EmptyLocation(stackLocation[1], stackLocation[0], 0, -1);
                            CircleTable[stackLocation[0], stackLocation[1]].PaintG(this.CreateGraphics());
                        }

                        //khởi tạo hình tròn mới nếu có quân cờ tại tọa độ click
                        CircleTable[result[0, 0], result[0, 1]] = new HighlightCircle(result[0, 1], result[0, 0], 0, -1);
                        CircleTable[result[0, 0], result[0, 1]].PaintG(this.CreateGraphics());

                        UpdateStack(stackLocation, result[0, 0], result[0, 1]);

                        label2.Text = "Available";
                        isClicked = true;
                    }
                    else
                    {
                        if (isClicked == true)
                        {
                            CleanAt(stackLocation[1], stackLocation[0]);
                            //xóa bỏ hình tròn cũ tại tọa độ stackLocation (nếu có)
                            CircleTable[stackLocation[0], stackLocation[1]] = new EmptyLocation(stackLocation[1], stackLocation[0], 0, -1);
                            CircleTable[stackLocation[0], stackLocation[1]].PaintG(this.CreateGraphics());
                        }

                        label2.Text = "NOT Available";
                        isClicked = false;
                    }
                    chessTable.UpdateCellPosition(label4, result[0, 1], result[0, 0]);

                    label3.Text = isClicked.ToString();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                //xóa bỏ hình tròn cũ tại tọa độ stackLocation (nếu có)
                if (isClicked == true)
                {
                    CleanAt(stackLocation[1], stackLocation[0]);
                    CircleTable[stackLocation[0], stackLocation[1]] = new EmptyLocation(stackLocation[1], stackLocation[0], 0, -1);
                    CircleTable[stackLocation[0], stackLocation[1]].PaintG(this.CreateGraphics());
                }

                isClicked = false;
                label3.Text = isClicked.ToString();
                label4.Text = "Canceled";
            }
        }






        //Mỗi khi this.Invalidate() sẽ gọi đến OnPaint() in lại toàn bộ bàn cờ gồm cả con cờ sau khi hoàn thành 1 bước đi
        //Sau mỗi nước đi chỉ cần Teleport sẽ paint lại bàn cờ ( vì trong Teleport có this.Invalidate());
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    manager.PaintBoard(e);
        //    manager.PaintPawn(e);
        //}



        //////////////////////////////////////////////////////////////////
        ////////////////////DI CHUYỂN QUÂN TRONG BÀN CỜ///////////////////
        /////////////////////////////////////////////////////////////////

        //Dọn 1 chỗ trống đc paint từ trước để tránh chỗ mới là emptypawn, tui ứng dụng vô phần ông Việt fix bữa ( line 90, 113 )
        private void CleanAt(int img_locX, int img_locY)
        {
            Point temp = new Point();
            temp = ChessItem.IMG_TO_FORM(img_locX, img_locY);
            this.Invalidate(new Rectangle(temp.X, temp.Y, 50, 50));
        }


        //Di chuyển quân từ (startX,startY) sang (endX,endY) và cho quân (startX,startY) thành EmptyPawn
        public void Teleport(int startX, int startY, int endX, int endY)
        {
            //Cach 1 - ok:
            GameBoard[endY, endX] = GameBoard[startY, startX].Copy();
            GameBoard[endY, endX].img_locX = endX;
            GameBoard[endY, endX].img_locY = endY;
            GameBoard[endY, endX].IMG_FORM();
            GameBoard[endY, endX].PaintG(this.CreateGraphics());

            CleanAt(startX, startY);
            GameBoard[startY, startX] = new EmptyLocation(startX, startY, 0, -1);
            GameBoard[startY, startX].IMG_FORM();
            GameBoard[startY, startX].PaintG(this.CreateGraphics());

            //GameBoard[startY, startX] = new EmptyLocation(startX, startY, 0, -1);
            //Repaint(GameBoard[startY, startX]);

            //Cach 2 - ko ok:
            //ChessItem temp = GameBoard[startY, startX].Copy();
            //GameBoard[endY, endX] = temp;
            //GameBoard[startY, startX] = new EmptyLocation(startX, startY, 0, -1);
            //Repaint(GameBoard[endY, endX]);
            //Repaint(GameBoard[startY, startX]);

            //Repaint(GameBoard[endY, endX]);  
            //Repaint(GameBoard[startY, startX]);  
            //this.Invalidate(); // Repaint nguyên bàn cờ và con cờ nhờ Onpaint(e);
        }


        //Di chuyển con tướng đc click đến vị trí bất kì trên bàn cờ (chưa CheckRule)
        //Có sử dụng phần CellIsClicked của ông Đoàn

        private ChessItem selectedPiece = null;
        private Point initialClick;
        private void LeftMouseClick(MouseEventArgs e)
        {
            int[,] result;
            result = chessTable.CellIsClicked(e);
            Point clicked_loc = new Point();

            result = chessTable.CellIsClicked(e);
            if (result[0, 0] != -1)
            {
                clicked_loc.X = result[0, 1];
                clicked_loc.Y = result[0, 0];

                if (selectedPiece == null)
                {
                    ChessItem clickedPiece = GameBoard[clicked_loc.Y, clicked_loc.X];
                    if (clickedPiece != null && clickedPiece.type != 0)
                    {
                        selectedPiece = clickedPiece;
                        initialClick = clicked_loc;
                    }
                }
                else
                {
                    Point destinationClick = clicked_loc;
                    //CHECK RULE: IF TRUE => TELEPORT
                    Teleport(initialClick.X, initialClick.Y, destinationClick.X, destinationClick.Y);
                    selectedPiece = null;
                }
            }

            label5.Text = clicked_loc.Y.ToString();

        }

        //Thêm LeftMouseClick vào sự kiện OnMouseClick bằng cách override
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            LeftMouseClick(e);
        }


        //Test chuyển 0,0 thành 6,0
        private void button2_Click(object sender, EventArgs e)
        {
            Teleport(0,0,6,0);
        }

    }
}
