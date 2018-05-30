using medUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using Windows.UI.Xaml.Media.Imaging;

namespace medUWP.ViewModels
{
    public class Diarys
    {
		private static String DB_NAME = "SQLiteSample.db";
		private static String TABLE_NAME = "SampleTable";
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

		}
		public void AddDiaryitem(string DiaryContent, string food, DateTime date, BitmapImage image, int emojiindex)
		{
			this.allItems.Add(new Diaryitem(DiaryContent, food, date, image, emojiindex));
			Diaryitem temp = this.allItems.Last();
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

	}
}
