using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Zoranof.GraphicsFramework.Common;

namespace Zoranof.GraphicsFramework
{
    public class GraphicsView : Control
    {

        public GraphicsView()
        {
            ViewerScale = 1;
            ClipToBounds = true;
            ViewerLocation = new Point(0, 0);
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FEFCFB"));

            ActiveItem = null;
        }



        #region Fields
        public bool IsDragging { get; set; }

        public Alignment Alignment { get; set; }

        public double ViewerScale { get; set; }

        public Point ViewerLocation { get; set; }
        #endregion

        #region Variables
        private GraphicsItem ActiveItem { get;set; }

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
        protected virtual void SetHoveredItem(GraphicsItem item)
        {

        }

        protected virtual void DeleteSelectedItems(ICollection<GraphicsItem> items)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (Items[i].IsSelected)
                {
                    Items.RemoveAt(i);
                }
            }
            InvalidateVisual();
        }

        protected virtual void DeleteItems(ICollection<GraphicsItem> items)
        {
            foreach (var item in items)
            {
                Items.Remove(item);
            }
            InvalidateVisual();
        }

        protected virtual void SetActiveItem(GraphicsItem item)
        {
            foreach (var i in Items)
            {
                i.IsActive = false;
            }
            ActiveItem = item;
            item.IsActive = true;
        }

        protected virtual void SelectItemAS(GraphicsItem item)
        {
            foreach (GraphicsItem item1 in Items)
            {
                item1.IsSelected = false;
            }

            item.IsSelected = true;
            InvalidateVisual();
            OnSelectedChanged(new GraphicsViewEventArgs(item));

        }

        protected virtual void SelectItemIS(GraphicsItem item)
        {
            item.IsSelected = true;
            InvalidateVisual();
            OnSelectedChanged(new GraphicsViewEventArgs(item));
        }

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
            ICollection<GraphicsItem> toSelectItems = new Collection<GraphicsItem>();
            foreach (var item in Items)
            {
                Rect itemRectInViewer = RectMapToViewer(item.BoundingRect);

                if (itemRectInViewer.IntersectsWith(selectedBoxRect))
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
            offset = OffsetMapToControl(offset);
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
        /// 物理坐标转换到视图坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected virtual Point PointMapToViewer(Point point) => new Point(
            point.X * ViewerScale + ViewerLocation.X,
            point.Y * ViewerScale + ViewerLocation.Y
            );

        protected virtual Rect RectMapToViewer(Rect rect) => new Rect(
                    rect.X * ViewerScale + ViewerLocation.X,
                    rect.Y * ViewerScale + ViewerLocation.Y,
                    rect.Width * ViewerScale,
                    rect.Height * ViewerScale);

        protected virtual Vector OffsetMapToControl(Vector offset) => new Vector(
                    offset.X / ViewerScale + ViewerLocation.X,
                    offset.Y / ViewerScale + ViewerLocation.Y);

        /// <summary>
        /// 移动画布
        /// </summary>
        /// <param name="point"></param>
        protected virtual void MoveViewerTo(Point point) {
            ViewerLocation = point;
            this.InvalidateVisual();
        }

        /// <summary>
        /// 通过偏移移动画布
        /// </summary>
        /// <param name="offset"></param>
        protected virtual void MoveViewerWithOffset(Vector offset) {
            ViewerLocation = new Point(ViewerLocation.X + offset.X, ViewerLocation.Y + offset.Y);
            Console.WriteLine("Move Viewer With Offset: " + Convert.ToString(offset.X) + "-" + Convert.ToString(offset.Y));
            this.InvalidateVisual();
        }
        
        #endregion

        #region Public Slots
        /// <summary>
        /// 是否碰撞
        /// </summary>
        /// <param name="item"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        protected internal virtual bool IsCollidesWithItem(GraphicsItem a, GraphicsItem b, ItemSelectionMode mode = ItemSelectionMode.IntersectsItemShape) { return true; }

        /// <summary>
        /// 获取所有碰撞Items
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        protected internal virtual IList<GraphicsItem> CollidesItems(GraphicsItem a, ItemSelectionMode mode) { return null; }

        /// <summary>
        /// Item是否包含点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected internal virtual bool IsContainsPoint(GraphicsItem a, Point point) { return false; }

        /// <summary>
        /// 确保Item可见
        /// </summary>
        protected internal virtual void EnsureVisible(GraphicsItem item) { }

        /// <summary>
        /// 确保Items可见
        /// </summary>
        protected internal virtual void EnsureVisible(ICollection<GraphicsItem> items) { }
        #endregion


        #region Propertys
        public FontFamily Font
        {
            get { return (FontFamily)GetValue(FontProperty); }
            set { SetValue(FontProperty, value); }
        }

        public static readonly DependencyProperty FontProperty =
            DependencyProperty.Register(
                "Font", 
                typeof(FontFamily), 
                typeof(GraphicsView), 
                new FrameworkPropertyMetadata(new FontFamily("Simsun"), FrameworkPropertyMetadataOptions.AffectsRender));



        public ObservableCollection<GraphicsItem> Items
        {
            get { return (ObservableCollection<GraphicsItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items",
                typeof(ObservableCollection<GraphicsItem>),
                typeof(GraphicsView),
                new FrameworkPropertyMetadata(null, OnItemsPropertyChanged));

        private static void OnItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = d as GraphicsView;

            if (e.OldValue != null)
            {
                var oldItems = e.OldValue as ObservableCollection<GraphicsItem>;
                oldItems.CollectionChanged -= view.OnItemsCollectionChanged;
            }

            if (e.NewValue != null)
            {
                var newItems = e.NewValue as ObservableCollection<GraphicsItem>;
                newItems.CollectionChanged += view.OnItemsCollectionChanged;
            }

        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach(var item in Items)
            {
                item.AttachedView= this;
            }
            Focus();
            InvalidateVisual();
        }


        #endregion

        #region Events
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // 容器框架绘制
            OnDrawFramework(drawingContext);
  
            // 绘制元素
            OnDrawItems(drawingContext);

            // 各事件绘制
            OnDrawActionIndicator(drawingContext);
            
            // 绘制信息
            OnDrawAlert(drawingContext);    

            // 绘制标志
            OnDrawMask(drawingContext); 
            #endregion
        }

        protected virtual void OnDrawFramework(DrawingContext drawingContext) {

            // 是否获取焦点绘制不同边框
            Pen borderPen = new Pen(
                IsFocused ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CBCFF2")) :Brushes.Transparent,
                3);

            Brush bgBrush = Background;

            drawingContext.DrawRectangle(
                bgBrush,
                borderPen,
                new Rect(new Point(4, 4), new Point(RenderSize.Width - 4, RenderSize.Height - 4)));


        }

        protected virtual void OnDrawItems(DrawingContext drawingContext) {

            if(Items == null) return;
            #region 绘制相对位置元件
            //启动偏移
            drawingContext.PushTransform(new TranslateTransform(ViewerLocation.X, ViewerLocation.Y));
            drawingContext.PushTransform(new ScaleTransform(ViewerScale, ViewerScale));
            // 绘制节点
            foreach (var item in Items.OrderBy(x => x.ZIndex).ToList())
            {
                item.OnRender(drawingContext);
            }
            // 关闭偏移
            drawingContext.Pop();
            drawingContext.Pop();
        }

        protected virtual void OnDrawActionIndicator(DrawingContext drawingContext)
        {
            if (m_isReadyToBoxSelect)
            {
                drawingContext.DrawRectangle(
                    new SolidColorBrush() { Opacity = 0.5, Color = Color.FromArgb(100, 0, 0, 0) },
                    new Pen(),
                    new Rect(m_boxSelectStartPoint, m_boxSelectEndPoint));
            }
        }

        protected virtual void OnDrawAlert(DrawingContext drawingContext) { }

        protected virtual void OnDrawMask(DrawingContext drawingContext) { }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {

            base.OnRenderSizeChanged(sizeInfo);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            Focus();
            ICollection<GraphicsItem> toSelectItems = new Collection<GraphicsItem>();
            foreach (var item in Items)
            {
                if (RectMapToViewer(item.BoundingRect).Contains(e.GetPosition(this)))
                {
                    toSelectItems.Add(item);
                }
            }

            var _activeItem = toSelectItems.OrderByDescending(a => a.ZIndex).FirstOrDefault();


            // 左键按下 => 选择元素 || 准备移动元素 || 准备框选元素
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // 按下鼠标时鼠标下有元素 => 选择Items
                if (toSelectItems.Count > 0)
                {
                    SetActiveItem(_activeItem);
                    if(!_activeItem.IsSelected)
                    {
                        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                            SelectItemIS(_activeItem);
                        else SelectItemAS(_activeItem);
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
                InvalidateVisual();
            }

            m_isReadyToMoveSelectedItems = false;

            if(m_isReadyToMoveViewer)
            {
                m_isReadyToMoveViewer = false;
                m_viewerMoveStartPoint = new Point(0, 0);
                m_viewerMoveEndPoint = new Point(0, 0);
                InvalidateVisual();
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
            else if (m_isReadyToMoveSelectedItems)
            {
                m_moveItemsEndPoint = e.GetPosition(this);
                MoveSelectedItemsWithOffset(new Vector(
                    m_moveItemsEndPoint.X - m_moveItemsStartPoint.X,
                    m_moveItemsEndPoint.Y - m_moveItemsStartPoint.Y));
                m_moveItemsStartPoint = m_moveItemsEndPoint;
            }

            // 移动画布
            else if (m_isReadyToMoveViewer)
            {
                m_viewerMoveEndPoint = e.GetPosition(this);
                MoveViewerWithOffset(new Vector(
                    m_viewerMoveEndPoint.X - m_viewerMoveStartPoint.X,
                    m_viewerMoveEndPoint.Y - m_viewerMoveStartPoint.Y));
                m_viewerMoveStartPoint = m_viewerMoveEndPoint;
            }
        
            else if(e.LeftButton == MouseButtonState.Released)
            {
                var p = e.GetPosition(this);
                ICollection<GraphicsItem> toSelectItems = new Collection<GraphicsItem>();
                foreach (var item in Items)
                {
                    if (item.IsHovered == true) {
                        item.IsHovered = false;
                        InvalidateVisual();
                    }
                    if (RectMapToViewer(item.BoundingRect).Contains(e.GetPosition(this)))
                    {
                        toSelectItems.Add(item);
                    }
                }
                if (toSelectItems.Count > 0)
                {
                    toSelectItems.OrderByDescending(a => a.ZIndex).FirstOrDefault().IsHovered = true;
                    InvalidateVisual();
                }
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                double zoom = e.Delta < 0 ? 1.1 : 0.9;
                ViewerScale = zoom * ViewerScale;
                Console.Write(ViewerScale);
                InvalidateVisual();
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                DeleteSelectedItems(Items);
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {

            if (e.Key == Key.Delete)
            {
                DeleteSelectedItems(Items);
                e.Handled = true;
            }
            else if (e.Key == Key.A && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                SelectAllItems();
            }

            base.OnKeyDown(e);
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
        }

        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);
        }
        #region Custom Events
        public event EventHandler ViewScaled;
        public event EventHandler SelectedChanged;
        public event EventHandler HoveredChanged;
        public event EventHandler ActiveChanged;
        public event EventHandler ItemAdded;
        public event EventHandler ItemRemoved;
        public event EventHandler ViewerMoved;
        public event EventHandler BoxSelectChanged;

        protected virtual void OnViewScaled(GraphicsViewEventArgs e) => ViewScaled?.Invoke(this, e);

        protected virtual void OnSelectedChanged(GraphicsViewEventArgs e) => SelectedChanged?.Invoke(this, e);

        protected virtual void OnHoveredChanged(GraphicsViewEventArgs e) => HoveredChanged?.Invoke(this, e);

        protected virtual void OnActiveChanged(GraphicsViewEventArgs e) => ActiveChanged?.Invoke(this, e);

        protected virtual void OnItemAdded(GraphicsViewEventArgs e) => ItemAdded?.Invoke(this, e);

        protected virtual void OnItemRemoved(GraphicsViewEventArgs e) => ItemRemoved?.Invoke(this, e);

        protected virtual void OnViewerMoved(GraphicsViewEventArgs e) => ViewerMoved?.Invoke(this, e);

        protected virtual void OnBoxSelectChanged(GraphicsViewEventArgs e) => BoxSelectChanged?.Invoke(this, e);
        #endregion

        #endregion

    }
}
