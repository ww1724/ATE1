using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Zoranof.GraphicsFramework.Common;

namespace Zoranof.GraphicsFramework
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

        #region Variables
        private bool m_isAnySelected;
        //private bool m_isReadyToMoveItems;

        private bool m_isReadyToBoxSelect;
        private Point m_boxSelectStartPoint;
        private Point m_boxSelectEndPoint;

        private bool m_isReadyToMoveViewer;
        private Point m_viewerMoveStartPoint;
        private Point m_viewerMoveEndPoint;

        private bool m_isReadyToMoveSelectedItems;
        private Point m_moveItemsStartPoint;
        private Point m_moveItemsEndPoint;
        #endregion

        #region General Slots
        /// <summary>
        /// 选择给定元素  绝对式
        /// </summary>
        /// <param name="items"></param>
        protected virtual void SelectItemsAS(ICollection<GraphicsItem> items)
        {
            foreach (GraphicsItem item in Items)
            {
                item.IsSelected = false;
            }

            if (items.Count == 0)
                return;

            foreach (GraphicsItem item in items)
            {
                item.IsSelected = true;
            }

            OnSelectedChanged(new GraphicsViewEventArgs(items.FirstOrDefault()));
            InvalidateVisual();
        }

        /// <summary>
        /// 选择给定元素  增量式
        /// </summary>
        /// <param name="items"></param>
        protected virtual void SelectItemsIS(ICollection<GraphicsItem> items)
        {

            if (items.Count == 0)
                return;

            foreach (GraphicsItem item in items)
            {
                item.IsSelected = true;
            }

            OnSelectedChanged(new GraphicsViewEventArgs(items.FirstOrDefault()));
            InvalidateVisual();
        }

        /// <summary>
        /// 选择全部元素
        /// </summary>
        protected virtual void SelectAllItems()
        {
            foreach (GraphicsItem item in Items)
            {
                item.IsSelected = true;
            }
            InvalidateVisual();
        }

        /// <summary>
        /// 取消选择全部元素
        /// </summary>
        protected virtual void UnSeselectAllItems()
        {
            foreach (GraphicsItem item in Items)
            {
                item.IsSelected = false;
            }
            InvalidateVisual();
        }

        /// <summary> 框选动作 </summary>
        /// <description> 取消之前的选中, 选中当前框选 </description>
        /// <param name="selectedBoxRect">当前选框的rect</param>
        protected virtual void ToBoxSelect(Rect selectedBoxRect)
        {

            m_isAnySelected = false;
            ICollection<GraphicsItem> toSelectItems = new Collection<GraphicsItem>();
            foreach (var item in Items)
            {
                if (item.BoundingRect.IntersectsWith(selectedBoxRect))
                    toSelectItems.Add(item);
            }

            if (toSelectItems.Count > 0)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    SelectItemsIS(toSelectItems);
                }
                else
                {
                    SelectItemsAS(toSelectItems);

                }
                m_isAnySelected = true;
            }
            InvalidateVisual();
        }

        /// <summary>
        /// 移动选择元素
        /// </summary>
        /// <param name="items"></param>
        /// <param name="target"></param>
        protected virtual void MoveSelectedItemsTo(Point target)
        {
            ICollection<GraphicsItem> toMoveItems = (ICollection<GraphicsItem>)Items.Select(x => x.IsSelected == true).ToList();
            MoveItemsTo(toMoveItems, target);
        }

        protected virtual void MoveSelectedItemsWithOffset(Vector offset)
        {
            var toMoveItems = (ICollection<GraphicsItem>)Items.Where(x => x.IsSelected == true).ToList();
            MoveItemsWithOffset(toMoveItems, offset);
        }

        protected virtual void MoveItemsTo(ICollection<GraphicsItem> items, Point target)
        {
            if (items.Count == 0) return;

            foreach (var item in items)
            {
                item.MoveTo(target);
            }

            InvalidateVisual();

        }

        protected virtual void MoveItemsWithOffset(ICollection<GraphicsItem> items, Vector offset)
        {
            Console.WriteLine("Move Items With Offset: " + Convert.ToString(offset.X) + "-" + Convert.ToString(offset.Y));
            if (items.Count == 0) return;
            foreach (var item in items)
            {
                item.MoveWithOssfet(offset);
            }
            InvalidateVisual();
        }
        /// <summary>
        /// 元素坐标转换到视图坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected virtual Point PointMapToViewer(Point point) => point;
        #endregion

        #region Fields
        public bool IsDragging { get; set; }

        public Alignment Alignment { get => alignment; set => alignment = value; }

        public double Scalage { get => scalage; set => scalage = value; }

        public Vector ViewerOffset { get; set; }

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
            set { SetValue(TitleProperty, value); }
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
            var size = RenderSize;
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

            // 绘制选择框
            drawingContext.DrawRectangle(
                new SolidColorBrush() { Opacity = 0.5, Color = Color.FromArgb(100, 0, 0, 0) },
                new Pen(),
                new Rect(m_boxSelectStartPoint, m_boxSelectEndPoint));
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {

            base.OnRenderSizeChanged(sizeInfo);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            ICollection<GraphicsItem> toSelectItems = new Collection<GraphicsItem>();
            foreach (var item in Items)
            {
                if (item.BoundingRect.Contains(e.GetPosition(this))) toSelectItems.Add(item);
            }


            // 左键按下 => 选择元素 || 准备移动元素 || 准备框选元素
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // 按下鼠标时鼠标下有元素 => 选择Items
                if (toSelectItems.Count > 0)
                {

                    if (!toSelectItems.Any(x => x.IsSelected))
                    {
                        // 根据Ctrl按键状态选择元素
                        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                            SelectItemsIS(toSelectItems);
                        else SelectItemsAS(toSelectItems);
                    }



                    // 准备移动元素
                    m_isReadyToMoveSelectedItems = true;
                    m_moveItemsStartPoint = e.GetPosition(this);
                    m_moveItemsEndPoint = e.GetPosition(this);


                }
                else
                {
                    // 按下鼠标时鼠标下没元素 => 取消全部选择 - 开始框选动作
                    if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
                        UnSeselectAllItems();

                    m_isReadyToBoxSelect = true;
                    m_boxSelectStartPoint = e.GetPosition(this);
                    m_boxSelectEndPoint = e.GetPosition(this);
                }
            }

            // 滚轮按下
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                m_isReadyToMoveViewer = true;
                m_viewerMoveStartPoint = e.GetPosition(this);
                m_viewerMoveEndPoint = e.GetPosition(this);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (m_isReadyToBoxSelect)
            {
                m_isReadyToBoxSelect = false;
                m_boxSelectStartPoint = new Point(0, 0);
                m_boxSelectEndPoint = new Point(0, 0);
            }

            m_isReadyToMoveSelectedItems = false;

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
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (m_isReadyToBoxSelect)
            {
                m_isReadyToBoxSelect = false;
                m_boxSelectStartPoint = new Point(0, 0);
                m_boxSelectEndPoint = new Point(0, 0);
                InvalidateVisual();
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            // 框选Items
            if (m_isReadyToBoxSelect)
            {
                m_boxSelectEndPoint = e.GetPosition(this);
                Rect selectedBoxRect = new Rect(m_boxSelectStartPoint, m_boxSelectEndPoint);
                ToBoxSelect(selectedBoxRect);
            }

            // 移动已选择元素
            if (m_isReadyToMoveSelectedItems)
            {
                m_moveItemsEndPoint = e.GetPosition(this);
                MoveSelectedItemsWithOffset(new Vector(
                    m_moveItemsEndPoint.X - m_moveItemsStartPoint.X,
                    m_moveItemsEndPoint.Y - m_moveItemsStartPoint.Y));
                m_moveItemsStartPoint = m_moveItemsEndPoint;
            }

        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                double zoom = e.Delta > 0 ? 1.1 : 0.9;
                Scalage = zoom * Scalage;
                InvalidateVisual();
            }



        }


        #region Custom Events
        public event EventHandler ViewScaled;
        public event EventHandler SelectedChanged;
        public event EventHandler HoveredChanged;
        public event EventHandler ItemAdded;
        public event EventHandler ItemRemoved;
        public event EventHandler ViewerMoved;
        public event EventHandler BoxSelectChanged;

        protected virtual void OnViewScaled(GraphicsViewEventArgs e) => ViewScaled?.Invoke(this, e);

        protected virtual void OnSelectedChanged(GraphicsViewEventArgs e) => SelectedChanged?.Invoke(this, e);

        protected virtual void OnHoveredChanged(GraphicsViewEventArgs e) => HoveredChanged?.Invoke(this, e);

        protected virtual void OnItemAdded(GraphicsViewEventArgs e) => ItemAdded?.Invoke(this, e);

        protected virtual void OnItemRemoved(GraphicsViewEventArgs e) => ItemRemoved?.Invoke(this, e);

        protected virtual void OnViewerMoved(GraphicsViewEventArgs e) => ViewerMoved?.Invoke(this, e);

        protected virtual void OnBoxSelectChanged(GraphicsViewEventArgs e) => BoxSelectChanged?.Invoke(this, e);
        #endregion

        #endregion

    }
}
