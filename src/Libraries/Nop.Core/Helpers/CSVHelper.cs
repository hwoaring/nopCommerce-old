using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace Nop.Core.Helpers
{
    /// <summary>
    /// CSV文件操作
    /// </summary>
    public class CSVHelper
    {
        public static void SaveCSV(DataTable dt, string fullPath)
        {
            var fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(fs, Encoding.UTF8);
            //string data = string.Empty;
            var data = new StringBuilder();

            var column_count = dt.Columns.Count;
            var row_count = dt.Rows.Count;
            //写出列名称

            for (var i = 0; i < column_count; i++)
            {
                data.Append(dt.Columns[i].ColumnName);
                if (i < column_count - 1)
                {
                    data.Append(",");
                }
            }
            sw.WriteLine(data.ToString());
            //写出各行数据
            for (var i = 0; i < row_count; i++)
            {
                data.Clear();
                for (var j = 0; j < column_count; j++)
                {
                    var str = dt.Rows[i][j].ToString();
                    str = str.Replace("\"", "\"\"");                      //替换英文冒号 英文冒号需要换成两个冒号
                    if (str.Contains(",") || str.Contains("\"")
                        || str.Contains("\r") || str.Contains("\n"))      //含逗号 冒号 换行符的需要放到引号中
                    {
                        str = string.Format("\"{0}\"", str);
                    }

                    data.Append(str);
                    if (j < column_count - 1)
                    {
                        data.Append(",");
                    }
                }
                sw.WriteLine(data.ToString());
            }
            sw.Close();
            fs.Close();
        }
    }
}
