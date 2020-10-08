using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace TaskQueueTest
{
    public class TaskQueue
    {
        private readonly SemaphoreSlim semaphore;
        private readonly SemaphoreSlim semaphore2;
        private readonly List<Tuple<int, Func<int, Task>>> funcs;

        public TaskQueue()
        {
            semaphore = new SemaphoreSlim(1);
            semaphore2 = new SemaphoreSlim(1);
            funcs = new List<Tuple<int, Func<int, Task>>>();
        }

        public async Task Enqueue(Func<int, Task> taskGenerator, params int[] parameters)
        {
            await semaphore2.WaitAsync();
            Tuple<int, Func<int, Task>> taskGenerator1 = new Tuple<int, Func<int, Task>>(parameters[0], taskGenerator);
            funcs.Add(taskGenerator1);
            semaphore2.Release();
            await semaphore.WaitAsync();
            try
            {
                Tuple<int, Func<int, Task>> taskGenerator2 = funcs.FirstOrDefault(x => x.Item1 == taskGenerator1.Item1);
                if (taskGenerator2 != null)
                {
                    await taskGenerator1.Item2(parameters[0]);
                }
            }
            finally
            {
                await semaphore2.WaitAsync();
                funcs.Remove(taskGenerator1);
                semaphore2.Release();
                semaphore.Release();
            }
        }

        public async Task Dequeue()
        {
            await semaphore2.WaitAsync();
            funcs.RemoveAt(funcs.Count - 1);
            semaphore2.Release();
        }
    }
}
