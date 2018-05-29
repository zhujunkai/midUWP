using medUWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace medUWP.Views
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class Page1 : Page
	{
		public emojis Emojis;
		FileOpenPicker openPicker = new FileOpenPicker();
		public StorageFile FilePicture = null;
		ImageSource the_source;
		public Page1()
		{
			openPicker.FileTypeFilter.Add(".jpg"); //过滤文件类型，目前只支持jpg, png,选择其他文件会报错。
			openPicker.FileTypeFilter.Add(".png");
			this.InitializeComponent();
			Emojis = ((App)App.Current).Emoji;
			Emojichoose.SelectedItem = Emojis.AllItems.First();
			the_source = t_photo.Source;
		}

		private async void img_click(object sender, RoutedEventArgs e)
		{
			UploadPicture();
		}
		public async void UploadPicture()
		{
			FilePicture = await openPicker.PickSingleFileAsync();
			if (FilePicture != null)
			{
				t_photo.Source = new BitmapImage(new Uri(FilePicture.Path, UriKind.Absolute));
			}
		}

		private async void Clear_click(object sender, RoutedEventArgs e)
		{
			StorageFile file = await Package.Current.InstalledLocation.GetFileAsync("Assets\\StoreLogo.png");
		}
	}
}
