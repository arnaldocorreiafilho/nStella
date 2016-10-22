using System.Resources;
using nStella.Core.Validation;
using System.Globalization;
using System.Text.RegularExpressions;

namespace nStella.Core
{
    public class ResourceBundleMessageProducer : IMessageProducer
    {
        private readonly ResourceManager bundle;

        public ResourceBundleMessageProducer(ResourceManager bundle)
        {
            this.bundle = bundle;
        }

        public IValidationMessage GetMessage(IInvalidValue error)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;

            string key = MessageKeyFor(culture, error.GetType(), error);
            string message;
            try
            {
                message = bundle.GetString(key);
            }
            catch (MissingManifestResourceException)
            {
                Regex regx = new Regex("");
                message = regx.Replace(key, "[.]", 1).Replace("_", " ").ToString();
            }

            return new SimpleValidationMessage(message);
        }

        public string MessageKeyFor<T>(CultureInfo cultureInfo, T errorClass, IInvalidValue error)
        {
            string simpleName = errorClass.GetType().Name;
            string errorName = error.Name();
            string key = simpleName + "." + errorName;
            return key.ToLower(cultureInfo);
        }
    }
}
