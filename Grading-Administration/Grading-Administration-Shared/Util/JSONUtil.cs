using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grading_Administration_Shared.Util
{
    public static class JSONUtil
    {

        /// <summary>
        /// Newtonsoft does not contain tryParse methodes, it chrases when a invalid string is given
        /// </summary>
        /// <param name="message">The string to be parsed to JSON</param>
        /// <param name="jObject">The object that was parsed</param>
        /// <returns>True if succesfull, otherwise false</returns>
        public static bool TryParse(this JObject jObject, string message, out JObject parsedObject)
        {
            parsedObject = null;

            // Checking some values that can be check witgout try-catch
            if (string.IsNullOrWhiteSpace(message)) return false;
            if (!message.StartsWith("{") || !message.EndsWith("}")) return false;

            try
            {
                parsedObject = JsonConvert.DeserializeObject(message) as JObject;
                return true;
            } catch (JsonReaderException e)
            {
                return false;
            }
        }

    }
}
