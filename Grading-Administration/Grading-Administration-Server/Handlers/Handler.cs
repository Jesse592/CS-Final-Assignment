using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Handlers
{
    abstract class Handler
    {
        private Dictionary<string, Action<JObject>> Actions;
        private Action<JObject> CurrentAction;

        protected Handler()
        {
            Init();
        }

        private void Init()
        {
            //Fill the actions dicitionary
        }
    }
}
