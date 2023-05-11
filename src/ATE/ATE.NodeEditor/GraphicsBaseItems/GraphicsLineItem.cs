﻿using ATE.GraphicsFramework.DataStructure;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ATE.GraphicsFramework.GraphicsBaseItems
{
    public class GraphicsLineItem : GraphicsItem
    {

        private Point startPoint;

        public Point StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        private Point endPoint = new Point(100, 100);

        public Point EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        private Brush lineColor;

        public Brush LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }


        public GraphicsLineItem(GraphicsItem parent = null)
        {
            Width = 100;
            Height = 100;
            Pos = new Point(0, 0);

            ParentItem = parent;

            LineColor = Brushes.Red;
            BoundingRect = new Rect(Pos.X, Pos.Y, Width, Height);
        }

        //public GraphicsLineItem(Line line, GraphicsItem parent = null)
        //{
        //    Width = 100;
        //    Height = 100;
        //    Pos = new Point(0, 0);

        //    ParentItem = parent;
        //}

        //public GraphicsLineItem(double x1, double y1, double x2, double y2, GraphicsItem parent = null)
        //{
        //    StartPoint= new Point(x1, y1);
        //    EndPoint = new Point(x2, y2);
        //    Width = Math.Abs(x2 - x1);
        //    Height = Math.Abs(y2 - y1) ;

        //    BoundingRect = new Rect(Pos.X, Pos.Y, Width, Height);
        //}

        protected internal override void OnRender(DrawingContext drawingContext)
        {

            // 框架绘制
            drawingContext.DrawRectangle(IsSelected ? Brushes.Aqua : Brushes.White, new Pen(LineColor, 2), BoundingRect);

            drawingContext.PushTransform(new TranslateTransform(Pos.X, Pos.Y));
            drawingContext.DrawLine(new Pen(lineColor, 2), StartPoint, EndPoint);
            drawingContext.Pop();
            base.OnRender(drawingContext);
        }

        protected internal override void OnMouseEnter(MouseEventArgs args)
        {
            
            lineColor = Brushes.Black;
            Update();
            base.OnMouseEnter(args);
        }

        protected internal override void OnMouseExit(MouseEventArgs args)
        {
            

            lineColor = Brushes.Red;
            Update();
            base.OnMouseExit(args);
        }
    }
}
