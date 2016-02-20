using System.Web;
using System.Web.Optimization;
using System.Web.Optimization.React;

namespace WebMVC
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/reactjs").Include(
                      "~/Scripts/react.js",
                      "~/Scripts/react-dom.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/bundles/landing").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/landing.css"));

            bundles.Add(new BabelBundle("~/bundles/main").Include(
                "~/Content/app/trackerSection.jsx",
                "~/Content/app/trackerList.jsx",
                "~/Content/app/trackerEditForm.jsx",
                "~/Content/app/userProfileForm.jsx",
                "~/Content/app/inputError.jsx",
                "~/Content/app/textInput.jsx",
                "~/Content/app/trackerListPanel.jsx",
                "~/Content/app/userProfile.jsx",
                "~/Content/app/googleMap.jsx",
                "~/Content/app/mainControlPanel.jsx",
                "~/Content/app/mainInfoPanel.jsx",
                "~/Content/app/app.jsx",
                "~/Content/app/main.jsx"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
