using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ECommerce.IntegratedTests.Helpers
{
    public static class HttpContentHelper
    {
        public static StringContent ToJsonStringContent(this object obj)
        {
            var serializedObject = JsonConvert.SerializeObject(obj);

            var httpContent = new StringContent(serializedObject, UnicodeEncoding.UTF8, "application/json");

            return httpContent;
        }
    }
}