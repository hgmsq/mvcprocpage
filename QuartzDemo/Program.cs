using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;
namespace QuartzDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //string str = GetList();
            //Console.WriteLine(str);
            //CodeMethod();
            //ConfigMethod();
        }
        private static void ConfigMethod()
        {
            var path = string.Format("{0}log4net.xml", AppDomain.CurrentDomain.BaseDirectory);
            try
            {   //工厂
                ISchedulerFactory factory = new StdSchedulerFactory();
                //启动
                IScheduler scheduler = factory.GetScheduler(); //IScheduler 计划者
                scheduler.GetJobGroupNames();
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));
                scheduler.Start();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void CodeMethod()
        {
            Console.WriteLine(DateTime.Now.ToString());
            //1、首先创建一个作业调度池
            ISchedulerFactory scheduler = new StdSchedulerFactory();
            IScheduler sched = scheduler.GetScheduler();
            //2、创建一个具体的作业
            IJobDetail job = JobBuilder.Create<JobDemo>().Build();
            //3、创建并配置一个一个触发器

            #region 每隔5秒执行一次Execture，无休止
            ISimpleTrigger trigger = (ISimpleTrigger)
            TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInSeconds(30).WithRepeatCount(int.MaxValue)).Build();
            #endregion
            #region 程序每隔5秒执行一次，一共执行50次，开始执行时间设定为当前时间，结束时间设定为1小时之后，不管50次是否执行完成，1小时后就不在执行
            DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(DateTime.Now.AddSeconds(1), 2);
            DateTimeOffset endTime = DateBuilder.NextGivenSecondDate(DateTime.Now.AddHours(1), 5);
            //ISimpleTrigger trigger1 = (ISimpleTrigger)TriggerBuilder.Create().StartAt(startTime).EndAt(endTime)
            //    .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).WithRepeatCount(50).Build());
            #endregion
            #region 实现各种维度的调用（使用cron-like）;在每小时的第10、20、30分钟，每分钟的第11、17秒执行一次
            ICronTrigger trigger2 = (ICronTrigger)TriggerBuilder.Create().StartAt(startTime).EndAt(endTime)
                .WithCronSchedule("11,27 10,20,30 * * * ? ").Build();
            #endregion
            //4、加入作业调度池中
            sched.ScheduleJob(job, trigger);
            //5、开始执行
            sched.Start();
            Console.ReadKey();

        }
        public class JobDemo : IJob
        {
            /// <summary>
            /// 这里是作业调度定时执行的方法
            /// </summary>
            /// <param name="context"></param>
            public void Execute(IJobExecutionContext context)
            {

                Console.WriteLine(DateTime.Now.ToString());
            }
        }

        public static string GetList()
        {
            StringBuilder sb = new StringBuilder();
            List<int> list = new List<int>();
            HashSet<int> hslist = new HashSet<int>();
            Random r = new Random();
            try
            {
                while (hslist.Count < 49)
                {
                    hslist.Add(r.Next(1, 50));
                }


                foreach (var item in hslist)
                {
                    sb.Append(item + ",");

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return sb.ToString();
        }

    }
}
