using Foundation;
using UIKit;

namespace XFOutlinedMaterialEntry.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Xamarin.Forms.FormsMaterial.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}