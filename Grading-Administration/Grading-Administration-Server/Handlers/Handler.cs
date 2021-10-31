using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Handlers
{
    /// <summary>
    /// Abstract class that holds all the commands that can be called at a given state
    /// </summary>
    public abstract class Handler
    {
        // delegate that is a action call from the client
        public delegate Task CommandAction(JObject data, int identifier);

        protected Dictionary<string, CommandAction> Actions;
        protected Action<JObject> SendAction;

        /// <summary>
        /// Constructor for abstract Handler, calls the Init method
        /// </summary>
        protected Handler(Action<JObject> sendAction)
        {
            this.Actions = new Dictionary<string, CommandAction>();
            this.SendAction = sendAction;

            Init();
        }

        /// <summary>
        /// Method checks if this handles has the command, if so the action is performed
        /// </summary>
        /// <param name="command">The command that is called to the handler</param>
        /// <param name="data">The data given</param>
        public async Task Invoke(string command, JObject data, int serial)
        {
            if (this.Actions.ContainsKey(command))
                 await this.Actions[command].Invoke(data, serial);
        }

        /// <summary>
        /// Init method inserts all the commands in the handler
        /// </summary>
        protected abstract void Init();
    }
}
    