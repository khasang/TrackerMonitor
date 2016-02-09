using System.Web;
using System.Web.Optimization;

namespace WebMVC
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Content/js/jquery-1.11.1.min.js",
                        "~/Content/bootstrap/js/bootstrap.min.js",
                        "~/Content/js/jquery.backstretch.min.js",
                        "~/Content/js/retina-1.1.0.min.js",
                        "~/Content/js/scripts.js",
                        "~/Content/js/jquery.main.js"));

            bundles.Add(new ScriptBundle("~/bundles/backgroundScript").Include(
                "~/Content/js/backgroundScript.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/stylebundle").Include(
                "~/Content/bootstrap/css/bootstrap.min.css",
                "~/Content/font-awesome/css/font-awesome.min.css",
                "~/Content/css/form-elements.css",
                "~/Content/css/style.css",
                "~/Content/css/all.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));



            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Content/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/privateareastyles").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap/css/bootstrap.min.css",
                      "~/Content/css/form-elements.css",
                      "~/Content/css/style.css",
                      "~/Content/font-awesome/css/font-awesome.min.css"));
        }
    }
}