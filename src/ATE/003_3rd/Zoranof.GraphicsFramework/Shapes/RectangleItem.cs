


using System.Windows.Media;

namespace Zoranof.GraphicsFramework.Shapes
{
    public class RectangleItem : GraphicsItem
    {
        public RectangleItem()
        {
            Width = 150;
            Height = 50;
            LineColor = Brushes.Black;
        }

        public Brush LineColor { get; set; }

        public double LineWidth { get; set; }

        protected internal override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(Background, new Pen(LineColor, LineWidth), new System.Windows.Rect(Pos.X, Pos.Y, Width, Height));
        }
    }
}
