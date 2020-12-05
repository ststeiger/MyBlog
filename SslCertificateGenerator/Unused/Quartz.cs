
using Quartz; // For WithSimpleSchedule 


// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/using-quartz.html
// https://www.quartz-scheduler.net/documentation/quartz-3.x/quick-start.html#trying-out-the-application-and-adding-jobs
// https://github.com/ststeiger/CronSharp
// https://github.com/kevincolyar/CronNET



// http://www.dotnetspeak.com/net-core/creating-schedule-driven-windows-service-in-net-core-3-0/
// https://andrewlock.net/new-in-aspnetcore-3-structured-logging-for-startup-messages/
// https://andrewlock.net/suppressing-the-startup-and-shutdown-messages-in-asp-net-core/
namespace RamMonitor
{
    
    
    public class Job1 
        :  Quartz.IJob
    {
        public async System.Threading.Tasks.Task Execute( Quartz.IJobExecutionContext context)
        {
            await System.IO.File.AppendAllLinesAsync(@"c:\temp\job1.txt", new[] { System.DateTime.Now.ToLongTimeString() });
        }
    }
    
    public class Job2 
    :  Quartz.IJob
    {
        public async System.Threading.Tasks.Task Execute( Quartz.IJobExecutionContext context)
        {
            await System.IO.File.AppendAllLinesAsync(@"c:\temp\job2.txt", new[] { System.DateTime.Now.ToLongTimeString() });
        }
    }



    public class sch
    {
        private Quartz.Impl.StdSchedulerFactory _schedulerFactory;
        private  Quartz.IScheduler _scheduler;
        private System.Threading.CancellationToken _stopppingToken;
        
        
        public sch()
        { } // End Constructor 
        
        
        protected async System.Threading.Tasks.Task StartJobs(System.Threading.CancellationToken stoppingToken)
        {
            this._stopppingToken = stoppingToken;
            this._schedulerFactory = new Quartz.Impl.StdSchedulerFactory();
            this._scheduler = await this._schedulerFactory.GetScheduler();
            await this._scheduler.Start();
            
            Quartz.IJobDetail job1 =  Quartz.JobBuilder.Create<Job1>()
                .WithIdentity("job1", "gtoup")
                .Build();
            
            Quartz.ITrigger trigger1 =  Quartz.TriggerBuilder.Create()
                .WithIdentity("trigger_10_sec", "group")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();
            
            Quartz.IJobDetail job2 =  Quartz.JobBuilder.Create<Job2>()
                .WithIdentity("job2", "group")
                .Build();
            
            Quartz.ITrigger trigger2 =  Quartz.TriggerBuilder.Create()
                .WithIdentity("trigger_20_sec", "group")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(20)
                    .RepeatForever())
                .Build();
            
            await this._scheduler.ScheduleJob(job1, trigger1, this._stopppingToken);
            await this._scheduler.ScheduleJob(job2, trigger2, this._stopppingToken);
        }
        
        
    } // End Class sch  
    
    
}  // End Namespace RamMonitor 
