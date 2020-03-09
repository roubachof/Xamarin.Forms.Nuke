using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImageSourceHandlers.Forms.Sample.Views
{
	[XamlCompilation (XamlCompilationOptions.Compile)]
	public partial class ToggleSourcePage : ContentPage
	{
		public ToggleSourcePage ()
		{
			InitializeComponent ();
		}

		private void Button_Clicked (object sender, EventArgs e)
		{
			var source = Images.SourceById(2);
			_image.Source = source;
			_label.Text = source.ToString ();
		}
	}
}