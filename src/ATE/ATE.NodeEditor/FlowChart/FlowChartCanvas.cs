using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Zoranof.GraphicsFramework.FlowChart
{
    public class FlowChartCanvas : GraphicsView
    {

        #region Events
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            OnDrawConnectLine(drawingContext);
        }

        #region Customs
        protected internal void OnDrawConnectLine(DrawingContext drawingContext)
        {

        }
        #endregion

        #endregion
    }
}
