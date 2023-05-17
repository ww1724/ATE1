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
        private Point pos;
        private double width;

        private double height;
        #endregion

        public GraphicsItem(bool applyDefaultOptions = true)
        {
            AcceptDrops = false;
            AcceptHoverEvents = true;
            AcceptTouchEvents = false;
            Height = 200;
            Width = 150;
            Options = new List<NodeOption>();
            Background = Brushes.Transparent;
            NearOptionDistance = 30;

            if (applyDefaultOptions) ApplyDefaultOptions();

            // 事件初始化

            ItemResized += (o, e) =>
            {
                BoundingRect = new Rect(pos.X, pos.Y, Width, Height);
            };
            ItemMoved += (o, e) =>
            {
                BoundingRect = new Rect(pos.X, pos.Y, Width, Height);
            };
        }



        #region Fields
        public GraphicsView AttachedView { get; set; }

        public double NearOptionDistance { get; set; }

        public Guid Id { get; set; }

        public Brush Background { get; set; }

        /// <summary>
        /// 是否接收Drop事件
        /// </summary>
        public bool AcceptDrops { get; set; }

        /// <summary>
        /// 是否接收悬浮事件
        /// </summary>
        public bool AcceptHoverEvents { get; set; }

        /// <summary>
        /// 是否接收触摸事件
        /// </summary>
        public bool AcceptTouchEvents { get; set; }

        /// <summary>
        /// 缓冲模式
        /// </summary>
        public Common.CacheMode CacheMode { get; set; }

        /// <summary>
        /// 抓取所有鼠标事件
        /// </summary>
        public bool GrabMouse { get; set; }

        /// <summary>
        /// 抓取所有键盘事件
        /// </summary>
        public bool GrabKeyboard { get; set; }

        /// <summary>
        /// 鼠标形状
        /// </summary>
        public Cursor Cursor { get; set; }

        public double Opacity { get; set; }

        public int Rotation { get; set; }

        public double Scale { get; set; }

        public double Width { get => width; set { width = value; OnItemResized(null); } }


        public double Height { get => height; set { height = value; OnItemResized(null); } }
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
            set { pos = value; OnItemMoved(null); }
        }
        /// <summary>
        /// 父项目
        /// </summary>
        public GraphicsItem ParentItem { get; set; }

        /// <summary>
        /// 子项目
        /// </summary>
        public List<GraphicsItem> ChildItems { get; set; }

        /// <summary>
        /// Item边框
        /// </summary>
        public Rect BoundingRect { get; set; }

        public Rect SelfRect { get => new Rect(0, 0, width, Height); }

        /// <summary>
        /// 包含的数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 元数据
        /// </summary>
        public Dictionary<string, object> MetaData { get; set; }

        /// <summary>
        /// 是否被选择
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 是否鼠标悬停
        /// </summary>
        public bool IsHovered { get; set; }

        /// <summary>
        /// 是否按下鼠标
        /// </summary>
        public bool IsMousePressed { get; set; }

        public bool IsDragging { get; set; }

        /// <summary>
        /// 缩放
        /// </summary>
        public int Scalage { get; set; }

        public int ZIndex { get; set; }
        #endregion

        #region public slots
        public virtual void UpdateOptions()
        {
            
            SetDefaultOptions();
        }

        /// <summary>
        /// 应用默认选项点
        /// </summary>
        public void ApplyDefaultOptions()
        {
            ItemResized += (sender, e) =>
            {
                SetDefaultOptions();
            };
            OnItemResized(null);
        }

        /// <summary>
        /// 更新连接
        /// </summary>
        public virtual void UpdateOptionsPosition() { }

        /// <summary>
        /// 获取Item边界
        /// </summary>
        /// <returns></returns>
        public virtual Rect GetBoundingRect() => new(pos.X, pos.Y, Width, Height);

        protected void Update()
        {
            AttachedView.InvalidateVisual();
        }
        #endregion

        #region Private Slots
        void SetDefaultOptions()
        {
            Options = new List<NodeOption>()
                {
                    new NodeOption(this) { CenterPos = new Point(Width / 2, 0), Location=NodeOptionLocation.Top,  },
                    new NodeOption(this) { CenterPos = new Point(Width / 2, Height), Location=NodeOptionLocation.Bottom },
                    new NodeOption(this) { CenterPos = new Point(0, Height / 2), Location=NodeOptionLocation.Left },
                    new NodeOption(this) { CenterPos = new Point(Width, Height / 2), Location=NodeOptionLocation.Right },
                };
        }
        #endregion

        #region Events
        protected internal virtual void OnRender(DrawingContext drawingContext)
        {
            drawingContext.PushTransform(new TranslateTransform(Pos.X, Pos.Y)); 

            OnDrawFramework(drawingContext);

            OnDrawTitle(drawingContext);

            OnDrawContent(drawingContext);

            OnDrawConnectOption(drawingContext);

            OnDrawConnectLine(drawingContext);

            OnDrawBeforeMark(drawingContext);

            OnDrawMark(drawingContext);

            //OnDrawResizeBorder(drawingContext);

            drawingContext.Pop();
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
        protected internal virtual void OnDrawConnectOption(DrawingContext drawingContext)
        {
            
            foreach (var option in Options)
            {
                if (!(IsHovered || option.IsOnConnecting)) continue;
                Pen dotPen = option.IsHovered ? new Pen(Brushes.Blue, 1) : new Pen(Brushes.Black, 1);
                Brush bg = option.IsHovered ? Brushes.Red : Brushes.White;
                drawingContext.DrawEllipse(bg, dotPen, option.CenterPos, 4, 4);
            }
        }

        protected internal virtual void OnDrawBeforeMark(DrawingContext drawingContext) { }

        /// <summary>
        /// 绘制Resize边框
        /// </summary>
        /// <param name="drawingContext"></param>
        protected virtual void OnDrawResizeBorder(DrawingContext drawingContext)
        {

            if (IsActive)
            {
                ICollection<Rect> rects = new Collection<Rect>()
                {
                    new Rect(0 - 3, 0 - 3, 6, 6),
                    new Rect(Width / 2 - 3, 0 - 3, 6, 6),
                    new Rect(Width -3, 0 - 3, 6, 6),
                    new Rect(0 - 3, Height / 2 - 3, 6, 6),
                    new Rect(Width -3, Height / 2 - 3, 6, 6),
                    new Rect(0 - 3, Height - 3, 6, 6),
                    new Rect(Width / 2 - 3, Height - 3, 6, 6),
                    new Rect(Width -3, Height - 3, 6, 6)
                };
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

        protected event EventHandler TextInputed;

        protected virtual void OnSizeChanged(EventArgs e) => SizeChanged?.Invoke(this, e);

        protected virtual void OnOptionChanged(EventArgs e) => OptionChanged?.Invoke(this, e);

        protected internal virtual void OnItemResized(EventArgs e) => ItemResized?.Invoke(this, e);

        protected internal virtual void OnItemMoved(EventArgs e) => ItemMoved?.Invoke(this, e);


        protected internal virtual void OnTextInputed(TextCompositionEventArgs e) => TextInputed?.Invoke(this, e);
        #endregion
    }
}
