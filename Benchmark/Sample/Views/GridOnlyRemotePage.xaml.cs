using Xamarin.Forms;

namespace ImageSourceHandlers.Forms.Sample.Views
{
	public partial class GridOnlyRemotePage
	{
		public GridOnlyRemotePage ()
		{
			InitializeComponent ();

			for (int i = 0; i < 100; i++) {
				_grid.RowDefinitions.Add (new RowDefinition { Height = 50 });

				for (int j = 0; j < 4; j++) {
					var image = new Image {
						Source = Images.SourceById((i + j) % 3),
					};
					Grid.SetRow (image, i);
					Grid.SetColumn (image, j);
					_grid.Children.Add (image);
				}
			}
		}
	}
}