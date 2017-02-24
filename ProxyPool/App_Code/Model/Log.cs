using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ProxyPool
{
    /// <summary>
    /// Log 的摘要说明
    /// </summary>
    public class Log
    {
        #region Parameters
        private int id;
        private int poolId;
        private int proxySourceId;
        private int proxyId;
        private int validationId;
        private DateTime logTime;
        private string description;
        #endregion

        #region Attributes
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public int PoolId
        {
            get
            {
                return poolId;
            }

            set
            {
                poolId = value;
            }
        }

        public int ProxySourceId
        {
            get
            {
                return proxySourceId;
            }

            set
            {
                proxySourceId = value;
            }
        }

        public int ProxyId
        {
            get
            {
                return proxyId;
            }

            set
            {
                proxyId = value;
            }
        }

        public int ValidationId
        {
            get
            {
                return validationId;
            }

            set
            {
                validationId = value;
            }
        }
        public DateTime LogTime
        {
            get
            {
                return logTime;
            }

            set
            {
                logTime = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }
        #endregion

        #region 构造函数
        public Log()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public Log(DateTime lTime, string lDescription, int l_Id = 0, int pool_Id = 0, int pSrc_Id = 0, int pxy_Id = 0, int vld_Id = 0)
        {
            id = l_Id;
            logTime = lTime;
            description = lDescription;
            poolId = pool_Id;
            proxySourceId = pSrc_Id;
            proxyId = pxy_Id;
            validationId = vld_Id;
        }
        #endregion

        public int saveLog()
        {
            string sql = "insert into tb_log (log_time,pool_id,pxy_src_id,pxy_id,vld_id,log_description]) values ("
                + "'" + logTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'," + poolId + "," + proxySourceId + "," + proxyId + "," + validationId + ",'" + description + "')";
            sql = new StringAdapter().sqlSafe(sql);
            return RunningCathe.DbHelper.ExecNonQuery(sql);
        }
    }
}