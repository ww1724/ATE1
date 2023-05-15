using System.Windows;
using System.Windows.Media;
using Zoranof.GraphicsFramework.FlowChart;

namespace Zoranof.GraphicsFramework.Nodes
{
    public class StartNode : FlowNode
    {


        public StartNode()
        {
            Height = 25;
            Width = 40;
            Pos = new Point(0, 0);
            BoundingRect = new Rect(Pos.X, Pos.Y, Width, Height);

        }

        protected internal override void OnDrawFramework(DrawingContext drawingContext)
        {
            //drawingContext.PushTransform(new TranslateTransform(Pos.X, Pos.Y));

            var borderRect = GetBoundingRect();
            Pen borderPen = new Pen(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1B5664")), 3);
            Brush bgBrush = IsSelected ? Brushes.Red : Brushes.Transparent;
            drawingContext.DrawRoundedRectangle(
                bgBrush,
                borderPen,
                borderRect,
                10, 10);
            //drawingContext.Pop();
            //base.OnDrawFramework(drawingContext);
        }

        protected internal override void OnDrawContent(System.Windows.Media.DrawingContext drawingContext)
        {
            //base.OnDrawContent(drawingContext);
            //var rect = new Rect(0, 0, Width, Height);
            //var pen = new Pen(Brushes.Black, 1);
            //drawingContext.DrawRectangle(Brushes.White, pen, rect);
            //drawingContext.DrawText(new System.Windows.Media.FormattedText("Start", System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new System.Windows.Media.Typeface("Arial"), 12, System.Windows.Media.Brushes.Black), new System.Windows.Point(0, 0));
        }


    }
}
