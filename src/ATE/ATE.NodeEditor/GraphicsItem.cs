using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Zoranof.GraphicsFramework.Common;
using Zoranof.GraphicsFramework.Common.Interface;

namespace Zoranof.GraphicsFramework
{
    public abstract class GraphicsItem : IScaled
    {
        #region Private Member
        private bool acceptDrops;
        private bool acceptHoverEvents;
        private bool acceptTouchEvents;
        private Rect boundingRect;
        private List<GraphicsItem> childItems;
        private Dictionary<string, object> metaData;
        private object data;
        private Common.CacheMode cacheMode;
        private double opacity;
        private Cursor cursor;
        private bool grabMouse;
        private bool isSelected;
        private bool grabKeyboard;
        private GraphicsItem parentItem;
        private Point pos;
        private int rotation;
        private double scale;
        private double width;
        private double height;
        private bool isMouseCaptured;
        private bool isMousePressed;
        private bool isDragging;
        private Point draggingStartPos;
        #endregion

        public GraphicsItem()
        {
            acceptDrops = false;
            acceptHoverEvents = true;
            acceptTouchEvents = false;
        }

        public GraphicsView AttachedView { get; set; }

        #region General Slots
        public virtual void MoveTo(Point target) { Pos = target; }

        public virtual void MoveWithOssfet(Vector offset)
        {
            Pos = new Point(Pos.X + offset.X, Pos.Y + offset.Y);
        }
        #endregion


        #region Options
        /// <summary>
        /// 是否接收Drop事件
        /// </summary>
        public bool AcceptDrops { get => acceptDrops; set => acceptDrops = value; }

        /// <summary>
        /// 是否接收悬浮事件
        /// </summary>
        public bool AcceptHoverEvents { get => acceptHoverEvents; set => acceptHoverEvents = value; }

        /// <summary>
        /// 是否接收触摸事件
        /// </summary>
        public bool AcceptTouchEvents { get => acceptTouchEvents; set => acceptTouchEvents = value; }

        /// <summary>
        /// 缓冲模式
        /// </summary>
        public Common.CacheMode CacheMode { get => cacheMode; set => cacheMode = value; }

        /// <summary>
        /// 抓取所有鼠标事件
        /// </summary>
        public bool GrabMouse { get => grabMouse; set => grabMouse = value; }


        /// <summary>
        /// 抓取所有键盘事件
        /// </summary>
        public bool GrabKeyboard { get => grabKeyboard; set => grabKeyboard = value; }
        #endregion

        #region Appearances
        /// <summary>
        /// 鼠标形状
        /// </summary>
        public Cursor Cursor { get => cursor; set => cursor = value; }

        public double Opacity { get => opacity; set => opacity = value; }

        public int Rotation { get => rotation; set => rotation = value; }

        public double Scale { get => scale; set => scale = value; }

        public double Width { get => width; set => width = value; }

        public double Height { get => height; set => height = value; }

        /// <summary>
        /// 当前位置
        /// </summary>
        public Point Pos
        {
            get => pos;
            set { pos = value; BoundingRect = new Rect(pos.X, pos.Y, width, height); }
        }
        #endregion

        #region Variables
        /// <summary>
        /// 父项目
        /// </summary>
        public GraphicsItem ParentItem { get => parentItem; set => parentItem = value; }

        /// <summary>
        /// 子项目
        /// </summary>
        public List<GraphicsItem> ChildItems { get => childItems; set => childItems = value; }

        /// <summary>
        /// Item边框
        /// </summary>
        public Rect BoundingRect { get => boundingRect; set => boundingRect = value; }


        /// <summary>
        /// 包含的数据
        /// </summary>
        public object Data { get => data; set => data = value; }

        /// <summary>
        /// 元数据
        /// </summary>
        public Dictionary<string, object> MetaData { get => metaData; set => metaData = value; }

        /// <summary>
        /// 是否获取焦点
        /// </summary>
        public bool IsSelected { get => isSelected; set => isSelected = value; }

        /// <summary>
        /// 是否抓取到鼠标
        /// </summary>
        public bool IsMouseCaptured { get => isMouseCaptured; set => isMouseCaptured = value; }

        /// <summary>
        /// 是否按下鼠标
        /// </summary>
        public bool IsMousePressed { get => isMousePressed; set => isMousePressed = value; }

        public bool IsDragging { get => isDragging; set => isDragging = value; }

        public Point DraggingStartPos { get => draggingStartPos; set => draggingStartPos = value; }

        public int Scalage { get; set; }


        #endregion

        #region Functions
        public Rect BoundingRegion() => boundingRect;

        public Rect SceneBoundingRect() => boundingRect;

        protected internal virtual bool IsCollidesWithItem(GraphicsItem item, ItemSelectionMode mode = ItemSelectionMode.IntersectsItemShape) { return true; }

        protected internal virtual IList<GraphicsItem> CollidesItems(ItemSelectionMode mode) { return null; }

        protected internal virtual bool IsContainsPoint(Point point) { return false; }

        protected internal virtual void EnsureVisible() { }

        protected internal virtual Point MapToScene(Point point) { return new Point(); }
        protected internal virtual Rect MapToScene(Rect rect) { return new Rect(); }
        protected internal virtual Path MapToScene(Path path) { return new Path(); }
        protected internal virtual Polygon MapToScene(Polygon Polygon) { return new Polygon(); }

        protected void Update()
        {
            AttachedView.InvalidateVisual();
        }
        #endregion

        #region Events
        protected internal virtual void OnRender(DrawingContext drawingContext) { }

        protected internal virtual void OnMouseEnter(MouseEventArgs args)
        {
            IsMouseCaptured = true;
        }

        protected internal virtual void OnMouseExit(MouseEventArgs args)
        {
            isMouseCaptured = false;
            IsDragging = false;
        }

        protected internal virtual void OnMouseMove(MouseEventArgs args)
        {
            Console.WriteLine("当前", isDragging.ToString());
            if (IsDragging)
            {
                var p = args.GetPosition(AttachedView);
                Pos = new Point(Pos.X + p.X - draggingStartPos.X, pos.Y + p.Y - draggingStartPos.Y);
                DraggingStartPos = p;

                Console.Write((Pos.X + p.X - draggingStartPos.X).ToString(), (pos.Y + p.Y - draggingStartPos.Y).ToString());

                Update();
            }
        }
        protected internal virtual void OnMouseDown(MouseEventArgs args)
        {
            if (args.LeftButton == MouseButtonState.Pressed)
            {
                isDragging = true;
                IsSelected = true;
                DraggingStartPos = args.GetPosition(AttachedView);
                Console.WriteLine("开始拖动" + Pos.ToString());
            }

        }
        protected internal virtual void OnMouseUp(EventArgs args)
        {
            isDragging = false;
            Update();
        }

        protected internal virtual void OnDrop(EventArgs args) { }

        protected internal virtual void OnKeyUp(EventArgs args) { }
        protected internal virtual void OnKeyDown(EventArgs args) { }

        protected internal virtual void OnMove(EventArgs args) { }
        protected internal virtual void OnResize(EventArgs args) { }
        #endregion
    }
}
