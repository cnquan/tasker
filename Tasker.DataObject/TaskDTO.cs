using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.DataObject
{
    public class TaskDTO
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务类别
        /// </summary>
        public TaskCategoryDTO Category { get; set; }

        /// <summary>
        /// 宿主节点
        /// </summary>
        public NodeDTO Node { get; set; }

        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime? TaskCreateTime { get; set; }

        /// <summary>
        /// 任务更新时间
        /// </summary>
        public DateTime? TaskUpdateTime { get; set; }

        /// <summary>
        /// 任务最近一次开始时间
        /// </summary>
        public DateTime? TaskLastStartTime { get; set; }

        /// <summary>
        /// 任务最近一次结束时间
        /// </summary>
        public DateTime? TaskLastEndTime { get; set; }

        /// <summary>
        /// 任务最近一次错误时间
        /// </summary>
        public DateTime? TaskLastErrorTime { get; set; }

        /// <summary>
        /// 任务运行次数
        /// </summary>
        public int? TaskRunCount { get; set; }

        /// <summary>
        /// 任务失败次数
        /// </summary>
        public int? TaskErrorCount { get; set; }

        /// <summary>
        /// 任务执行状态
        /// </summary>
        public int? TaskState { get; set; }

        /// <summary>
        /// 任务版本号
        /// </summary>
        public TaskVersionDTO TaskVersion { get; set; }

        /// <summary>
        /// 任务配置
        /// </summary>
        public string TaskConfiguration { get; set; }

        /// <summary>
        /// 任务执行频率Cron表达式
        /// </summary>
        public string TaskCron { get; set; }

        /// <summary>
        /// 任务入口DLL文件名
        /// </summary>
        public string TaskMainClassDLLName { get; set; }

        /// <summary>
        /// 任务入口函数命名空间
        /// </summary>
        public string TaskMainClassNameSpace { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public UserDTO Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
