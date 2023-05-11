using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ATE.GraphicsFramework.Interface
{
    public interface IGraphicsItem
    {
        protected internal virtual void OnRender(DrawingContext drawingContext) { }

        protected internal virtual void OnRenderSizeChanged(SizeChangedInfo sizeInfo) { }

        protected abstract Rect GetBoundingRect();

    }
}
