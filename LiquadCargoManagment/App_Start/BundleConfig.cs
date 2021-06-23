using System.Web;
using System.Web.Optimization;

namespace LiquadCargoManagment
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));



            //ACCOUNTS

                bundles.Add(new StyleBundle("~/assets/accounts/cache/site").Include(
                       "~/assets/accounts/css/bootstrap.css",
                       //"~/assets/accounts/assets/css/themes.css",
                       //"~//assets/css/nifty.css",
                       //"~/assets/accounts/css/nifty-demo-icons.css",
                       "~/assets/accounts/plugins/pace/pace.min.css",
                       //"~/assets/accounts/css/nifty-demo.css",
                       "~/assets/accounts/plugins/font-awesome/css/font-awesome.min.css",
                       "~/assets/accounts/css/jquery-confirm.min.css",
                       "~/assets/accounts/css/summernote.min.css",
                       "~/assets/accounts/css/dropzone.min.css",
                       "~/assets/accounts/css/plugins.css",
                       "~/assets/accounts/css/main.css"

                ));

                bundles.Add(new StyleBundle("~/assets/accounts/cache/datatablestyle").Include(
                      "~/assets/accounts/plugins/datatables/media/css/dataTables.bootstrap.css",
                      "~/assets/accounts/plugins/datatables/extensions/Responsive/css/responsive.dataTables.min.css"
                ));
                bundles.Add(new StyleBundle("~/assets/accounts/cache/datepickerstyle").Include(
                     "~/assets/accounts/plugins/bootstrap-datepicker/bootstrap-datepicker.min.css"
                 ));
                bundles.Add(new StyleBundle("~/assets/accounts/cache/timepickerstyle").Include(
                    "~/assets/accounts/plugins/bootstrap-timepicker/bootstrap-timepicker.min.css"
                ));
                bundles.Add(new StyleBundle("~/assets/accounts/cache/chosen").Include(
                    "~/assets/accounts/plugins/chosen/chosen.min.css"
                ));
                bundles.Add(new StyleBundle("~/assets/accounts/cache/bootstrapselect").Include(
                    "~/assets/accounts/plugins/bootstrap-select/bootstrap-select.min.css"
                ));
                bundles.Add(new StyleBundle("~/assets/accounts/cache/select2style").Include(
                    "~/assets/accounts/plugins/select2/css/select2.min.css"
                ));
                bundles.Add(new ScriptBundle("~/assets/accounts/cache/jquery").Include(
                     "~/assets/accounts/js/jquery-3.3.1.js"
                ));
                bundles.Add(new ScriptBundle("~/assets/accounts/cache/core").Include(
                    "~/assets/accounts/js/bootstrap.js",
                    "~/assets/accounts/plugins/pace/pace.min.js",
                    "~/assets/accounts/js/nifty.js",
                    "~/assets/accounts/js/nifty-demo.js",
                    "~/assets/accounts/js/notify.js",
                    "~/assets/accounts/js/jquery-confirm.min.js",
                    "~/assets/accounts/js/autoNumeric.js",
                    "~/assets/accounts/js/jquery.validate.js",
                    "~/assets/accounts/js/jquery.unobtrusive-ajax.js",
                    "~/assets/accounts/js/jquery.validate.unobtrusive.js",
                     "~/assets/accounts/js/Arinvoice.js"
             

                ));
                bundles.Add(new ScriptBundle("~/assets/accounts/cache/datatablescript").Include(
                    "~/assets/accounts/plugins/datatables/media/js/jquery.dataTables.js",
                    "~/assets/accounts/plugins/datatables/media/js/dataTables.bootstrap.js",
                    "~/assets/accounts/plugins/datatables/extensions/Responsive/js/dataTables.responsive.min.js"
                ));
                bundles.Add(new ScriptBundle("~/assets/accounts/cache/datepickerscript").Include(
                    "~/assets/accounts/plugins/bootstrap-datepicker/bootstrap-datepicker.min.js"
                ));
                bundles.Add(new ScriptBundle("~/assets/accounts/cache/timepickerscript").Include(
                   "~/assets/accounts/plugins/bootstrap-timepicker/bootstrap-timepicker.min.js"
               ));
                bundles.Add(new ScriptBundle("~/assets/accounts/cache/chosen.min.js").Include(
                   "~/assets/accounts/plugins/chosen/chosen.jquery.min.js"
               ));
                bundles.Add(new ScriptBundle("~/assets/accounts/cache/select2script").Include(
                    "~/assets/accounts/plugins/select2/js/select2.min.js"
                ));
                bundles.Add(new ScriptBundle("~/assets/accounts/cache/summernote").Include(
                   "~/assets/accounts/plugins/summernote/summernote.min.js"
               ));
                bundles.Add(new ScriptBundle("~/assets/accounts/cache/dropzone").Include(
                  "~/assets/accounts/plugins/dropzone/dropzone.min.js"
              ));
                bundles.Add(new ScriptBundle("~/assets/accounts/cache/function").Include(
                     "~/assets/accounts/js/functions.js"
                ));
                bundles.Add(new ScriptBundle("~/assets/accounts/cache/component").Include(
                    "~/assets/accounts/js/form-component.js"
               ));
                bundles.Add(new ScriptBundle("~/assets/accounts/cache/fileupload").Include(
                   "~/assets/accounts/js/form-file-upload.js"
              ));

        }
    }
}
