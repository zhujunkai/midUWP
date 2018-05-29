using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace medUWP.Models
{
	public class Emojitem
	{
		public BitmapImage emoji { get; set; }
		public string Description { get; set; }
		public Emojitem(BitmapImage image,string Description)
		{
			this.emoji = image;
			this.Description = Description;
		}
	}
}
