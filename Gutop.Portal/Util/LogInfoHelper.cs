﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gutop.Portal.Util
{
    public class LogInfoHelper:Model.Autofac.ISingleton
    {
        static System.Collections.Concurrent.ConcurrentQueue<Model.LogInfo> lstLogInfo = new System.Collections.Concurrent.ConcurrentQueue<Model.LogInfo>();
        static object logInfoLock = new object();
        static int initFlag = 0;
        Bll.LogInfo bllLogInfo;
        public LogInfoHelper(Bll.LogInfo bllLogInfo)
        {
            this.bllLogInfo = bllLogInfo;
        }
        

        public  void Add(Model.LogInfo entity)
        {
            lstLogInfo.Enqueue(entity);
        }

        public void StartPersist()
        {
            if (System.Threading.Interlocked.Exchange(ref initFlag, 1) != 0)
                return;//已经执行过下面的代码了，避免因为调用多次这个方法就启动多个线程处理日志数据
            System.Threading.ThreadPool.QueueUserWorkItem(x =>
            {
                while (true)
                {
                    if (lstLogInfo.Count <= 0)
                    {
                        System.Threading.Thread.Sleep(6 * 1000);
                        continue;
                    }
                    List<Model.LogInfo> lst = new List<Model.LogInfo>();
                    Model.LogInfo tmp;
                    while (lst.Count < 50 &&lstLogInfo.TryDequeue(out tmp))
                    {
                        lst.Add(tmp);
                    }
                    try
                    {
                        this.bllLogInfo.Add(lst);
                    }
                    catch (Exception ex)
                    {
                        //最后办法，写入本机文件中，或者windows事件日志中
                        System.Diagnostics.EventLog.WriteEntry("Gutop", "LogInfoHelper执行StartPersist保存日志信息到数据发送异常;异常明细信息如下" + ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
                    }
                }
            });
        }
    }
}