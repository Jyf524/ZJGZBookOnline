using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJGZBookOnline.Repository.Entities;

namespace ZJGZBookOnline.Service.Model
{
    public class BookViewPage
    {
        public IEnumerable<BookInfo> aa { get; set; }
        public int pageNumber;
        public int pageNumx;
        public string ItemNum;
        public string searchstring;


        public string BookID { get; set; }
        public string BookPicture { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
        public string BookISBN { get; set; }
        public string BookContent { get; set; }
        public int BookNum { get; set; }
        public Nullable<System.DateTime> BookAddDate { get; set; }
    }
}
