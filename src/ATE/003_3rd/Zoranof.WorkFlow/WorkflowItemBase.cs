using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Zoranof.GraphicsFramework;

namespace Zoranof.WorkFlow
{
    public class WorkflowNodeBase : GraphicsItem
    {


        public WorkflowNodeBase()
        {
            Width = 200;
            Height = 300;
            Pos = new Point(0, 0);
            BoundingRect = new Rect(Pos.X, Pos.Y, 300, 400);
            
        }

        #region Events
        protected override void OnDrawBeforeMark(DrawingContext drawingContext)
        {
            //base.OnRender(drawingContext);
            // draw bg

            Brush borderBrush = IsSelected ?
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4E89FE"))
                : Brushes.Transparent;
            drawingContext.DrawRoundedRectangle(
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3E5F4")),
                new Pen(borderBrush, 2),
                BoundingRect,
                16, 16);

            drawingContext.DrawRoundedRectangle(
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF")),
                new Pen(Brushes.Transparent, 2),
                new Rect(new Point(BoundingRect.Left + 2, BoundingRect.Top + 32), new Point(BoundingRect.Right - 2, BoundingRect.Bottom - 2)),
                16, 16);


            // 开始偏移绘制内容
            drawingContext.PushTransform(new TranslateTransform { X = Pos.X, Y = Pos.Y });
            

            // title
            FormattedText formattedText = new FormattedText(
                    "输入对地测试",
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("LXGW WenKai"),
                    12,
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555")),
                    VisualTreeHelper.GetDpi(Application.Current.MainWindow).PixelsPerDip);


            drawingContext.DrawText(formattedText, new Point(24, 10));
            drawingContext.Pop();
        }
        #endregion

    }
}
