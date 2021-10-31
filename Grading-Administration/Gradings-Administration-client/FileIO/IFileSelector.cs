using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradings_Administration_client.FileIO
{
    public interface IFileSelector
    {
        string Start(object sender, string filer);
    }
}
