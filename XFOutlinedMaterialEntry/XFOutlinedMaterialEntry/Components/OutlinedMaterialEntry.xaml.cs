using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XFOutlinedMaterialEntry.Components
{
    public partial class OutlinedMaterialEntry : Grid
    {
        private ImageSource tempIcon;

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(OutlinedMaterialEntry),
            default(string),
            BindingMode.TwoWay,
            null,
            (bindable, oldValue, newValue) =>
            {
                var view = (OutlinedMaterialEntry)bindable;

                view.customEntry.Text = (string)newValue;
            }
        );

        public static readonly BindableProperty PlaceholderTextProperty = BindableProperty.Create(
            nameof(PlaceholderText),
            typeof(string),
            typeof(OutlinedMaterialEntry),
            default(string),
            BindingMode.OneWay,
            null,
            (bindable, oldValue, newValue) =>
            {
                var view = (OutlinedMaterialEntry)bindable;

                view.placeholderText.Text = (string)newValue;
            }
        );

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(
            nameof(HelperText),
            typeof(string),
            typeof(OutlinedMaterialEntry),
            default(string),
            BindingMode.OneWay,
            null,
            (bindable, oldValue, newValue) =>
            {
                var view = (OutlinedMaterialEntry)bindable;

                view.helperText.Text = (string)newValue;

                if (view.errorText.IsVisible)
                    view.helperText.IsVisible = false;
                else
                    view.helperText.IsVisible = !string.IsNullOrEmpty(view.helperText.Text);
            }
        );

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
            nameof(ErrorText),
            typeof(string),
            typeof(OutlinedMaterialEntry),
            default(string),
            BindingMode.OneWay,
            null,
            (bindable, oldValue, newValue) =>
            {
                var view = (OutlinedMaterialEntry)bindable;

                view.errorText.Text = (string)newValue;
            }
        );

        public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(
            nameof(LeadingIcon),
            typeof(ImageSource),
            typeof(OutlinedMaterialEntry),
            default(ImageSource),
            BindingMode.OneWay,
            null,
            (bindable, oldValue, newValue) =>
            {
                var view = (OutlinedMaterialEntry)bindable;

                view.leadingIcon.Source = (ImageSource)newValue;

                view.leadingIcon.IsVisible = !view.leadingIcon.Source.IsEmpty;
            }
        );

        public static readonly BindableProperty TrailingIconProperty = BindableProperty.Create(
            nameof(TrailingIcon),
            typeof(ImageSource),
            typeof(OutlinedMaterialEntry),
            default(ImageSource),
            BindingMode.OneWay,
            null,
            (bindable, oldValue, newValue) =>
            {
                var view = (OutlinedMaterialEntry)bindable;

                view.trailingIcon.Source = (ImageSource)newValue;

                view.trailingIcon.IsVisible = view.trailingIcon.Source != null;
            }
        );

        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(
            nameof(HasError),
            typeof(bool),
            typeof(OutlinedMaterialEntry),
            default(bool),
            BindingMode.OneWay,
            null,
            (bindable, oldValue, newValue) =>
            {
                var view = (OutlinedMaterialEntry)bindable;

                view.errorText.IsVisible = (bool)newValue;

                view.containerFrame.BorderColor = view.errorText.IsVisible ? Color.Red : Color.Black;

                view.helperText.IsVisible = !view.errorText.IsVisible;

                view.placeholderText.TextColor = view.errorText.IsVisible ? Color.Red : Color.Gray;

                view.PlaceholderText = view.errorText.IsVisible ? $"{view.PlaceholderText}*" : view.PlaceholderText;

                if (view.TrailingIcon != null && !view.TrailingIcon.IsEmpty)
                    view.tempIcon = view.TrailingIcon;

                view.TrailingIcon = view.errorText.IsVisible
                    ? ImageSource.FromFile("ic_error.png")
                    : view.tempIcon;

                view.trailingIcon.IsVisible = view.errorText.IsVisible;
            }
        );

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            nameof(IsPassword),
            typeof(bool),
            typeof(OutlinedMaterialEntry),
            default(bool),
            BindingMode.OneWay,
            null,
            (bindable, oldValue, newValue) =>
            {
                var view = (OutlinedMaterialEntry)bindable;

                view.customEntry.IsPassword = (bool)newValue;

                view.passwordIcon.IsVisible = (bool)newValue;
            }
        );

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
            nameof(MaxLength),
            typeof(int),
            typeof(OutlinedMaterialEntry),
            default(int),
            BindingMode.OneWay,
            null,
            (bindable, oldValue, newValue) =>
            {
                var view = (OutlinedMaterialEntry)bindable;

                view.customEntry.MaxLength = (int)newValue;

                view.charCounterText.IsVisible = view.customEntry.MaxLength > 0;

                view.charCounterText.Text = $"0 / {view.MaxLength}";
            }
        );

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(OutlinedMaterialEntry),
            Color.Blue,
            BindingMode.OneWay            
        );

        public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(
            nameof(ReturnCommand),
            typeof(ICommand),
            typeof(OutlinedMaterialEntry),
            default(ICommand),
            BindingMode.OneWay,
            null,
            (bindable, oldValue, newValue) =>
            {
                var view = (OutlinedMaterialEntry)bindable;

                view.customEntry.ReturnCommand = (ICommand)newValue;
            }
        );

        public OutlinedMaterialEntry()
        {
            InitializeComponent();

            this.customEntry.Text = this.Text;

            this.customEntry.TextChanged += this.OnCustomEntryTextChanged;

            this.customEntry.Completed += this.OnCustomEntryCompleted;
        }

        public event EventHandler<EventArgs> EntryCompleted;

        public event EventHandler<TextChangedEventArgs> TextChanged;

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        public string HelperText
        {
            get => (string)GetValue(HelperTextProperty);
            set => SetValue(HelperTextProperty, value);
        }

        public string ErrorText
        {
            get => (string)GetValue(ErrorTextProperty);
            set => SetValue(ErrorTextProperty, value);
        }

        public ImageSource LeadingIcon
        {
            get => (ImageSource)GetValue(LeadingIconProperty);
            set => SetValue(LeadingIconProperty, value);
        }

        public ImageSource TrailingIcon
        {
            get => (ImageSource)GetValue(TrailingIconProperty);
            set => SetValue(TrailingIconProperty, value);
        }

        public bool HasError
        {
            get => (bool)GetValue(HasErrorProperty);
            set => SetValue(HasErrorProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public Keyboard Keyboard
        {
            set => this.customEntry.Keyboard = value;
        }

        public ReturnType ReturnType
        {
            set => this.customEntry.ReturnType = value;
        }

        public ICommand ReturnCommand
        {
            get => (ICommand)GetValue(ReturnCommandProperty);
            set => SetValue(ReturnCommandProperty, value);
        }

        private async Task ControlFocused()
        {
            if (string.IsNullOrEmpty(this.customEntry.Text) || this.customEntry.Text.Length > 0)
            {
                this.customEntry.Focus();

                this.containerFrame.BorderColor = this.HasError ? Color.Red : this.BorderColor;
                this.placeholderText.TextColor = this.HasError ? Color.Red : this.BorderColor;

                int y = DeviceInfo.Platform == DevicePlatform.UWP ? -25 : -20;

                await this.placeholderContainer.TranslateTo(0, y, 100, Easing.Linear);

                this.placeholderContainer.HorizontalOptions = LayoutOptions.Start;
                this.placeholderText.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            }
            else
                await this.ControlUnfocused();
        }

        private async Task ControlUnfocused()
        {
            this.containerFrame.BorderColor = this.HasError ? Color.Red : Color.Black;
            this.placeholderText.TextColor = this.HasError ? Color.Red : Color.Gray;

            this.customEntry.Unfocus();

            if (string.IsNullOrEmpty(this.customEntry.Text) || this.customEntry.MaxLength <= 0)
            {
                await this.placeholderContainer.TranslateTo(0, 0, 100, Easing.Linear);
                
                this.placeholderContainer.HorizontalOptions = LayoutOptions.FillAndExpand;
                this.placeholderText.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            }
        }

        private void CustomEntryFocused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
                MainThread.BeginInvokeOnMainThread(async () => await this.ControlFocused());
        }

        private void CustomEntryUnfocused(object sender, FocusEventArgs e)
        {
            if (!e.IsFocused)
                MainThread.BeginInvokeOnMainThread(async () => await this.ControlUnfocused());
        }

        private void OutlinedMaterialEntryTapped(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () => await this.ControlFocused());
        }

        private void PasswordEyeTapped(object sender, EventArgs e)
        {
            this.customEntry.IsPassword = !this.customEntry.IsPassword;
        }

        private void OnCustomEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            this.Text = e.NewTextValue;

            if (this.charCounterText.IsVisible)
                this.charCounterText.Text = $"{this.customEntry.Text.Length} / {this.MaxLength}";

            this.TextChanged?.Invoke(this, e);
        }

        private void OnCustomEntryCompleted(object sender, EventArgs e)
        {
            this.EntryCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}