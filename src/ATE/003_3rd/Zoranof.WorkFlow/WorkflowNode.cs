using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Zoranof.GraphicsFramework;
using Zoranof.WorkFlow.Common;

namespace Zoranof.WorkFlow
{
    public class WorkflowNode : GraphicsItem
    {


        public WorkflowNode()
        {
            Width = 200;
            Height = 300;
            Pos = new Point(0, 0);

            FromOptions = new();
            ToOptions = new();
            InputOptions = new();   
            OutPutOptions = new();
        }

        #region Fields
        public string Title { get; set; }

        public char Icon { get; set; }
        
        public string Description { get; set; }

        public List<ExecNodeOption> FromOptions { get; set; }

        public List<ExecNodeOption> ToOptions { get; set; }

        public List<ExecNodeOption> InputOptions { get; set; }

        public List<ExecNodeOption> OutPutOptions { get; set; }
        #endregion

        #region Events
        protected override void OnDrawContent(DrawingContext drawingContext)
        {
            //Brush borderBrush = IsSelected ?
            //    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4E89FE"))
            //    : Brushes.Transparent;

            //drawingContext.DrawRoundedRectangle(
            //    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D3E5F4")),
            //    new Pen(borderBrush, 2),
            //    SelfRect,
            //    16, 16);

            //drawingContext.DrawRoundedRectangle(
            //    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF")),
            //    new Pen(Brushes.Transparent, 2),
            //    new Rect(new Point(SelfRect.Left + 2, SelfRect.Top + 32), new Point(SelfRect.Right - 2, SelfRect.Bottom - 2)),
            //    16, 16);

            //// title
            //FormattedText formattedText = new FormattedText(
            //        "输入对地测试",
            //        CultureInfo.CurrentCulture,
            //        FlowDirection.LeftToRight,
            //        new Typeface("LXGW WenKai"),
            //        12,
            //        new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555")),
            //        VisualTreeHelper.GetDpi(Application.Current.MainWindow).PixelsPerDip);

            //drawingContext.DrawText(formattedText, new Point(24, 10));
        }
        
        
        #endregion

    }
}
