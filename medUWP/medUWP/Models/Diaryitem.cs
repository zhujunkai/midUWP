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
		public string DiaryContent { set; get; }
		public string food { set; get; }
		public DateTime Date { get; set; }
		public BitmapImage Image { set; get; }
		public int emojiindex { get; set; }
		public Diaryitem(string DiaryContent, string food, DateTime Date, BitmapImage Image,  int emojiindex)
		{
			this.DiaryContent = DiaryContent;
			this.food = food;
			this.Date = Date;
			this.Image = Image;
			this.emojiindex = emojiindex;
		}
	}
}
