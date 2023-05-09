using ATE.Graphics.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ATE.NodeEditor
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
        private Graphics.Enums.CacheMode cacheMode;
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
        public Graphics.Enums.CacheMode CacheMode { get => cacheMode; set => cacheMode = value; }

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

        public abstract bool IsCollidesWithItem(GraphicsItem item, ItemSelectionMode mode = ItemSelectionMode.IntersectsItemShape);

        public abstract IList<GraphicsItem> CollidesItems(ItemSelectionMode mode);

        public abstract bool IsContainsPoint(Point point);

        public abstract void EnsureVisible();

        public abstract Point MapToScene(Point point);
        public abstract Rect MapToScene(Rect rect);
        public abstract Path MapToScene(Path path);
        public abstract Polygon MapToScene(Polygon Polygon);

        public abstract void OnRender(DrawingContext drawingContext);
        #endregion


    }
}
