using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Zoranof.GraphicsFramework.FlowChart
{
    public class OptionLink
    {
        
        public Point StartPoint { get => From.PointToViewer; }

        public Point EndPoint { get => To.PointToViewer; }
        
        public NodeOption From { get; set; }

        public NodeOption To { get; set; }

        public GraphicsItem Attacher { get; set; }

        public Polyline Polyline { get; set; }

        public Polygon Arrow { get; set; }

        public bool IsDragging { get; set; }

        public OptionLink()
        {
            LinkUpdated += (o, e) =>
            {
                
            };
        }

        public virtual void OnRender(DrawingContext drawingContext)
        {

        }

        #region Events
        public event EventHandler LinkUpdated;

        public virtual void OnLinkUpdated(EventArgs e) => LinkUpdated?.Invoke(this, e);
        #endregion



    }
}
