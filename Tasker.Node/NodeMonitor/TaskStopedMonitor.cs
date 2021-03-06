﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Infrastructure.Unity;
using Tasker.ServiceContracts;
using Tasker.Node.NodeRuntime;
using Tasker.Infrastructure;

namespace Tasker.Node.NodeMonitor
{
    /// <summary>
    /// 任务异常停止监控
    /// </summary>
    public class TaskStopedMonitor : BaseMonitor
    {
        public override int Interval
        {
            get
            {
                return 1000 * 60;//一分钟间隔
            }
        }

        public override void Run()
        {
            try
            {
                Tools.ServiceSupport service = new Tools.ServiceSupport();
                List<int> tasks = service.NodeService.GetNodeTasks(GlobalConfig.NodeId, DataObject.Constants.TaskState.Running);
                List<int> stopTasks = new List<int>();
                foreach (var item in tasks)
                {
                    try
                    {
                        var info = TaskPoolManager.GetInstance().Get(item.ToString());
                        if (info == null)
                            stopTasks.Add(item);
                    }
                    catch (Exception ex)
                    {
                        service.LogService.AddNodeError(GlobalConfig.NodeId, "节点任务异常停止检测出现错误，", ex);
                    }
                }
                stopTasks.ForEach((c) =>
                {
                    service.LogService.AddTaskError(c, "任务[" + c + "]运行可能异常停止了", new Exception("任务处于运行状态，但是相应集群节点中，未发现任务在运行"));
                });
            }
            catch (Exception ex)
            {
                LogHelper.Write("节点[" + GlobalConfig.NodeId + "],节点任务异常停止检测出现错误,错误信息：" + ex.Message);
            }
        }
    }
}
