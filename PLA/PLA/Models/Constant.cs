using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;



namespace PLA.Models
{
    public class Constant
    {
        public const string List = "List";
        public const string CMS = "CMS";
        public const string Images = "Image";
        public const string Videos = "Video";
        public const string Subscribe = "Subscribe";
        public const string UnSubscribe = "Unsubscribe";
      //  public static string Path = HttpContext.Current.Server.MapPath("~/IFFImages/");
      //  public static string AdPath = HttpContext.Current.Server.MapPath("~/Advertisement/");
          public static string Path = "C://inetpub//wwwroot//IFF//IFFImages//";
          public static string AdPath = "C://inetpub//wwwroot//IFF//Advertisement//";
          public const string Focus = "Focus";
          public const string News = "News";
          public const string Conference = "Conference";
          public const string AdVideo = "Video";
          public const string MainNav = "Main Navigation";
          public const string SmallNav = "Small Navigation";
    }
}
