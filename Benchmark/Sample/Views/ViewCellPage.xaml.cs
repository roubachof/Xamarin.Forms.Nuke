using System.Linq;

namespace ImageSourceHandlers.Forms.Sample.Views
{
	public partial class ViewCellPage
	{
		public ViewCellPage ()
		{
			InitializeComponent ();

			BindingContext = Images.Sources().ToArray ();
		}
	}
}