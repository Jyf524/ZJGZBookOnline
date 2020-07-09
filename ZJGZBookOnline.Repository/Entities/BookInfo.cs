using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZJGZBookOnline.Repository.Entities
{
    [Table("BookInfo")]
    public  class BookInfo
    {
        [Key]
        public string BookID { get; set; }
        public string BookPicture { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
        public string BookISBN { get; set; }
        public string BookContent { get; set; }
        public int BookNum { get; set; }
        public Nullable<System.DateTime> BookAddDate { get; set; }
        public int ColumnID { get; set; }
    }
}
