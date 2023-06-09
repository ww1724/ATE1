﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoranof.GraphicsFramework.Common
{
    public class GraphicsViewEventArgs : EventArgs
    {
        private GraphicsItem item;

        public GraphicsItem Item
        {
            get { return item; }
            set { item = value; }
        }

        public GraphicsViewEventArgs(GraphicsItem item) { Item = item; }
    }
}
