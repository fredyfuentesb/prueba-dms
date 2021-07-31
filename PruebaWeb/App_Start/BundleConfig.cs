using System.Web;
using System.Web.Optimization;

namespace PruebaWeb
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/adminlte").Include(
                        "~/Scripts/bootstrap/js/bootstrap.bundle.min.js",
                        "~/Scripts/js/adminlte.min.js"));
            /*
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            */
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/adminlte.min.css",
                      "~/Content/fontawesome-free/css/all.min.css"));
        }
    }
}
