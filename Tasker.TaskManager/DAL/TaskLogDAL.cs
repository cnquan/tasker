using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Tasker.TaskManager.DAL
{
    public class TaskLogDAL
    {
        private Util.SqlHelper dbHelper;

        public TaskLogDAL(string connectString)
        {
            dbHelper = new Util.SqlHelper(connectString);
        }

        public int Add(Model.TaskLog obj)
        {
            string cmd = @"insert into tb_log(Content,LogType,CreateTime,Task_Id,Node_Id)
                           values(@Content,@LogType,@CreateTime,@Task_Id,@Node_Id)";
            IDataParameter[] param = {
                new SqlParameter("@Content", SqlDbType.NVarChar, 8000),
                new SqlParameter("@LogType", SqlDbType.Int, 4),
                new SqlParameter("@CreateTime", SqlDbType.DateTime),
                new SqlParameter("@Task_Id", SqlDbType.Int, 4),
                new SqlParameter("@Node_Id",SqlDbType.Int,4)
            };
            param[0].Value = obj.Content;
            param[1].Value = obj.LogType;
            param[2].Value = obj.CreateTime;
            param[3].Value = obj.Task_Id;
            param[4].Value = obj.Node_Id;
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
