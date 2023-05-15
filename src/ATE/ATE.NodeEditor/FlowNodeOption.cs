using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoranof.GraphicsFramework
{
    public class FlowNodeOption
    {
        public FlowNodeOption()
        {

        }

        public FlowNodeOption(string text)
        {

        }

        #region Fields
        public string Text { get; set; }

        public string Description { get; set; }

        public GraphicsItem Owner { get; set; }

        public bool IsSingle { get; set; }

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
