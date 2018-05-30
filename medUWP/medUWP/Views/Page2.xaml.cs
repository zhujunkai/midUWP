using medUWP.Models;
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
using Windows.UI;
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
	public sealed partial class Page2 : Page
	{
		private bool[] has_item = new bool[40];
		private string[] Diarys_IDs = new string[40];
		private BitmapImage[] Bit_Images = new BitmapImage[40];
		public emojis Emojis;
		public Diarys my_Diarys;
		int now_month = 5;//显示的月份
		int now_day = 31;//加载的天数
		public Page2()
		{
			Emojis = ((App)App.Current).Emoji;
			my_Diarys = ((App)App.Current).my_Diarys;
			this.InitializeComponent();
			make_btn(31);
		}

		private void move(object sender, RoutedEventArgs e)
		{
			int i = 0;
			MainPage.NavMove(i);
		}
		private async void btn_Click(object sender, RoutedEventArgs e)
		{
			int position= int.Parse(((Button)sender).Name.Substring(6));
			if (has_item[position])
			{
				my_Diarys.SeleteDiaryitem(Diarys_IDs[position]);
				((App)App.Current).is_update = true;
				MainPage.NavMove(0);
			}
			else
			{
				var dialog = new MessageDialog("这天没写日记哟~", "查询结果").ShowAsync();
			}
		
		}
		private void Month_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var combo = (ComboBox)sender;
			var item = (ComboBoxItem)combo.SelectedItem;
			now_month = combo.SelectedIndex + 1;
			int day;
			if (now_month==1|| now_month == 3 || now_month ==3|| now_month == 7 || now_month ==8|| now_month ==10|| now_month == 12)
				day = 31;
			else if (now_month == 2)
				day = 28;
			else
				day = 30;
			calendar.Children.Clear();
			now_day = day;
			make_btn(now_day);
		}
		private async void make_btn(int days)
		{
			int month_selected = MonthCombo.SelectedIndex + 1;
			foreach (Diaryitem diary_item in my_Diarys.allItems)
			{
				if (diary_item.date.Month == month_selected)
				{
					int day_index = diary_item.date.Day - 1;
					has_item[day_index] = true;
					Diarys_IDs[day_index] = diary_item.Id;
					Bit_Images[day_index] =Emojis.AllItems[diary_item.emojiindex].emoji;
				}
			}
			for (int i = 0; i < days; i++)
			{
				int col = i % 8;
				int row = i / 8;
				Button myButton = new Button();
				myButton.Name = "Button";
				myButton.Name += i.ToString();
				myButton.Click += btn_Click;
				Grid.SetColumn(myButton, col);
				Grid.SetRow(myButton, 2 * row + 1);
				myButton.Height = 70;
				myButton.Width = 70;
				myButton.Opacity = 0;

				TextBlock myText = new TextBlock();
				myText.Text = (i + 1).ToString();
				myText.FontSize = 20;
				Grid.SetColumn(myText, col);
				Grid.SetRow(myText, 2 * row);

				Border myboader = new Border();
				myboader.Width = 70;
				myboader.Height = 70;
				Grid.SetColumn(myboader, col);
				Grid.SetRow(myboader, 2 * row + 1);
				SolidColorBrush border_brush = new SolidColorBrush(Colors.Orange);
				myboader.BorderBrush = border_brush;
				myboader.BorderThickness = new Thickness(2f);

				Image my_image = new Image();
				StorageFile file = await Package.Current.InstalledLocation.GetFileAsync("Assets\\blank.png");
				BitmapImage blank = new BitmapImage(new Uri(file.Path, UriKind.Absolute));
				my_image.Width = 70;
				my_image.Height = 70;
				Grid.SetColumn(my_image, col);
				Grid.SetRow(my_image, 2 * row + 1);
				if (has_item[i])
				{
					my_image.Source = Bit_Images[i];
				}
				else
				{
					my_image.Source = blank;
				}


				calendar.Children.Add(my_image);
				calendar.Children.Add(myboader);
				calendar.Children.Add(myButton);
				calendar.Children.Add(myText);
			}
		}
	}
}
