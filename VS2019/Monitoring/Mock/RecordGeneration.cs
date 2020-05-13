using Monitoring.Model;
using System;
using System.Collections.Generic;

namespace Monitoring.Mock
{
    public class Entries
    {
        public static  List<MonitorRecord> GenerateSuccessRecords()
        {
            List<MonitorRecord> monitorRecords = new List<MonitorRecord>();
            for(int i= 1; i <= 5;i++)
            {
                MonitorRecord monitorRecord = new MonitorRecord();
                monitorRecord.AppID = i;
                monitorRecord.AppArea = $"Area {i}";
                monitorRecord.Information = $"Information for Area {i}";
                monitorRecord.ModifiedDate = DateTime.Now.AddDays(i * -1);
                monitorRecord.Error = false;

                monitorRecords.Add(monitorRecord);
            }

            return monitorRecords;
        }

        public static List<MonitorRecord> GenerateOneErrorRecord()
        {
            List<MonitorRecord> monitorRecords = new List<MonitorRecord>();
            for(int i = 1; i <= 4; i++)
            {
                MonitorRecord monitorRecord = new MonitorRecord();
                monitorRecord.AppID = i;
                monitorRecord.AppArea = $"Area {i}";
                monitorRecord.Information = $"Information for Area {i}";
                monitorRecord.ModifiedDate = DateTime.Now.AddDays(i * -1);
                monitorRecord.Error = false;

                monitorRecords.Add(monitorRecord);
            }

            MonitorRecord errorRecord = GenerateOneRecord(5, true);
            monitorRecords.Add(errorRecord);
            

            return monitorRecords;
        }

        public static MonitorRecord GenerateOneRecord(int i,bool createError = false)
        {
            MonitorRecord monitorRecord = new MonitorRecord();
            monitorRecord.AppID = i;
            monitorRecord.AppArea = $"Area {i}";
            monitorRecord.Information =createError? $"ERROR Information for Area {i}": $"Information for Area {i}";
            monitorRecord.ModifiedDate = DateTime.UtcNow.AddDays(i * -1);
            monitorRecord.Error = createError;

            return monitorRecord;
        }


    }
}
