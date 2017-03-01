using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.IO;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private const long Iterations = long.MaxValue;

        private const string ClassString = "Class-";

        private string ObjectString = "Object-";

        protected void Application_Start()
        {
            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            HostingEnvironment.QueueBackgroundWorkItem(ct => DoWorkAsync("Method-"));
        }

        private async Task DoWorkAsync(string methodString)
        {
            await Task.Run(() =>
            {
                var appData = HttpRuntime.AppDomainAppPath;
                var path = Path.Combine(appData, "foo.txt");
                File.Decrypt(path);
                for (long i = 0; i < Iterations; i = i + 1)
                {
                    File.AppendAllText(path, ClassString + ObjectString + methodString + i.ToString() + Environment.NewLine);
                }
            });
        }
    }
}
