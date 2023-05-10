using ATE.GraphicsFramework;
using System.Windows;
using System.Windows.Media;

namespace ATE.GraphicsFramework.GraphicsBaseItems
{
    public class GraphicsLineItem : GraphicsItem
    {
        protected internal override void OnRender(DrawingContext drawingContext)
        {
            Pen pen = new Pen(Brushes.AliceBlue, 1);
            Brush brush = Brushes.Black;
            drawingContext.DrawLine(pen, new Point(0, 0), new Point(100, 100));
            base.OnRender(drawingContext);
        }
    }
}
