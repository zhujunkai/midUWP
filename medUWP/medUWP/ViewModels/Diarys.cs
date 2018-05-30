using medUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.AccessCache;

namespace medUWP.ViewModels
{
    public class Diarys
    {
		private static String DB_NAME = "MidSimple.db";
		private static String TABLE_NAME = "MidTable";
		private static String SQL_CREATE_TABLE = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " (Id TEXT,Content TEXT,Food TEXT,Date TEXT,Image TEXT,Emojiindex TEXT);";
		private static String SQL_QUERY_VALUE = "SELECT * FROM " + TABLE_NAME + " ;";
		private static String SQL_SEARCH = "SELECT * FROM " + TABLE_NAME + " WHERE Content LIKE ? OR Food LIKE ? OR Date LIKE ? OR Emojiindex LIKE ?;";
		private static String SQL_INSERT = "INSERT INTO " + TABLE_NAME + " VALUES(?,?,?,?,?,?);";
		private static String SQL_UPDATE = "UPDATE " + TABLE_NAME + " SET Content = ?,Food = ?,Date = ?,Image = ?,Emojiindex = ? WHERE Id = ?;";
		private static String SQL_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Id = ?;";


		public ObservableCollection<Diaryitem> allItems = new ObservableCollection<Diaryitem>();
		public ObservableCollection<Diaryitem> AllItems { get { return this.allItems; } }
		public Diaryitem seletedItem = null;

		SQLiteConnection _connection;
		public Diarys()
		{
			_connection = new SQLiteConnection(DB_NAME);
			using (var statement = _connection.Prepare(SQL_CREATE_TABLE))
			{
				statement.Step();
			}
		}
		public void AddDiaryitem(string DiaryContent, string food, DateTime date, BitmapImage image, int emojiindex)
		{
			this.allItems.Add(new Diaryitem(DiaryContent, food, date, image, emojiindex));
			Diaryitem temp = this.allItems.Last();
			using (var statement = _connection.Prepare(SQL_INSERT))
			{
				statement.Bind(1, temp.Id);
				statement.Bind(2, temp.DiaryContent);
				statement.Bind(3, temp.food);
				statement.Bind(4, temp.date.ToString());
				statement.Bind(5, temp.image.UriSource.ToString());
				statement.Bind(6, temp.emojiindex.ToString());
				statement.Step();
			}
		}
		public void RemoveDiaryitem(string id)
		{
			foreach (Diaryitem cache in allItems)
			{
				if (cache.Id == id)
				{
					seletedItem = cache;
					break;
				}
			}
			allItems.Remove(seletedItem);
			using (var statement = _connection.Prepare(SQL_DELETE))
			{
				statement.Bind(1, id);
				statement.Step();
			}
			seletedItem = null;
		}
		public void UpdateDiaryitem(string id, string DiaryContent, string food, DateTime date, BitmapImage image, int emojiindex)
		{
			foreach (Models.Diaryitem cache in allItems)
			{
				if (cache.Id == id)
				{
					seletedItem = cache;
					break;
				}
			}
			seletedItem.DiaryContent =DiaryContent;
			seletedItem.food = food;
			seletedItem.date = date;
			seletedItem.image = image;
			seletedItem.emojiindex =emojiindex;
			using (var statement = _connection.Prepare(SQL_UPDATE))
			{
				statement.Bind(1, DiaryContent);
				statement.Bind(2, food);
				statement.Bind(3, date.ToString());
				statement.Bind(4, image.UriSource.ToString());
				statement.Bind(5, emojiindex.ToString());
				statement.Bind(6, id);
				statement.Step();
			}
			seletedItem = null;
		}
		public void SeleteDiaryitem(string id)
		{
			foreach (Models.Diaryitem cache in allItems)
			{
				if (cache.Id == id)
				{
					seletedItem = cache;
					break;
				}
			}
		}
		public async void readFromfile()
		{
			using (var statement = _connection.Prepare(SQL_QUERY_VALUE))
			{
				while (SQLiteResult.ROW == statement.Step())
				{
					string id = statement[0] as String;
					string Content = statement[1] as String;
					string Food = statement[2] as String;
					string date = statement[3] as String;
					string image = statement[4] as String;
					int emojiindex= int.Parse(statement[5] as String);
					DateTime t_date = DateTime.Parse(date);
					StorageFile file = await Package.Current.InstalledLocation.GetFileAsync("Assets\\StoreLogo.png");
					BitmapImage w_image = new BitmapImage(new Uri(file.Path, UriKind.Absolute));
					if (image != "ms-appx:///Assets/StoreLogo.png")
					{
						var t_file = await StorageApplicationPermissions.FutureAccessList.GetFileAsync((string)ApplicationData.Current.LocalSettings.Values[image]);
						if (t_file != null)
						{
							file = t_file;
							w_image = new BitmapImage(new Uri(file.Path, UriKind.Absolute));
						}
					}
					this.allItems.Add(new Diaryitem(id, Content, Food, t_date, w_image, emojiindex));
				}
			}
		}


	}
}
