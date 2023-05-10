using ATE.GraphicsFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Zoranof.GraphicsFramework.WorkFlow
{
    public class WorkFlowCanvas : GraphicsView
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawEllipse(Brushes.Red, new Pen(), new System.Windows.Point(50, 50), 50, 50);
        }
    }
}
