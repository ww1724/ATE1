using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Zoranof.GraphicsFramework.FlowChart
{
    public class OptionLink
    {
        public NodeOption From { get; set; }

        public NodeOption To { get; set; }

        public GraphicsItem Attacher { get; set; }

        public Polyline Polyline { get; set; }

        public Polygon Arrow { get; set; }

        public bool IsDragging { get; set; }

        public NodeOption Target {get; set; }

        public virtual void OnRender(DrawingContext drawingContext)
        {

        }

        public virtual void UpdateLink() { }

        public event EventHandler TargetChanged;

        public virtual void OnTargetChanged(EventArgs e) => TargetChanged?.Invoke(this, e);

    }
}
