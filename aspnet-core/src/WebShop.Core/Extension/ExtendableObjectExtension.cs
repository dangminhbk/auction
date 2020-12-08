using Abp.Domain.Entities;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace WebShop.Extension
{
    public static class ExtendableObjectExtension
    {
        public static List<KeyValuePair<string, string>> GetKeyValuePairData(
            this IExtendableObject extendableObject)
        {
            JObject json = JObject.Parse(extendableObject.ExtensionData);
            Dictionary<string, string> kVP = (Dictionary<string, string>)json.ToObject(typeof(Dictionary<string, string>));
            return kVP.ToList<KeyValuePair<string, string>>();
        }
    }
}
