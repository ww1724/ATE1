using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Zoranof.GraphicsFramework.FlowChart
{
    public class FlowItem : FlowItemBase, IFlowItem
    {
        public FlowItem() {
            Height = 320;
            Width = 120;
        }


        #region Paint
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }

        /// <summary>
        /// 绘制连接线
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawConnectLine(DrawingContext drawingContext)
        {

        }

        /// <summary>
        /// 绘制连接点
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawConnectDot(DrawingContext drawingContext)
        {

        }

        /// <summary>
        /// 绘制标记
        /// </summary>
        /// <param name="drawingContext"></param>
        protected internal virtual void OnDrawMark(DrawingContext drawingContext)
        {

        }

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
        #endregion

    }
}
