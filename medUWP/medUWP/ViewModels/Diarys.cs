using medUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medUWP.ViewModels
{
    public class Diarys
    {
		private static String DB_NAME = "SQLiteSample.db";
		private static String TABLE_NAME = "SampleTable";
		private static String SQL_CREATE_TABLE = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " (Id TEXT,Content TEXT,Description TEXT,Date TEXT,Image TEXT,Completed TEXT);";
		private static String SQL_QUERY_VALUE = "SELECT * FROM " + TABLE_NAME + " ;";
		private static String SQL_SEARCH = "SELECT * FROM " + TABLE_NAME + " WHERE Title LIKE ? OR Description LIKE ? OR Date LIKE ?;";
		private static String SQL_INSERT = "INSERT INTO " + TABLE_NAME + " VALUES(?,?,?,?,?,?);";
		private static String SQL_UPDATE = "UPDATE " + TABLE_NAME + " SET Title = ?,Description=?,Date=?,Image=? WHERE Id = ?;";
		private static String SQL_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Id = ?;";


		public ObservableCollection<Diaryitem> allItems = new ObservableCollection<Diaryitem>();
		public ObservableCollection<Diaryitem> AllItems { get { return this.allItems; } }
		public Diaryitem seletedItem = null;
	}
}
