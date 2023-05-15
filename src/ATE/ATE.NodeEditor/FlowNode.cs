using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Zoranof.GraphicsFramework
{
    public class FlowNode : GraphicsItem
    {
        public FlowNode()
        {
            Height = 320;
            Width = 120;
        }


        #region Paint
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal override void OnRender(DrawingContext drawingContext)
        {

            base.OnRender(drawingContext);

            OnDrawFramework(drawingContext);

            OnDrawTitle(drawingContext);

            OnDrawContent(drawingContext);

            OnDrawConnectDot(drawingContext);

            OnDrawConnectLine(drawingContext);

            OnDrawMark(drawingContext);

            OnDrawResizeBorder(drawingContext);
        }


        /// <summary>
        /// 绘制框架 背景+边框
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawFramework(DrawingContext drawingContext) { }

        /// <summary>
        /// 绘制标题
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawTitle(DrawingContext drawingContext) { }

        /// <summary>
        /// 绘制内容
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawContent(DrawingContext drawingContext) { }

        /// <summary>
        /// 绘制连接点
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawConnectDot(DrawingContext drawingContext)
        {
            drawingContext.PushTransform(new TranslateTransform(Pos.X, Pos.Y));
            Pen dotPen;
            Brush bg;
            ICollection<Point> points = new Collection<Point>()
                {
                    new Point(Width / 2, 0),
                    new Point(0, Height / 2),
                    new Point(Width, Height / 2),
                    new Point(Width / 2, Height)
                };
            if (IsHovered) {
                dotPen = new Pen(Brushes.Black, 1);
                bg = Brushes.White;
            } else {
                dotPen = new Pen(Brushes.Transparent, 1);
                bg = Brushes.Transparent;
            }

            foreach (Point point in points)
            {
                drawingContext.DrawEllipse(bg, dotPen, point, 3, 3);
            }
            drawingContext.Pop();
        }

        protected virtual void OnDrawResizeBorder(DrawingContext drawingContext) 
        {
            ICollection<Rect> rects = new Collection<Rect>()
            {
                new Rect(0 - 3, 0 - 3, 6, 6),
                new Rect(Width / 2 - 3, 0 - 3, 6, 6),
                new Rect(Width -3, 0 - 3, 6, 6),
                new Rect(0 - 3, Height / 2 - 3, 6, 6),
                //new Rect(Width / 2 - 3, Height / 2 - 3, 6, 6),
                new Rect(Width -3, Height / 2 - 3, 6, 6),
                new Rect(0 - 3, Height - 3, 6, 6),
                new Rect(Width / 2 - 3, Height - 3, 6, 6),
                new Rect(Width -3, Height - 3, 6, 6)
            };
            if (IsActive)
            {
                drawingContext.PushTransform(new TranslateTransform(Pos.X, Pos.Y));
                foreach (Rect rect in rects)
                {
                    drawingContext.DrawRectangle(Brushes.White, new Pen(Brushes.CadetBlue, 1), rect);
                }
                drawingContext.Pop();
            }
        
        }

        /// <summary>
        /// 绘制连接线
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawConnectLine(DrawingContext drawingContext)
        {

        }

        /// <summary>
        /// 绘制标记
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawMark(DrawingContext drawingContext)
        {

        }


        #endregion

    }
}
