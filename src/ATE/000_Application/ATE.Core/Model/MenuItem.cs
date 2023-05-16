using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATE.Core.Model
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public bool CanClose { get; set; } = true;
    }
}
