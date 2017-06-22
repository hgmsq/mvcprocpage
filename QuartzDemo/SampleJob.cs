using System;
using log4net;
using Quartz;

namespace QuartzDemo
{
    public sealed class SampleJob : IJob
    {
        private static readonly ILog _logger = log4net.LogManager.GetLogger("SampleLog");

        public void Execute(IJobExecutionContext context)
        {
            _logger.Info("SampleJob测试");
            Console.WriteLine("执行任务" + DateTime.Now.ToLongTimeString());
        }
    }
}
