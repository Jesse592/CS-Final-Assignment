using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradings_Administration_client.FileIO
{
    public static class FileReadWriter
    {
        private static string DirectoryPath = Directory.GetCurrentDirectory();

        public static async Task<string> ReadUsernameAsync()
        {
            // Getting the path
            string path = Path.Combine(DirectoryPath, "saved.usn");

            // Creates new file if it doesnt exists, else overrides existing file
            string text = await File.ReadAllTextAsync(path);

            return text;
        }

        public static async Task<bool> SaveUserNameAsync(string username)
        {
            // Getting the path
            string path = Path.Combine(DirectoryPath, "saved.usn");

            // Checking if the file exists
            if (!File.Exists(path)) return false;

            await File.WriteAllTextAsync(path, username);

            return true;
        }

    }
}
