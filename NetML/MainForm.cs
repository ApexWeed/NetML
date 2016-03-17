using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Apex.Extensions;

namespace NetML
{
    public partial class MainForm : Form
    {
        private enum CanvasState
        {
            None,
            Create,
            Move,
            Link,
            Select
        }

        private CanvasState State;
        private DateTime ClickTime;
        private Point ClickPosition;
        private IDrawable ClickItem;

        public MainForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //var image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //var g = Graphics.FromImage(image);
            //var pen1 = new Pen(Color.Black, 20);
            //g.DrawCurve(pen1, new PointF[] { new PointF(50, pictureBox1.Height / 2), new PointF(e.X, e.Y), new PointF(pictureBox1.Width - 50, pictureBox1.Height / 2) });
            //var pen2 = new Pen(Color.White, 10);
            //g.DrawCurve(pen2, new PointF[] { new PointF(50, pictureBox1.Height / 2), new PointF(e.X, e.Y), new PointF(pictureBox1.Width - 50, pictureBox1.Height / 2) });
            //var oldPic = pictureBox1.Image;
            //pictureBox1.Image = image;
            //pen1.Dispose();
            //pen2.Dispose();
            //g.Dispose();
            //if (oldPic != null)
            //{
            //    oldPic.Dispose();
            //}
        }

        private void canNetwork_MouseDown(object sender, MouseEventArgs e)
        {
            ClickTime = DateTime.Now;
            ClickItem = canNetwork.GetItem(e.X, e.Y);
            ClickPosition = new Point(e.X, e.Y);

            if (ClickItem == null)
            {
                State = CanvasState.Create;
            }
            else
            {
                State = CanvasState.None;
            }

            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        break;
                    }
                case MouseButtons.None:
                    {
                        break;
                    }
                case MouseButtons.Right:
                    {
                        break;
                    }
                case MouseButtons.Middle:
                    {
                        break;
                    }
                case MouseButtons.XButton1:
                    {
                        break;
                    }
                case MouseButtons.XButton2:
                    {
                        break;
                    }
            }
        }

        private void canNetwork_MouseMove(object sender, MouseEventArgs e)
        {
            if (Math.Abs(e.X - ClickPosition.X) + Math.Abs(e.Y - ClickPosition.Y) > 5)
            {
                if (ClickItem != null)
                {
                    switch (State)
                    {
                        case CanvasState.Move:
                        case CanvasState.None:
                            {
                                State = CanvasState.Move;
                                if (ClickItem is Node)
                                {
                                    var node = ClickItem as Node;
                                    var startRect = node.Bounds();
                                    node.X = e.X;
                                    node.Y = e.Y;
                                    var endRect = node.Bounds();
                                    // Redraw changed region.
                                    canNetwork.Invalidate(startRect.Merge(endRect));
                                }
                                break;
                            }
                        case CanvasState.Create:
                            {
                                break;
                            }
                        case CanvasState.Link:
                            {
                                break;
                            }
                        case CanvasState.Select:
                            {
                                break;
                            }
                    }
                }
            }
        }

        private void canNetwork_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        switch (State)
                        {
                            case CanvasState.None:
                                {
                                    break;
                                }
                            case CanvasState.Create:
                                {
                                    var node = new Node
                                    {
                                        X = e.X,
                                        Y = e.Y,
                                        Name = $"{e.X}, {e.Y}",
                                        Type = Node.NodeType.Node
                                    };

                                    canNetwork.AddItem(node);
                                    break;
                                }
                            case CanvasState.Move:
                                {
                                    State = CanvasState.None;
                                    ClickItem = null;
                                    break;
                                }
                            case CanvasState.Link:
                                {
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case MouseButtons.None:
                    {
                        break;
                    }
                case MouseButtons.Right:
                    {
                        break;
                    }
                case MouseButtons.Middle:
                    {
                        break;
                    }
                case MouseButtons.XButton1:
                    {
                        break;
                    }
                case MouseButtons.XButton2:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
