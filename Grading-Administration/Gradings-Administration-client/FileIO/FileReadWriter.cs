using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradings_Administration_client.FileIO
{
    /// <summary>
    /// Class that handles the reading and writing to a file
    /// </summary>
    public static class FileReadWriter
    {
        private static string DirectoryPath = Directory.GetCurrentDirectory();

        /// <summary>
        /// Reads a saved username from the clients computer
        /// </summary>
        /// <returns>The retreived username</returns>
        public static async Task<string> ReadUsernameAsync()
        {
            // Getting the path
            string path = Path.Combine(DirectoryPath, "saved.usn");

            // Checking if file exists
            if (!File.Exists(path)) return "Not Saved";

            string text = await File.ReadAllTextAsync(path);

            return text;
        }

        /// <summary>
        /// Writes a username to a file on the clients computer
        /// </summary>
        /// <param name="username">The username to write to</param>
        /// <returns>true if succesfull</returns>
        public static async Task<bool> SaveUserNameAsync(string username)
        {
            // Getting the path
            string path = Path.Combine(DirectoryPath, "saved.usn");

            // Creates new file if it doesnt exists, else overrides existing file
            await File.WriteAllTextAsync(path, username);

            return true;
        }

    }
}
