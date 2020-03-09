using System;

using ImageSourceHandlers.Forms.Sample.Profiler;

using Xamarin.Forms;

namespace ImageSourceHandlers.Forms.Sample.Views
{
	public partial class EdgeCasesPage : ProfilerPage
	{
		public EdgeCasesPage ()
		{
			InitializeComponent ();

			_stack.Children.Add (new Image {
				HeightRequest = 50,
				Source = Images.SourceById(1),
			});
			_stack.Children.Add (new Frame {
				Content = new Image {
					HeightRequest = 50,
					Source = Images.SourceById(5),
				},
			});
			_stack.Children.Add (new Image {
				Source = ImageSource.FromFile ("doesn't exist")
			});
			_stack.Children.Add (new Image {
				Source = ImageSource.FromUri (new Uri ("http://dontexist"))
			});
			_stack.Children.Add (new Image {
				Source = ImageSource.FromUri (new Uri ("http://jonathanpeppers.com/404"))
			});
			_stack.Children.Add (new Image {
				Source = ImageSource.FromResource ("doesn't exist", typeof (App)),
			});
			_stack.Children.Add (new Image {
				Source = ImageSource.FromStream (() => null)
			});
		}
	}
}