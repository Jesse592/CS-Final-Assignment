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
        protected Dictionary<string, Action<JObject>> Actions;
        protected Action<JObject> CurrentAction;

        /// <summary>
        /// Constructor for abstract Handler, calls the Init method
        /// </summary>
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

        /// <summary>
        /// Init method inserts all the commands in the handler
        /// </summary>
        protected abstract void Init();
    }
}
