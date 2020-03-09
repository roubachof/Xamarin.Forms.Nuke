using Xamarin.Forms;

namespace ImageSourceHandlers.Forms.Sample.Views
{
	public partial class HugeImagePage
	{
		public HugeImagePage ()
		{
			InitializeComponent ();

			for (int i = 0; i < 100; i++) {
				var image = new Image {
					WidthRequest = 650,
					HeightRequest = 413,
					Source = ImageSource.FromFile ("trump.jpg")
				};
				_stack.Children.Add (image);
			}
		}
	}
}