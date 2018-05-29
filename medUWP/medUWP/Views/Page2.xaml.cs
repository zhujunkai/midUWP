using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace medUWP.Views
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class Page2 : Page
	{
		public Page2()
		{
			this.InitializeComponent();
			for(int i = 0; i < 30; i++)
			{
				int col = i % 8;
				int row = i / 8;
				Button myButton = new Button();
				myButton.Foreground = new SolidColorBrush(Colors.Transparent);
				myButton.Name = "Button";
				if (i < 10) myButton.Name += "0";
				myButton.Name += i.ToString();
				myButton.Click += btn_Click;
				Grid.SetColumn(myButton, col);
				Grid.SetRow(myButton, 2*row+1);
				myButton.Height = 70;
				myButton.Width = 70;

				TextBlock myText = new TextBlock();
				myText.Text = (i+1).ToString();
				myText.FontSize = 20;
				Grid.SetColumn(myText, col);
				Grid.SetRow(myText, 2 * row );

				calendar.Children.Add(myButton);
				calendar.Children.Add(myText);
			}
		}

		private void move(object sender, RoutedEventArgs e)
		{
			int i = 0;
			MainPage.NavMove(i);
		}
		private async void btn_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new MessageDialog("1", "查询结果").ShowAsync();
		}
	}
}
