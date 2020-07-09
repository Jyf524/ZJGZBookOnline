using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJGZBookOnline.Repository.Entities;

namespace ZJGZBookOnline.Repository.Constaint
{
    public partial class DataContent : DbContext
    {
        public DataContent()
            : base("DataContent")
        {
        }
        public DbSet<BookColumn> BookColumn { get; set; }
        public DbSet<BookInfo> BookInfo { get; set; }
    }
}
