using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Zoranof.GraphicsFramework.Common;

namespace Zoranof.GraphicsFramework
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
        private bool isDragging;
        private Point draggingStartPos;
        private bool isActive;
        #endregion

        public GraphicsItem()
        {

            // default value init
            acceptDrops = false;
            acceptHoverEvents = true;
            acceptTouchEvents = false;
            Height = 320;
            Width = 120;
            Options = new List<NodeOption>();
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
            OnPosChanged(EventArgs.Empty);
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
        public Guid Id { get; set; }

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

        #endregion

        #region Variables
        public NodeOption ActiveOption { get; set; }
        public NodeOption HoverOprion { get; set; }
        public List<NodeOption> Options { get; set; }

        /// <summary>
        /// 当前位置
        /// </summary>
        public Point Pos
        {
            get => pos;
            set { pos = value; BoundingRect = new Rect(pos.X, pos.Y, width, height); }
        }
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
        /// 是否被选择
        /// </summary>
        public bool IsSelected { get => isSelected; set => isSelected = value; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get => isActive; set { isActive = value; ZIndex = value ? 1 : 0; } }

        /// <summary>
        /// 是否鼠标悬停
        /// </summary>
        public bool IsHovered { get; set; }

        /// <summary>
        /// 是否按下鼠标
        /// </summary>
        public bool IsMousePressed { get; set; }

        public bool IsDragging { get => isDragging; set => isDragging = value; }

        public Point DraggingStartPos { get => draggingStartPos; set => draggingStartPos = value; }

        /// <summary>
        /// 缩放
        /// </summary>
        public int Scalage { get; set; }


        public int ZIndex { get; set; }
        #endregion

        #region public slots
        /// <summary>
        /// 更新连接
        /// </summary>
        public virtual void UpdateOptionsPosition() { }

        /// <summary>
        /// 获取附近连接点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual NodeOption NearOption(Point point) => null;

        /// <summary>
        /// 获取Item边界
        /// </summary>
        /// <returns></returns>
        public virtual Rect GetBoundingRect() => new(pos.X, pos.Y, width, height);

        protected void Update()
        {
            AttachedView.InvalidateVisual();
        }
        #endregion

        #region Events
        protected internal virtual void OnRender(DrawingContext drawingContext) 
        {

            OnDrawFramework(drawingContext);

            OnDrawTitle(drawingContext);

            OnDrawContent(drawingContext);

            OnDrawConnectDot(drawingContext);

            OnDrawConnectLine(drawingContext);

            OnDrawMark(drawingContext);

            OnDrawResizeBorder(drawingContext);

        }

        /// <summary>
        /// 绘制框架 背景+边框
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawFramework(DrawingContext drawingContext) { }

        /// <summary>
        /// 绘制标题
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawTitle(DrawingContext drawingContext) { }

        /// <summary>
        /// 绘制内容
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawContent(DrawingContext drawingContext) { }

        /// <summary>
        /// 绘制连接点
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawConnectDot(DrawingContext drawingContext)
        {
            drawingContext.PushTransform(new TranslateTransform(Pos.X, Pos.Y));
            Pen dotPen;
            Brush bg;
            ICollection<Point> points = new Collection<Point>()
                {
                    new Point(Width / 2, 0),
                    new Point(0, Height / 2),
                    new Point(Width, Height / 2),
                    new Point(Width / 2, Height)
                };
            if (IsHovered)
            {
                dotPen = new Pen(Brushes.Black, 1);
                bg = Brushes.White;
            }
            else
            {
                dotPen = new Pen(Brushes.Transparent, 1);
                bg = Brushes.Transparent;
            }

            foreach (Point point in points)
            {
                drawingContext.DrawEllipse(bg, dotPen, point, 3, 3);
            }
            drawingContext.Pop();
        }

        /// <summary>
        /// 绘制Resize边框
        /// </summary>
        /// <param name="drawingContext"></param>
        protected virtual void OnDrawResizeBorder(DrawingContext drawingContext)
        {
            ICollection<Rect> rects = new Collection<Rect>()
            {
                new Rect(0 - 3, 0 - 3, 6, 6),
                new Rect(Width / 2 - 3, 0 - 3, 6, 6),
                new Rect(Width -3, 0 - 3, 6, 6),
                new Rect(0 - 3, Height / 2 - 3, 6, 6),
                //new Rect(Width / 2 - 3, Height / 2 - 3, 6, 6),
                new Rect(Width -3, Height / 2 - 3, 6, 6),
                new Rect(0 - 3, Height - 3, 6, 6),
                new Rect(Width / 2 - 3, Height - 3, 6, 6),
                new Rect(Width -3, Height - 3, 6, 6)
            };
            if (IsActive)
            {
                drawingContext.DrawRectangle(Brushes.Transparent, new Pen(Brushes.Black, 1), BoundingRect);
                drawingContext.PushTransform(new TranslateTransform(Pos.X, Pos.Y));

                foreach (Rect rect in rects)
                {
                    drawingContext.DrawRectangle(Brushes.White, new Pen(Brushes.CadetBlue, 1), rect);
                }
                drawingContext.Pop();
            }

        }

        /// <summary>
        /// 绘制连接线
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawConnectLine(DrawingContext drawingContext)
        {

        }

        /// <summary>
        /// 绘制标记
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawMark(DrawingContext drawingContext)
        {

        }


        #endregion

        #region Custom Events
        public event EventHandler ItemResized;

        public event EventHandler ItemMoved;

        public event EventHandler PosChanged;

        protected event EventHandler SizeChanged;

        protected event EventHandler OptionChanged;

        protected virtual void OnSizeChanged(EventArgs e) => SizeChanged?.Invoke(this, e);

        protected virtual void OnOptionChanged(EventArgs e) => OptionChanged?.Invoke(this, e);

        protected internal virtual void OnItemResized(EventArgs e) => ItemResized?.Invoke(this, e);

        protected internal virtual void OnItemMoved(EventArgs e) => ItemMoved?.Invoke(this, e);

        protected internal virtual void OnPosChanged(EventArgs e) => PosChanged?.Invoke(this, e);
        #endregion
    }
}
