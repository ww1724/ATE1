using ATE.Core.Mvvm;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATE.Core.Stores
{
    public class TestingStore : PropertyChangedBase, IViewModel
    {
        private Dictionary<string, string> codeMaps;
        private string workflowCode;
        private string flowChartCode;

        public TestingStore() { }

        public string CurrentTestingCode { get; set; }

        public Dictionary<string, string> OpeningCodeMaps { get => codeMaps; set { codeMaps = value; NotifyOfPropertyChange(); } }

        public string WorkflowCode { get => workflowCode; set => workflowCode = value; }

        public string FlowChartCode { get => flowChartCode; set => flowChartCode = value; }

        #region Actions
        public void InitAssembly()
        {

        }

        public void LoadTestingCode()
        {

        }

        public void SwitchTestingCode(string id)
        {
            CurrentTestingCode = OpeningCodeMaps[id];
        }
        #endregion

    }
}
