using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zoranof.GraphicsFramework.Common;

namespace Zoranof.GraphicsFramework
{
    

    public class NodeOption
    {


        public NodeOption(GraphicsItem owner, string text="")
        {
            Owner = owner;
        }

        #region Fields
        // 中心点
        public Point CenterPos { get; set; }

        // 内容
        public string Text { get; set; }

        // 描述
        public string Description { get; set; }

        // 所有者
        public GraphicsItem Owner { get; set; }

        // 单连接
        public bool IsSingle { get; set; }

        // 通用位置
        public bool IsRelative { get; set; }
        public NodeOptionLocation Location { get; set; }

        // 视图绝对位置
        public Point PointToViewer { get => new Point(Owner.Pos.X + CenterPos.X, Owner.Pos.Y + CenterPos.Y); }

        // hover 准备高亮
        public bool IsHovered;

        // 作为连接起点正在连接
        public bool IsOnConnecting;
        #endregion

        #region static public slots
        
        #endregion

        #region Custom Events
        public event EventHandler Connected;

        public event EventHandler ConnectStarted;

        public event EventHandler Disconnected;

        public event EventHandler DisconnectStarted;

        public event EventHandler DataTransfered;

        public event EventHandler DataTransferStarted;


        protected internal virtual void OnConnected(EventArgs e) { Connected?.Invoke(this, e); }

        protected internal virtual void OnConnectStarted(EventArgs e) { ConnectStarted?.Invoke(this, e); }

        protected internal virtual void OnDisconnectStarted(EventArgs e) { DisconnectStarted?.Invoke(this, e); }

        protected internal virtual void OnDisconnected(EventArgs e) { Disconnected?.Invoke(this, e); }

        protected internal virtual void OnDataTransfered(EventArgs e) { DataTransfered?.Invoke(this, e); }

        protected internal virtual void OnDataTransferStarted(EventArgs e) { DataTransferStarted.Invoke(this, e); }
        #endregion

        #region private slots
        //private bool AddConnection(GraphicsOption graphicsOption) { }
        #endregion
    }
}
