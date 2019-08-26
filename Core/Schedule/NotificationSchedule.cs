using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Services.Scheduling;
using DotNetNuke.Common.Utilities;
using System.Xml;
using DotNetNuke.Entities.Portals;
using System.Collections;
using DotNetNuke.Entities.Modules;

namespace Playngo.Modules.ClientZone
{
    public class NotificationSchedule : DotNetNuke.Services.Scheduling.SchedulerClient
    {
        /// <summary>
        /// 默认构造
        /// </summary>
        public NotificationSchedule() { }

        public NotificationSchedule(DotNetNuke.Services.Scheduling.ScheduleHistoryItem objScheduleHistoryItem)
            : base()
        {
            this.ScheduleHistoryItem = objScheduleHistoryItem;
        }



        /// <summary>
        /// 调度器执行
        /// </summary>
        public override void DoWork()
        {
           

            try
            {
                this.Progressing();


       



                this.ScheduleHistoryItem.Succeeded = true;
                this.ScheduleHistoryItem.AddLogNote("Notification send completed.");
            }
            catch (Exception exc)
            {
                this.ScheduleHistoryItem.Succeeded = false;
                this.ScheduleHistoryItem.AddLogNote("Notification send failed." + exc.ToString());
                this.Errored(ref exc);
                DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);
            }

        }
 

         




        /// <summary>
        /// 更新调度器
        /// </summary>
        public void UpdateScheduler(BaseModule baseModule)
        {
            ScheduleItem objScheduleItem = SchedulingProvider.Instance().GetSchedule("Playngo.Modules.ClientZone.NotificationSchedule,Playngo.Modules.ClientZone", Null.NullString);
            if (!(objScheduleItem != null && objScheduleItem.ScheduleID > 0))
            {
                //这里需要创建新的调度器
                Int32 ScheduleID = AddScheduler(true,10,10,100);
                objScheduleItem = SchedulingProvider.Instance().GetSchedule(ScheduleID);
            }




        }





        public Int32 AddScheduler(Boolean Schedule_Enabled, Int32 Schedule_TimeLapse, Int32 Schedule_RetryFrequency, Int32 Schedule_History)
        {
            ScheduleItem item = new ScheduleItem();
            item.TypeFullName = "Playngo.Modules.ClientZone.NotificationSchedule,Playngo.Modules.ClientZone";
            item.TimeLapse = Schedule_TimeLapse;
            item.TimeLapseMeasurement = "m";
            item.RetryTimeLapse = Schedule_RetryFrequency;
            item.RetryTimeLapseMeasurement = "m";
            item.RetainHistoryNum = Schedule_History;
            item.CatchUpEnabled = false;
            item.Enabled = Schedule_Enabled;
            item.ObjectDependencies = "ClientZone_Notification_Scheduler";
            item.FriendlyName = "ClientZone Notification Scheduler";
            return AddScheduler(item);
            
        }

        /// <summary>
        /// 增加调度器
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Int32 AddScheduler(ScheduleItem item)
        {
            return SchedulingProvider.Instance().AddSchedule(item);
        }


 




    }
}