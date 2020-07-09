using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZJGZBookOnline.Service.Interface;
using ZJGZBookOnline.Service.Method;
using ZJGZBookOnline.Service.Model;

namespace ZJGZBookOnline.Controllers
{
    public class BookController : Controller
    {
        //
        // GET: /Book/

        public ActionResult BookIndex(HttpPostedFileBase file)
        {
            try
            {
                var fileName = file.FileName;
                var filePath = Server.MapPath(string.Format("~/{0}", "Files"));
                string path = Path.Combine(filePath, fileName);
                file.SaveAs(path);

                DataTable excelTable = new DataTable();
                excelTable = ImportExcel.GetExcelDataTable(path);

                DataTable dbdata = new DataTable();
                dbdata.Columns.Add("ColumnID");
                dbdata.Columns.Add("ColumnName");
                dbdata.Columns.Add("ParentID");

                for (int i = 0; i < excelTable.Rows.Count; i++)
                {
                    DataRow dr = excelTable.Rows[i];
                    DataRow dr_ = dbdata.NewRow();
                    dr_["ColumnID"] = dr["类别编号"];
                    dr_["ColumnName"] = dr["类别名称"];
                    dr_["ParentID"] = dr["所属父类ID "];
                    dbdata.Rows.Add(dr_);
                }
                RemoveEmpty(dbdata);

                string constr = System.Configuration.ConfigurationManager.AppSettings["meixinEntities_"];
                SqlBulkCopyByDatatable(constr, "BookColumn", dbdata);
            }
            catch
            {
                return View();
            }
            return View();
        }

        public static void SqlBulkCopyByDatatable(string connectionString, string TableName, DataTable dtSelect)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    try
                    {
                        sqlbulkcopy.DestinationTableName = TableName;
                        sqlbulkcopy.BatchSize = 20000;
                        sqlbulkcopy.BulkCopyTimeout = 0;//不限时间
                        for (int i = 0; i < dtSelect.Columns.Count; i++)
                        {
                            sqlbulkcopy.ColumnMappings.Add(dtSelect.Columns[i].ColumnName, dtSelect.Columns[i].ColumnName);
                        }
                        sqlbulkcopy.WriteToServer(dtSelect);
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public ActionResult BookIndex1(string searchString, string searchString2, int? page)
        {
            IBook IB = new BookMethod();
            return Json(IB.Index(searchString,searchString2, page), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Type(string searchString)
        {
            IBook IB = new BookMethod();
            return Json(IB.TypeShow(searchString), JsonRequestBehavior.AllowGet);
        }

        protected void RemoveEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool IsNull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        IsNull = false;
                    }
                }
                if (IsNull)
                {
                    removelist.Add(dt.Rows[i]);
                }
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
        }

        public ActionResult BookAdd()
        {
            return View();
        }

        int count = 0;
        public ActionResult BookAdd1(BookViewPage BookAdd)
        {
            IBook IB = new BookMethod();
            
            string fileSave = Server.MapPath("~/upload/");
            try
            {
                HttpFileCollectionBase file = Request.Files;
                if (file.Count != 0)
                {
                    for (int i = 0; i < file.Count; i++)
                    {
                        if (file.AllKeys[i] == "BookPicture")
                        {
                            HttpPostedFileBase file1 = file[i];
                            string extName = Path.GetExtension(file1.FileName);
                            string newName = Guid.NewGuid().ToString() + extName;
                            file1.SaveAs(Path.Combine(fileSave, newName));
                            BookAdd.BookPicture = "../upload/" + newName;
                        }
                    }
                }
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }


            if (IB.AddCheck(BookAdd) == "添加成功")
            {
                return Json(IB.Add(BookAdd), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(IB.AddCheck(BookAdd), JsonRequestBehavior.AllowGet);
            }

            

        }


    }
}
