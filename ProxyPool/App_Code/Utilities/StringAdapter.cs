using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;


/// <summary>
/// StringAdapter 的摘要说明
/// </summary>
public class StringAdapter
{
    public StringAdapter()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region SQL字符处理  

    /// <summary>  
    /// 检测是否有Sql危险字符  
    /// </summary>  
    /// <param name="str">要判断字符串</param>  
    /// <returns>判断结果</returns>  
    public bool isSafeSqlString(string str)
    {
        return !Regex.IsMatch(str, @"[-|;|,|\/|||||\}|\{|%|@|\*|!|\']");
    }

    /// <summary>  
    /// 删除SQL注入特殊字符  
    /// </summary>  
    protected string stripSQLInjection(string sql)
    {
        if (!string.IsNullOrEmpty(sql))
        {
            //过滤 ' --  
            string pattern1 = @"(\%27)|(\')|(\-\-)";

            //防止执行 ' or  
            string pattern2 = @"((\%27)|(\'))\s*((\%6F)|o|(\%4F))((\%72)|r|(\%52))";

            //防止执行sql server 内部存储过程或扩展存储过程  
            string pattern3 = @"\s+exec(\s|\+)+(s|x)p\w+";

            sql = Regex.Replace(sql, pattern1, string.Empty, RegexOptions.IgnoreCase);
            sql = Regex.Replace(sql, pattern2, string.Empty, RegexOptions.IgnoreCase);
            sql = Regex.Replace(sql, pattern3, string.Empty, RegexOptions.IgnoreCase);
        }
        return sql;
    }

    public string sqlSafe(string sql)
    {
        sql = sql.ToLower();
        //sql = sql.Replace("'", "''");
        //Parameter = Parameter.Replace(">", ">");
        //Parameter = Parameter.Replace("<", "<");
        sql = sql.Replace("\n", "");
        //sql = sql.Replace("\0", ".");
        //sql = stripSQLInjection(sql);
        return sql;
    }

    /// <summary>  
    ///  判断是否有非法字符  
    /// </summary>  
    /// <param name="strString"></param>  
    /// <returns>返回TRUE表示有非法字符，返回FALSE表示没有非法字符。</returns>  
    protected static bool checkBadStr(string strString)
    {
        bool outValue = false;
        if (strString != null && strString.Length > 0)
        {
            string[] bidStrlist = new string[9];
            bidStrlist[0] = "'";
            bidStrlist[1] = ";";
            bidStrlist[2] = ":";
            bidStrlist[3] = "%";
            bidStrlist[4] = "@";
            bidStrlist[5] = "&";
            bidStrlist[6] = "#";
            bidStrlist[7] = "\"";
            bidStrlist[8] = "net user";
            bidStrlist[9] = "exec";
            bidStrlist[10] = "net localgroup";
            bidStrlist[11] = "select";
            bidStrlist[12] = "asc";
            bidStrlist[13] = "char";
            bidStrlist[14] = "mid";
            bidStrlist[15] = "insert";
            bidStrlist[19] = "order";
            bidStrlist[20] = "exec";
            bidStrlist[21] = "delete";
            bidStrlist[22] = "drop";
            bidStrlist[23] = "truncate";
            bidStrlist[24] = "xp_cmdshell";
            bidStrlist[25] = "<";
            bidStrlist[26] = ">";
            string tempStr = strString.ToLower();
            for (int i = 0; i < bidStrlist.Length; i++)
            {
                if (tempStr.IndexOf(bidStrlist[i]) != -1)
                //if (tempStr == bidStrlist[i])  
                {
                    outValue = true;
                    break;
                }
            }
        }
        return outValue;
    }

    #endregion
}