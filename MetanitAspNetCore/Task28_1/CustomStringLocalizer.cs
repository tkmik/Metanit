using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Task28_1
{
    public class CustomStringLocalizer : IStringLocalizer
    {
        Dictionary<string, Dictionary<string, string>> resources;

        const string HEADER = "Header";
        const string MESSAGE = "Message";

        public CustomStringLocalizer()
        {
            Dictionary<string, string> enDict = new Dictionary<string, string>
            {
                {HEADER, "Welcome" },
                {MESSAGE, "Hello" }
            };
            Dictionary<string, string> ruDict = new Dictionary<string, string>
            {
                {HEADER, "Добо пожаловать" },
                {MESSAGE, "Привет" }
            };
            Dictionary<string, string> deDict = new Dictionary<string, string>
            {
                {HEADER, "Willkommen" },
                {MESSAGE, "Hallo" }
            };
            resources = new Dictionary<string, Dictionary<string, string>>
            {
                {"en", enDict },
                {"ru", ruDict },
                {"de", deDict }
            };
        }
        public LocalizedString this[string name]
        {
            get
            {
                var currentCulture = CultureInfo.CurrentUICulture;
                string val = "";
                if (resources.ContainsKey(currentCulture.Name))
                {
                    if (resources[currentCulture.Name].ContainsKey(name))
                    {
                        val = resources[currentCulture.Name][name];
                    }
                }
                return new LocalizedString(name, val);
            }
        }
        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();              

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }
    }
}
