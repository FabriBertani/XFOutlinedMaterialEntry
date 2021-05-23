using System;
using AppKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;
using XFOutlinedMaterialEntry.MacOS.Effects;

[assembly: ResolutionGroupName("XFOutlinedMaterialEntry")]
[assembly: ExportEffect(typeof(RemoveEntryBordersEffect), nameof(RemoveEntryBordersEffect))]
namespace XFOutlinedMaterialEntry.MacOS.Effects
{
    public class RemoveEntryBordersEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var nsTextField = this.Control as NSTextField;

            if (nsTextField is null)
                throw new NotImplementedException();

            nsTextField.FocusRingType = NSFocusRingType.None;
        }

        protected override void OnDetached()
        {
        }
    }
}