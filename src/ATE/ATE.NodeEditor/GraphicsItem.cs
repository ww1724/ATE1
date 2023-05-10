using ATE.GraphicsFramework.Enums;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ATE.GraphicsFramework
{
    public abstract class GraphicsItem
    {
        #region Private Member
        private bool acceptDrops;
        private bool acceptHoverEvents;
        private bool acceptTouchEvents;
        private Rect boundingRect;
        private List<GraphicsItem> childItems;
        private Dictionary<string, object> metaData;
        private object data;
        private Enums.CacheMode cacheMode;
        private double opacity;
        private Cursor cursor;
        private bool grabMouse;
        private bool isFocus;
        private bool grabKeyboard;
        private GraphicsItem parentItem;
        private Point pos;
        private int rotation;
        private double scale;
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
        public GraphicsFramework.Enums.CacheMode CacheMode { get => cacheMode; set => cacheMode = value; }

        /// <summary>
        /// 抓取所有鼠标事件
        /// </summary>
        public bool GrabMouse { get => grabMouse; set => grabMouse = value; }


        /// <summary>
        /// 抓取所有键盘事件
        /// </summary>
        public bool GrabKeyboard { get => grabKeyboard; set => grabKeyboard = value; }

        /// <summary>
        /// 是否获取焦点
        /// </summary>
        public bool IsFocus { get => isFocus; set => isFocus = value; }
        #endregion

        #region Appearances
        /// <summary>
        /// 鼠标形状
        /// </summary>
        public Cursor Cursor { get => cursor; set => cursor = value; }

        public double Opacity { get => opacity; set => opacity = value; }

        public int Rotation { get => rotation; set => rotation = value; }

        public double Scale { get => scale; set => scale = value; }
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
        /// 当前位置
        /// </summary>
        public Point Pos { get => pos; set => pos = value; }
        #endregion

        #region Functions
        public Rect BoundingRegion() => boundingRect;

        public Rect SceneBoundingRect () => boundingRect;

        protected internal virtual bool IsCollidesWithItem(GraphicsItem item, ItemSelectionMode mode = ItemSelectionMode.IntersectsItemShape) { return true; }

        protected internal virtual IList<GraphicsItem> CollidesItems(ItemSelectionMode mode) { return null; }

        protected internal virtual bool IsContainsPoint(Point point) { return false; } 

        protected internal virtual void EnsureVisible() { }

        protected internal virtual Point MapToScene(Point point) { return new Point(); }
        protected internal virtual Rect MapToScene(Rect rect) { return new Rect();  }
        protected internal virtual Path MapToScene(Path path) { return new Path(); }
        protected internal virtual Polygon MapToScene(Polygon Polygon) { return new Polygon(); }

        #endregion

        #region Events
        protected internal virtual void OnRender(DrawingContext drawingContext) { }

        protected internal virtual void OnMouseEnter(EventArgs args) { }
        protected internal virtual void OnMouseExit(EventArgs args) { }
        protected internal virtual void OnMouseMove(EventArgs args) { }
        protected internal virtual void OnMouseDown(EventArgs args) { }
        protected internal virtual void OnMouseUp(EventArgs args) { }

        protected internal virtual void OnDrop(EventArgs args) { }

        protected internal virtual void OnKeyUp(EventArgs args) { }
        protected internal virtual void OnKeyDown(EventArgs args) { }

        protected internal virtual void OnMove(EventArgs args) { }
        protected internal virtual void OnResize(EventArgs args) { }
        #endregion
    }
}
