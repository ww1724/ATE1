using ATE.GraphicsFramework.Enums;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
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
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphicsView), new FrameworkPropertyMetadata(typeof(GraphicsView)));
            ListBox a;
        }


        #region Events
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (Items == null) return;
            var size = this.RenderSize;
            drawingContext.DrawRectangle(Background, new Pen(Brushes.Blue, 2), new Rect(new Point(0, 0), size));

            if (Title != null)
            {
                double pixelsPerDip = VisualTreeHelper.GetDpi(this).PixelsPerDip;
                drawingContext.DrawText(new FormattedText(
                        Title,                // 要绘制的文本
                        CultureInfo.CurrentCulture,     // 区域信息
                        FlowDirection.LeftToRight,      // 文本方向
                        new Typeface("Arial"),          // 字体类型
                        24 / pixelsPerDip,                             // 字体大小
                        Brushes.Black,                  // 文本颜色
                        pixelsPerDip
                    ), new Point(0, 0));
            }


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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.InvalidateVisual();
            base.OnMouseMove(e);
        }

        #endregion

        #region Fields
        public Alignment Alignment { get => alignment; set => alignment = value; }
        #endregion

        #region Property
        public ObservableCollection<GraphicsItem> Items
        {
            get { return (ObservableCollection<GraphicsItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items",
                typeof(ObservableCollection<GraphicsItem>), 
                typeof(GraphicsView),
                new PropertyMetadata(
                    new ObservableCollection<GraphicsItem>(),
                    OnItemsPropertyChanged));

        private static void OnItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Control).InvalidateVisual(); ;
        }


        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); InvalidateVisual(); }
        }

        public static readonly DependencyProperty TitleProperty =
            //DependencyProperty.Register("Title", typeof(string), typeof(GraphicsView), new FrameworkPropertyMetadata("你好",  FrameworkPropertyMetadataOptions.AffectsRender));
            DependencyProperty.Register("Title", typeof(string), typeof(GraphicsView), new FrameworkPropertyMetadata("你好"));
        #endregion



    }
}
