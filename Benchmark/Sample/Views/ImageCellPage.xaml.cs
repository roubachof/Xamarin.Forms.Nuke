using System.Linq;

namespace ImageSourceHandlers.Forms.Sample.Views
{
	public partial class ImageCellPage
	{
		public ImageCellPage ()
		{
			InitializeComponent ();

			BindingContext = Images.Sources(1000).ToArray ();
		}
	}
}