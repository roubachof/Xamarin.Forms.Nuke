using Xamarin.Forms;

namespace ImageSourceHandlers.Forms.Sample.Views
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage (new MainPage ());
		}
	}
}
