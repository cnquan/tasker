using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Tasker.TaskManager.DAL
{
    public class TaskDAL
    {
        private Util.SqlHelper dbHelper;

        public TaskDAL(string connectString)
        {
            dbHelper = new Util.SqlHelper(connectString);
        }

        public int UpdateTaskLastStartTime(int taskId, DateTime time)
        {
            string cmd = string.Format("UPDATE TB_TASK SET TaskLastStartTime=@TaskLastStartTime WHERE Id=@Id");
            IDataParameter[] param = {
                new SqlParameter("@TaskLastStartTime",SqlDbType.DateTime),
                new SqlParameter("@Id", SqlDbType.Int, 4)
            };
            param[0].Value = time;
            param[1].Value = taskId;
            try
            {
                int result = dbHelper.ExecuteSql(cmd, param);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UpdateTaskLastEndTime(int taskId, DateTime time)
        {
            string cmd = string.Format("UPDATE TB_TASK SET TaskLastEndTime=@TaskLastEndTime WHERE Id=@Id");
            IDataParameter[] param = {
                new SqlParameter("@TaskLastEndTime",SqlDbType.DateTime),
                new SqlParameter("@Id", SqlDbType.Int, 4)
            };
            param[0].Value = time;
            param[1].Value = taskId;
            try
            {
                int result = dbHelper.ExecuteSql(cmd, param);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UpdateTaskError(int taskId, DateTime time)
        {
            string cmd = "UPDATE TB_TASK SET TaskErrorCount=TaskErrorCount+1,TaskLastErrorTime=@TaskLastErrorTime where Id=@Id";
            IDataParameter[] param = {
                new SqlParameter("@TaskLastErrorTime",SqlDbType.DateTime),
                new SqlParameter("@Id",SqlDbType.Int,4)
            };
            param[0].Value = time;
            param[1].Value = taskId;
            try
            {
                return dbHelper.ExecuteSql(cmd, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UpdateTaskSuccess(int taskId)
        {
            string cmd = "UPDATE TB_TASK SET TaskRunCount=TaskRunCount+1 where Id=@Id";
            IDataParameter[] param = {
                new SqlParameter("@Id",SqlDbType.Int,4)
            };
            param[0].Value = taskId;
            try
            {
                return dbHelper.ExecuteSql(cmd, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
