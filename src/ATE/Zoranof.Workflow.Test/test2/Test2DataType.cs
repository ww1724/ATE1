using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoranof.Workflow.Test.test2
{
    public class DynamicData
    {
        public Dictionary<string, object> Storage { get; set; } = new Dictionary<string, object>();

        public object this[string propertyName]
        {
            get => Storage.TryGetValue(propertyName, out var value) ? value : null;
            set => Storage[propertyName] = (object)value;
        }
    }
}
