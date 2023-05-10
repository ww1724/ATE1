using ATE.GraphicsFramework.Enums;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ATE.GraphicsFramework
{
    public class GraphicsView : Control
    {
        private Alignment alignment;

        static GraphicsView()
        {
            IsHitTestVisibleProperty.OverrideMetadata(typeof(GraphicsView), new FrameworkPropertyMetadata(true));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphicsView), new FrameworkPropertyMetadata(typeof(GraphicsView)));
        
        }


        #region Events
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            var size = this.RenderSize;
            drawingContext.DrawRectangle(Background, new Pen(Brushes.Blue, 2), new Rect(new Point(0, 0), size));


            // 绘制框架

            // 绘制网格

            // 绘制节点
            int index = 0;
            foreach (var item in Items)
            {
                index++;
                drawingContext.PushTransform(new TranslateTransform(index * 100, 0));
                item.OnRender(drawingContext);
                drawingContext.Pop();
            }
            // 绘制连线  
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            
            base.OnRenderSizeChanged(sizeInfo);
        }
        #endregion

        #region Fields
        public Alignment Alignment { get => alignment; set => alignment = value; }
        #endregion


        #region Property
        public IEnumerable<GraphicsItem> Items
        {
            get { return (IEnumerable<GraphicsItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); onItemsPropertyChanged(); }
        }
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", 
                typeof(IEnumerable<GraphicsItem>), typeof(GraphicsView), 
                new FrameworkPropertyMetadata(
                    new List<GraphicsItem>(),
                   onItemsPropertyChanged));

        private static void onItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Control).InvalidateVisual(); ;
        }

        private void onItemsPropertyChanged()
        {
            this.InvalidateVisual();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.InvalidateVisual();
            base.OnMouseMove(e);
        }
        #endregion



    }
}
