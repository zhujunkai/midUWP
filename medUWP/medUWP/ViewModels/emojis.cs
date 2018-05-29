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
			Addemojis();
		}
		public async void Addemojis()
		{
			StorageFile file = await Package.Current.InstalledLocation.GetFileAsync("Assets\\emoji-1.png");
			BitmapImage w_image = new BitmapImage(new Uri(file.Path, UriKind.Absolute));
			this.allItems.Add(new Emojitem(w_image,"love"));
		}
	}
}
