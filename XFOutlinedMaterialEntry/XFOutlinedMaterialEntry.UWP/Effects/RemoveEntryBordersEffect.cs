using System;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XFOutlinedMaterialEntry.UWP.Effects;

[assembly: ResolutionGroupName("XFOutlinedMaterialEntry")]
[assembly: ExportEffect(typeof(RemoveEntryBordersEffect), nameof(RemoveEntryBordersEffect))]
namespace XFOutlinedMaterialEntry.UWP.Effects
{
    public class RemoveEntryBordersEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var textBox = this.Control as TextBox;

            if (textBox is null)
                throw new NotSupportedException();

            textBox.BorderThickness = new Windows.UI.Xaml.Thickness(0);
            textBox.BorderBrush = Color.White.ToBrush();
        }

        protected override void OnDetached()
        {
        }
    }
}