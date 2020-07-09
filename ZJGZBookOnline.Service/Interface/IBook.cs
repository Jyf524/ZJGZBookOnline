using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJGZBookOnline.Service.Model;

namespace ZJGZBookOnline.Service.Interface
{
    public interface IBook
    {
        String Add(BookViewPage BookAdd);
        String AddCheck(BookViewPage AddCheck);
        BookViewPage Index(string searchString, string searchString2, int? page);
        TypeViewPage TypeShow(string searchString);
    }
}
