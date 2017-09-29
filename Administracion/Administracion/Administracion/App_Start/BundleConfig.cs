﻿using System.Web;
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
            

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/bootstrap-datetimepicker.min.css",                      
                      "~/Content/_reboot.scss",
                      "~/Content/base.default.css",
                      "~/Content/base.responsive.css",
                      "~/Content/fonts.css",
                      "~/Content/form-validation.css",
                      "~/Content/basic-form-elements.css",
                      "~/Content/Site.css"));
        }
    }
}
