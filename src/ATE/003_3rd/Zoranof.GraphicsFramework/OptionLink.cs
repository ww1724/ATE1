using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Zoranof.GraphicsFramework.Common;

namespace Zoranof.GraphicsFramework
{
    public class OptionLink
    {
        // func
        public GraphicsView Attacher { get; set; }

        public NodeOption From { get; set; }

        public NodeOption To { get; set; }

        // draw
        public Point StartPoint { get => From.PointToViewer; }

        public Point EndPoint { get => To.PointToViewer; }

        PointCollection Points { get; set; }

        public Polyline Polyline { get; set; }

        public Polygon Arrow { get; set; }

        public bool IsDragging { get; set; }

        public bool IsHovered { get; set; }

        public bool IsSelected { get; set; }

        public OptionLink()
        {
            LinkUpdated += (o, e) =>
            {

            };
        }

        #region public slots
        public Point PointExtend(Point point, NodeOptionLocation location, double distance) {
            Point ret;
            switch(location)
            {
                case NodeOptionLocation.Left: ret = new Point(point.X - distance, point.Y);break;
                case NodeOptionLocation.Right: ret = new Point(point.X + distance, point.Y); break;
                case NodeOptionLocation.Top: ret = new Point(point.X, point.Y - distance); break;
                case NodeOptionLocation.Bottom: ret = new Point(point.X, point.Y + distance); break;
                default: ret = point; break;
            }
            return ret;
        }

        public Polyline GenerateLinkLines()
        {
            Polyline a = new Polyline();
            List<Point> points1 = new() { StartPoint };
            List<Point> points2 = new() { EndPoint };
            
            points1.Add(PointExtend(StartPoint, From.Location, Attacher.ConnectLineDistance));
            points2.Add(PointExtend(EndPoint, To.Location, Attacher.ConnectLineDistance));

            Vector vector = new(points1.Last().X - points2.Last().X, points1.Last().Y - points2.Last().Y);
            
            
            
            return a;
        }
        #endregion

        #region Events
        public event EventHandler LinkUpdated;

        public virtual void OnLinkUpdated(EventArgs e) => LinkUpdated?.Invoke(this, e);
        #endregion



    }
}
