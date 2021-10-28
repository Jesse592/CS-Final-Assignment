using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Handlers
{
    public abstract class Handler
    {
        protected Dictionary<string, Action<JObject, int>> Actions;
        protected Action<JObject> SendAction;

        /// <summary>
        /// Constructor for abstract Handler, calls the Init method
        /// </summary>
        protected Handler(Action<JObject> sendAction)
        {
            this.Actions = new Dictionary<string, Action<JObject, int>>();

            this.Actions.Add("Send", (j, s) => sendAction.Invoke(j));
            this.SendAction = sendAction;

            Init();
        }

        /// <summary>
        /// Method checks if this handles has the command, if so the action is performed
        /// </summary>
        /// <param name="command">The command that is called to the handler</param>
        /// <param name="data">The data given</param>
        public bool Invoke(string command, JObject data, int serial)
        {
            if (this.Actions.ContainsKey(command))
                this.Actions[command].Invoke(data, serial);
            return this.Actions.ContainsKey(command);
        }

        /// <summary>
        /// Init method inserts all the commands in the handler
        /// </summary>
        protected abstract void Init();
    }
}
    