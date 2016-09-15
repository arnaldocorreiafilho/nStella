using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Inwords
{
    public class Messages
    {
        private static readonly string BUNDLE_NAME = "nStella.Inwords.Messages";
        static readonly CultureInfo LOCALE_PT_BR = new CultureInfo("pt-BR");
        private static readonly IDictionary<string, ResourceManager> RESOURCE_BUNDLES;

        static Messages()
        {
            Dictionary<string, ResourceManager> resourcesByLocale = new Dictionary<string, ResourceManager>(2);
            resourcesByLocale.Add(LOCALE_PT_BR.Name, new ResourceManager(BUNDLE_NAME, Assembly.GetAssembly(typeof(Messages))));
            resourcesByLocale.Add(CultureInfo.CreateSpecificCulture("en").Name, new ResourceManager(BUNDLE_NAME, Assembly.GetAssembly(typeof(Messages))));/
        }
        private Messages()
        {
        }

        static string GetString(string key)
        {
            return RESOURCE_BUNDLES[key].GetString(key);
        }

        static string GetString(string key, CultureInfo cultureInfo)
        {
            ResourceManager resourceManager = RESOURCE_BUNDLES[cultureInfo.Name];

            if (resourceManager == null)
            {
                throw new NotSupportedException("Não é possivel converter números para o idioma " + cultureInfo.DisplayName);
            }

            return resourceManager.GetString(key);
        }
    }
}
