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
        private double scalage;


        public GraphicsView()
        {
            Scalage = 1;
            ClipToBounds = true;
        }

        #region Fields

        public bool IsDragging { get; set; }

        public Alignment Alignment { get => alignment; set => alignment = value; }

        public double Scalage { get => scalage; set => scalage = value; }
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
                    null,
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
            DependencyProperty.Register("Title", typeof(string), typeof(GraphicsView), new FrameworkPropertyMetadata("你好", FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion

        #region Events
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (Items != null)
                foreach (var item in Items)
                    item.AttachedView ??= this;
            // 容器框架绘制

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

            // 装饰性元件
            drawingContext.PushTransform(new ScaleTransform(Scalage, Scalage));

            // 绘制节点
            int index = 0;
            if (Items == null) return;
            foreach (var item in Items)
            {
                index++;
                //drawingContext.PushTransform(new TranslateTransform(item.Pos.X, item.Pos.Y));
                item.OnRender(drawingContext);
                //drawingContext.Pop();
            }
            // 绘制连线  
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {

            base.OnRenderSizeChanged(sizeInfo);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            IsDragging = true;
            e.GetPosition(this);

            foreach (var item in Items)
            {
                if (item.BoundingRect.Contains(e.GetPosition(this)))
                {
                    item.OnMouseDown(e);
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            foreach (var item in Items)
            {
                item.OnMouseUp(e);
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            this.InvalidateVisual();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            this.InvalidateVisual();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Console.WriteLine("Moving");
            InvalidateVisual();
            if (e.LeftButton == MouseButtonState.Released)
            {
                foreach (var item in this.Items)
                {
                    Console.WriteLine(item.BoundingRect.Contains(e.GetPosition(this)).ToString());
                    if (item.BoundingRect.Contains(e.GetPosition(this)))
                    {
                        if (!item.IsMouseCaptured)
                        {
                            item.OnMouseEnter(e);
                        }
                        Console.WriteLine("Child Move");
                        item.OnMouseMove(e);

                    }
                    else
                    {
                        if (item.IsMouseCaptured)
                        {
                            item.OnMouseExit(e);
                        }
                    }
                }
            } else
            {
                foreach (var item in this.Items)
                {
                    Console.WriteLine(item.BoundingRect.Contains(e.GetPosition(this)).ToString());
                    if (item.BoundingRect.Contains(e.GetPosition(this)))
                    {
                        if (!item.IsMouseCaptured)
                        {
                            item.OnMouseEnter(e);
                        }
                        Console.WriteLine("Child Move");
                        item.OnMouseMove(e);

                    }
                }
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                double zoom = e.Delta > 0 ? 1.1 : 0.9;
                Scalage = zoom * Scalage;
                this.InvalidateVisual();
            }



        }

        /// <summary>
        /// 
        /// </summary>
        public EventHandler ScaleEventHandler;



        #endregion

    }
}
