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
        /// <summary>
        /// 移动到点位
        /// </summary>
        /// <param name="target"></param>
        public virtual void MoveTo(Point target) { Pos = target; }

        /// <summary>
        /// 移动偏移
        /// </summary>
        /// <param name="offset"></param>
        public virtual void MoveWithOssfet(Vector offset)
        {
            Pos = new Point(Pos.X + offset.X, Pos.Y + offset.Y);
        }

        /// <summary>
        /// 转换坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected internal virtual Point MapToView(Point point) { return new Point(); }

        /// <summary>
        /// 转换矩形坐标
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        protected internal virtual Rect MapToView(Rect rect) { return new Rect(); }

        #endregion


        #region Fields
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
        /// 是否按下鼠标
        /// </summary>
        public bool IsMousePressed { get => isMousePressed; set => isMousePressed = value; }

        public bool IsDragging { get => isDragging; set => isDragging = value; }

        public Point DraggingStartPos { get => draggingStartPos; set => draggingStartPos = value; }

        /// <summary>
        /// 缩放
        /// </summary>
        public int Scalage { get; set; }


        #endregion

        #region public slots
        /// <summary>
        /// 获取Item边界
        /// </summary>
        /// <returns></returns>
        public virtual Rect GetBoundingRect() => new Rect(pos.X, pos.Y, width, height);

        protected void Update()
        {
            AttachedView.InvalidateVisual();
        }
        #endregion

        #region Events
        protected internal virtual void OnRender(DrawingContext drawingContext) { }
        #endregion

        #region Custom Events
        public event EventHandler ItemResized;

        public event EventHandler ItemMoved;


        protected internal virtual void OnItemResized(EventArgs e) => ItemResized?.Invoke(this, e);

        protected internal virtual void OnItemMoved(EventArgs e) => ItemMoved?.Invoke(this, e);
        #endregion
    }
}
