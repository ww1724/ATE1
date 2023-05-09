using ATE.Graphics.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATE.NodeEditor
{
    public class GraphicsView : Control
    {
        private Alignment alignment;

        static GraphicsView()
        {
        }


        #region Events
        protected override void OnRender(DrawingContext drawingContext)
        {
            // 绘制框架

            // 绘制网格

            // 绘制节点

            // 绘制连线
            
            base.OnRender(drawingContext);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
        }
        #endregion

        #region Fields
        public Alignment Alignment { get => alignment; set => alignment = value; }
        #endregion





    }
}
