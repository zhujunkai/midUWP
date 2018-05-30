using medUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace medUWP.ViewModels
{
	public class emojis
	{
		public ObservableCollection<Emojitem> allItems = new ObservableCollection<Emojitem>();
		public ObservableCollection<Emojitem> AllItems { get { return this.allItems; } }
		public emojis()
		{
		}
		public async void Addemojis()
		{
			StorageFile file = await Package.Current.InstalledLocation.GetFileAsync("Assets\\emoji-1.png");
			BitmapImage w_image = new BitmapImage(new Uri(file.Path, UriKind.Absolute));
			this.allItems.Add(new Emojitem(w_image,"Love"));
			StorageFile file1 = await Package.Current.InstalledLocation.GetFileAsync("Assets\\emoji-2.png");
			BitmapImage w_image2 = new BitmapImage(new Uri(file1.Path, UriKind.Absolute));
			this.allItems.Add(new Emojitem(w_image2, "Happy"));
			StorageFile file2 = await Package.Current.InstalledLocation.GetFileAsync("Assets\\emoji-3.png");
			BitmapImage w_image3 = new BitmapImage(new Uri(file2.Path, UriKind.Absolute));
			this.allItems.Add(new Emojitem(w_image3, "Crazy"));
		}
	}
}
