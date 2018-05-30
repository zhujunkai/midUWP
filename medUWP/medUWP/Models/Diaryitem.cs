using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace medUWP.Models
{
    public class Diaryitem
    {
		public string Id { get; set; }
		public string DiaryContent { set; get; }
		public string food { set; get; }
		public DateTime date { get; set; }
		public BitmapImage image { set; get; }
		public int emojiindex { get; set; }
		public Diaryitem(string DiaryContent, string food, DateTime date, BitmapImage image,  int emojiindex)
		{
			this.Id = Guid.NewGuid().ToString();
			this.DiaryContent = DiaryContent;
			this.food = food;
			this.date = date;
			this.image = image;
			this.emojiindex = emojiindex;
		}
		public Diaryitem(string id,string DiaryContent, string food, DateTime date, BitmapImage image, int emojiindex)
		{
			this.Id =id;
			this.DiaryContent = DiaryContent;
			this.food = food;
			this.date = date;
			this.image = image;
			this.emojiindex = emojiindex;
		}
	}
}
