using System.Web.Optimization;

namespace IdentitySample
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery.min.js",
                "~/Scripts/jquery.scrollex.min.js",
                "~/Scripts/jquery.scrolly.min.js",
                "~/Scripts/main.js",
                "~/Scripts/skel.min.js",
                "~/Scripts/util.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/main.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap.min.css"
                ));
        }
    }
}
