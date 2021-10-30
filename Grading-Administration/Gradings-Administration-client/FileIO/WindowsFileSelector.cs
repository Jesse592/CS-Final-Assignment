using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gradings_Administration_client.FileIO
{
    class WindowsFileSelector : IFileSelector
    {
        public string Start(object sender, string extentions)
        {
                var fileSelector = new SaveFileDialog();

                if (!string.IsNullOrWhiteSpace(extentions))
                {
                    fileSelector.Filter = extentions;
                }
                fileSelector.ShowDialog(sender as Window);

                return fileSelector.FileNames.FirstOrDefault();
        }
    }
}
