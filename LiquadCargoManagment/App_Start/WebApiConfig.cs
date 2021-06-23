using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

class WebApiConfig
{
    public static void Register(HttpConfiguration configuration)
    {
       

        configuration.Routes.MapHttpRoute("emailRoute", "api/{controller}/{action}/{id}",
           new { id = RouteParameter.Optional });

        var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
        jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

    }
}