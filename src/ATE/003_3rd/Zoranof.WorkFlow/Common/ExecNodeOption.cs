using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoranof.GraphicsFramework;
using Zoranof.WorkFlow.Common.Interfaces;

namespace Zoranof.WorkFlow.Common
{
    public class ExecNodeOption : NodeOption, IExecOption
    {
        public ExecNodeOption(GraphicsItem owner, string text = "") : base(owner, text)
        {
        }

        public ExecOptionType OptionType { get; set; }
    }
}
