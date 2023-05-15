using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoranof.GraphicsFramework.Common
{
    public class GraphicsItemAttribute : Attribute
    {
		public GraphicsItemAttribute(string name, string path, string description) { 
			Name = name;
			Path = path;
			Description = description;
		}

		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}


		private string path;

		public string Path
		{
			get { return path; }
			set { path = value; }
		}


		private string description;

		public string Description
		{
			get { return description; }
			set { description = value; }
		}


		private string  auther;

		public string  Auther
		{
			get { return auther; }
			set { auther = value; }
		}

		private string link;

		public string Link
		{
			get { return link; }
			set { link = value; }
		}


	}
}
