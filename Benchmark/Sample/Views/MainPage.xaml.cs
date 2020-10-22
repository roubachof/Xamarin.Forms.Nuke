using System;

using ImageSourceHandlers.Forms.Sample.Profiler;

using Xamarin.Forms;

namespace ImageSourceHandlers.Forms.Sample.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
        {
            Title = $"{MemoryProfiler.Instance.Name} Benchmark";
			InitializeComponent ();
        }

        async void GridOnlyRemote_Clicked (object sender, EventArgs e)
        {
            await Navigation.PushAsync (new GridOnlyRemotePage ());
        }
        async void Grid_Clicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new GridPage ());
		}
		async void Edge_Clicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new EdgeCasesPage ());
		}

		async void ViewCell_Clicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new ViewCellPage ());
		}

		async void ImageCell_Clicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new ImageCellPage ());
		}

		async void HugeImage_Clicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new HugeImagePage ());
		}

		async void ToggleImages_Clicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new ToggleSourcePage ());
		}

		async void ToggleImagesMaterial_Clicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new ToggleSourcePage());
		}

        private void ClearCache_Clicked(object sender, EventArgs e)
        {
            MemoryProfiler.Instance.ClearCache();
        }

        private void CheckPoint_Clicked(object sender, EventArgs e)
        {
            MemoryProfiler.Instance.Reset();
        }
    }
}
