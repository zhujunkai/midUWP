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
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
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
		public Diarys my_Diarys;
		FileOpenPicker openPicker = new FileOpenPicker();
		public StorageFile FilePicture = null;
		ImageSource the_source;
		public Page1()
		{
			openPicker.FileTypeFilter.Add(".jpg"); //过滤文件类型，目前只支持jpg, png,选择其他文件会报错。
			openPicker.FileTypeFilter.Add(".png");
			this.InitializeComponent();
			Emojis = ((App)App.Current).Emoji;
			my_Diarys = ((App)App.Current).my_Diarys;
			if (((App)App.Current).is_update)
			{
				save_button.Content = "Update";
				delete_button.Visibility = Visibility.Visible;
				t_diary.Text = my_Diarys.seletedItem.DiaryContent;
				t_food.Text = my_Diarys.seletedItem.food;
				t_date.Date = my_Diarys.seletedItem.date;
				t_photo.Source = my_Diarys.seletedItem.image;
				Emojichoose.SelectedItem = Emojis.AllItems[my_Diarys.seletedItem.emojiindex];
				((App)App.Current).is_update = false;
			}
			else
			{
				Emojichoose.SelectedItem = Emojis.AllItems.First();
				the_source = t_photo.Source;
			}

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
			t_date.Date = DateTime.Now;
			t_food.Text = "";
			t_diary.Text = "";
			StorageFile file = await Package.Current.InstalledLocation.GetFileAsync("Assets\\StoreLogo.png");
			t_photo.Source = new BitmapImage(new Uri(file.Path, UriKind.Absolute));
			the_source = t_photo.Source;
		}

		private void Save_click(object sender, RoutedEventArgs e)
		{
			if (t_diary.Text == "")
			{
				var dialog = new MessageDialog("Diary不能为空", "消息提示").ShowAsync();
			}
			else if (t_date.Date > DateTime.Today.AddDays(1))
			{
				var dialog = new MessageDialog("Date不能大于当前日期", "消息提示").ShowAsync();
			}
			else
			{
				if (save_button.Content == "Update")
				{
					my_Diarys.UpdateDiaryitem(my_Diarys.seletedItem.Id, t_diary.Text, t_food.Text, t_date.Date.DateTime, (BitmapImage)t_photo.Source, Emojichoose.SelectedIndex);
					var dialog = new MessageDialog("Update successfully", "消息提示").ShowAsync();
					if (FilePicture != null)
						ApplicationData.Current.LocalSettings.Values[((BitmapImage)t_photo.Source).UriSource.ToString()] = StorageApplicationPermissions.FutureAccessList.Add(FilePicture);
				}
				else
				{
					my_Diarys.AddDiaryitem(t_diary.Text, t_food.Text, t_date.Date.DateTime, (BitmapImage)t_photo.Source, Emojichoose.SelectedIndex);
					var dialog = new MessageDialog("Create successfully", "消息提示").ShowAsync();
					if (FilePicture != null)
						ApplicationData.Current.LocalSettings.Values[((BitmapImage)t_photo.Source).UriSource.ToString()] = StorageApplicationPermissions.FutureAccessList.Add(FilePicture);
				}
				//Initialization();
				//((App)App.Current).UpdatePrimaryTile();
				MainPage.NavMove(1);
			}
		}

		private void delete_button_Click(object sender, RoutedEventArgs e)
		{
			my_Diarys.RemoveDiaryitem(my_Diarys.seletedItem.Id);
			MainPage.NavMove(1);
			//Initialization();
			//((App)App.Current).UpdatePrimaryTile();
		}
	}
}
