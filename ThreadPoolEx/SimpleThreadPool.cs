using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPoolEx
{
    public class SimpleThreadPool
    {
        private static BlockingCollection<Action> s_work = new();
        public static void QueueUserWorkItem(Action work)
        {
            s_work.Add(work);
        }
        static SimpleThreadPool()
        {
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                new Thread(() => 
                {
                    while (true) s_work.Take()();
                }) { IsBackground = true }.Start();
            }
        }
    }
}
