using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJGZBookOnline.Repository.Entities;

namespace ZJGZBookOnline.Service.Model
{
    public class TypeViewPage
    {
        public IEnumerable<BookColumn> aa { get; set; }
        public IEnumerable<BookColumn> bb { get; set; }
        public string searchstring;


        public string ColumnID { get; set; }
        public string UserName { get; set; }
        public string ColumnName { get; set; }
        public string ParentID { get; set; }
    }
}
