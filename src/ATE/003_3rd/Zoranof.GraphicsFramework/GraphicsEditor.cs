using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Zoranof.GraphicsFramework.FlowChart;

namespace Zoranof.GraphicsFramework
{
    public class GraphicsEditor : GraphicsView
    {
        public GraphicsEditor() : base()
        {
            IsEditable = true;
        }
    }
}
