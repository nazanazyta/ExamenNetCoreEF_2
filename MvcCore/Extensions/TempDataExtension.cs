using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MvcCore.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Extensions
{
    public static class TempDataExtension
    {
        public static void SetObjectTempData
            (this ITempDataDictionary tempdata, String key, Object objeto)
        {
            tempdata[key] = JsonConvert.SerializeObject(objeto);
            //String data = HelperToolKit.SerializeJsonObject(objeto);
            //tempdata.SetObjectTempData(key, data);
        }

        public static T GetObjectTempData<T>(this ITempDataDictionary tempdata, String key)
        {
            if (tempdata[key] == null)
            {
                return default(T);
            }
            else
            {
                String data = tempdata[key].ToString();
                return JsonConvert.DeserializeObject<T>(data);
            }
        }
    }
}
