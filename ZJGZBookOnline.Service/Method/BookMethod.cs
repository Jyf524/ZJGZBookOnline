using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJGZBookOnline.Repository;
using ZJGZBookOnline.Repository.Entities;
using ZJGZBookOnline.Service.Interface;
using ZJGZBookOnline.Service.Model;

namespace ZJGZBookOnline.Service.Method
{
    public class BookMethod : BaseRespository, IBook
    {
        public String Add(BookViewPage BookAdd)
        {
            BookInfo BookInfo1 = new BookInfo();
            BookInfo1.BookID = DateTime.Now.ToString("yyyyMMddHHmmss");
            BookInfo1.BookPicture = BookAdd.BookPicture;
            BookInfo1.BookName = BookAdd.BookName;
            BookInfo1.BookAuthor = BookAdd.BookAuthor;
            BookInfo1.PublishDate = BookAdd.PublishDate;
            BookInfo1.BookISBN = BookAdd.BookISBN;
            BookInfo1.BookContent = BookAdd.BookContent;
            BookInfo1.BookNum = BookAdd.BookNum;
            BookInfo1.BookAddDate = DateTime.Now;

            db.BookInfo.Add(BookInfo1);
            db.SaveChanges();
            return "添加成功";
        }

        public String AddCheck(BookViewPage AddCheck)
        {
            if (AddCheck.BookPicture == "undefined")
            {
                return "请上传文件";
            }
            if (AddCheck.BookName == null)
            {
                return "请输入名称";
            }
            if (AddCheck.BookAuthor == null)
            {
                return "请输入作者";
            }
            if (AddCheck.PublishDate == null)
            {
                return "请选择日期";
            }
            if (AddCheck.BookISBN == null)
            {
                return "请输入ISBN号";
            }
            if (AddCheck.BookContent == null)
            {
                return "请输入简介";
            }
            if (AddCheck.BookNum == null)
            {
                return "请输入库存";
            }

            return "添加成功";
        }

        public BookViewPage Index(string searchString, string searchString2, int? page)
        {
            BookViewPage xx = new BookViewPage();
            var BookInfo = db.BookInfo.ToList();///

            int pageNumber;
            int pageSizeNum;
            int ItemNum;
            int pageNum;


            if (searchString != "")
            {
                BookInfo = BookInfo.Where(s => s.BookISBN.Contains(searchString)||s.BookName.Contains(searchString)).ToList();
            }
            if (searchString2 != "")
            {
                BookInfo = BookInfo.Where(s => s.ColumnID == Convert.ToInt32(searchString2)).ToList();
            }
            

            pageSizeNum = 5;//每页显示多少条
            ItemNum = BookInfo.Count();//数据总数
            pageNum = (ItemNum % pageSizeNum) == 0 ? (ItemNum / pageSizeNum) : (ItemNum / pageSizeNum) + 1;//总页数
            if (page == 4)
            {
                pageNumber = pageNum;
            }
            else
            {
                pageNumber = page ?? 1;
            }
            BookInfo = BookInfo.OrderBy(x => x.BookAddDate).Skip((pageNumber - 1) * pageSizeNum).Take(pageSizeNum).ToList();
            xx.aa = BookInfo;
            xx.pageNumber = pageNumber;
            xx.pageNumx = pageNum;
            xx.ItemNum = ItemNum.ToString();
            xx.searchstring = searchString;
            return xx;
        }

        public TypeViewPage TypeShow(string searchString)
        {
            TypeViewPage xx = new TypeViewPage();
            var TypeInfo = db.BookColumn.Where(a => a.ParentID == null||a.ParentID=="").ToList();
            var TypeSonInfo = db.BookColumn.Where(a => a.ParentID != null).ToList();
            if (searchString != "")
            {
                var TypeInfo1 =db.BookColumn.Where(a=>a.ColumnID==searchString).FirstOrDefault();
                TypeSonInfo = TypeSonInfo.Where(s => s.ParentID == TypeInfo1.ColumnID).ToList();
            }

            xx.aa = TypeInfo;
            xx.bb = TypeSonInfo;
            xx.searchstring = searchString;
            return xx;
        }
    }
}
