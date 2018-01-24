using System.Web;
using System.Web.Optimization;

namespace Administracion
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/moment.min.js",                        
                        "~/Scripts/bootstrap-datetimepicker.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/tablesDatatables").Include(
                "~/Scripts/tablesDatatables.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                "~/Scripts/plugins.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/lightbox").Include(
                "~/Scripts/lightbox.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/bootstrap-datetimepicker.min.css",                      
                      //"~/Content/_reboot.scss",
                      "~/Content/base.default.css",
                      "~/Content/base.responsive.css",
                      "~/Content/fonts.css",
                      "~/Content/form-validation.css",
                      "~/Content/basic-form-elements.css",
                      "~/Content/dropdowns.css",
                      "~/Content/main.css",
                      "~/Content/plugins.css",
                      "~/Content/themes.css",
                      "~/Content/lightbox.min.css"
                      //"~/Content/Site.css"
                      ));
        }
    }
}
