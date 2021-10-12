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

        /// <summary>
        /// Method checks if this handles has the command, if so the action is performed
        /// </summary>
        /// <param name="command">The command that is called to the handler</param>
        /// <param name="data">The data given</param>
        public void Invoke(string command, JObject data)
        {
            if (this.Actions.ContainsKey(command))
                this.Actions[command].Invoke(data);
        }

        private void Init()
        {
            //Fill the actions dicitionary
        }
    }
}
