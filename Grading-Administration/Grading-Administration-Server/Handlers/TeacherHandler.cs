using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Handlers
{
    class TeacherHandler : Handler
    {
        public TeacherHandler(Action<JObject> sendAction) : base(sendAction)
        {
        }

        protected override void Init()
        {
            throw new NotImplementedException();
        }
    }
}
