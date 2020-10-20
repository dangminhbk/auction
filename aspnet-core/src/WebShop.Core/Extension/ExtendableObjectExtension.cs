using Abp.Domain.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebShop.Extension
{
    public static class ExtendableObjectExtension
    {
        public static List<KeyValuePair<string, string>> GetKeyValuePairData(
            this IExtendableObject extendableObject) 
        {
            var json = JObject.Parse(extendableObject.ExtensionData);
            var kVP =(Dictionary<string, string>) json.ToObject(typeof(Dictionary<string, string>));
            return kVP.ToList<KeyValuePair<string, string>>();
        }
    }
}
