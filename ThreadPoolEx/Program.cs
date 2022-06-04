// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using System.Diagnostics;
using ThreadPoolEx;

Console.WriteLine("Hello, World!");

//SimpleThreadPool.QueueUserWorkItem(() => { Console.WriteLine("111111111"); });
//SimpleThreadPool.QueueUserWorkItem(() => { Console.WriteLine("22222222"); });
NonPooling();
#region
//var tcs = new TaskCompletionSource();
//var tasks = new List<Task>();
//for (int i = 0; i < Environment.ProcessorCount*4; i++)
//{
//    int id = i;
//    tasks.Add(Task.Run(() =>
//    {
//        Console.WriteLine($"{DateTime.UtcNow:MM:ss.ff}: {id}");
//        tcs.Task.Wait();
//    }));

//}

//tasks.Add(Task.Run(() => tcs.SetResult())) ;
//var sw = Stopwatch.StartNew();
//Task.WaitAll(tasks.ToArray());
//Console.WriteLine($"Done: {sw.Elapsed}");
#endregion

#region

[Benchmark(Baseline = true)]
Task NonPooling()
{
    return Task.WhenAll(from i in Enumerable.Range(0, 256)
                        select Task.Run(async delegate
                        {
                            for (int i = 0; i < 100; i++)
                                await A().ConfigureAwait(false);
                        }));

    static async ValueTask A() => await B().ConfigureAwait(false);

    static async ValueTask B() => await C().ConfigureAwait(false);

    static async ValueTask C() => await D().ConfigureAwait(false);

    static async ValueTask D() => await Task.Yield();
}
#endregion