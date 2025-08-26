using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ETModel
{
    public partial struct ETTask
    {
        public class CoroutineBlocker
        {
            private int count;

            private ETTaskCompletionSource tcs = new ETTaskCompletionSource();

            public bool IsCompleted => tcs?.Task.IsCompleted ?? false;
            
            public CoroutineBlocker(int count)
            {
                this.count = count;
            }

            public async ETTask WaitAsync()
            {
                await tcs.Task;
            }

            public void CompleteTask()
            {
                --this.count;

                if (this.count < 0)
                {
                    return;
                }
                if (this.count == 0)
                {
                    tcs.SetResult();
                }
            }

            public void Cancel()
            {
                tcs.TrySetCanceled();
            }
            public void SetResult()
            {
                tcs.TrySetResult();
            }
        }

        public static ETTask CompletedTask => new ETTask();

        public static ETTask FromException(Exception ex)
        {
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();
            tcs.TrySetException(ex);
            return tcs.Task;
        }

        public static ETTask<T> FromException<T>(Exception ex)
        {
            var tcs = new ETTaskCompletionSource<T>();
            tcs.TrySetException(ex);
            return tcs.Task;
        }

        public static ETTask<T> FromResult<T>(T value)
        {
            return new ETTask<T>(value);
        }

        public static ETTask FromCanceled()
        {
            return CanceledETTaskCache.Task;
        }

        public static ETTask<T> FromCanceled<T>()
        {
            return CanceledETTaskCache<T>.Task;
        }

        public static ETTask FromCanceled(CancellationToken token)
        {
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();
            tcs.TrySetException(new OperationCanceledException(token));
            return tcs.Task;
        }

        public static ETTask<T> FromCanceled<T>(CancellationToken token)
        {
            var tcs = new ETTaskCompletionSource<T>();
            tcs.TrySetException(new OperationCanceledException(token));
            return tcs.Task;
        }

        /// <summary>
        /// 等待全部任務完成
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ETTask<bool> WaitAll(ETTask[] tasks, CancellationToken? cancellationToken = null)
        {
            if (tasks.Length == 0)
            {
                return false;
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(tasks.Length);

            cancellationToken?.Register(() =>
            {
                coroutineBlocker.Cancel();
            });

            foreach (ETTask task in tasks)
            {
                RunOneTask(task).Coroutine();
            }

            var wait = coroutineBlocker.WaitAsync();
            await wait;

            async ETVoid RunOneTask(ETTask task)
            {
                await task;
                coroutineBlocker.CompleteTask();
            }

            if (cancellationToken == null)
            {
                return true;
            }

            return wait.IsCompleted;
        }

        /// <summary>
        /// 等待全部任務完成
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ETTask<bool> WaitAll(List<ETTask> tasks, CancellationToken? cancellationToken = null)
        {
            if (tasks.Count == 0)
            {
                return false;
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(tasks.Count);

            cancellationToken?.Register(() =>
            {
                coroutineBlocker.Cancel();
            });

            foreach (ETTask task in tasks)
            {
                RunOneTask(task).Coroutine();
            }

            var wait = coroutineBlocker.WaitAsync();
            await wait;

            async ETVoid RunOneTask(ETTask task)
            {
                await task;
                coroutineBlocker.CompleteTask();
            }

            if (cancellationToken == null)
            {
                return true;
            }

            return wait.IsCompleted;
        }
        /// <summary>
        /// 等待全部任務完成
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ETTask<bool> WaitAll(Task[] tasks, CancellationToken? cancellationToken = null)
        {
            if (tasks.Length == 0)
            {
                return false;
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(tasks.Length);

            cancellationToken?.Register(() =>
            {
                coroutineBlocker.Cancel();
            });

            foreach (var task in tasks)
            {
                RunOneTask(task).Coroutine();
            }

            var wait = coroutineBlocker.WaitAsync();
            await wait;

            async ETVoid RunOneTask(Task task)
            {
                await task;
                coroutineBlocker.CompleteTask();
            }

            if (cancellationToken == null)
            {
                return true;
            }

            return wait.IsCompleted;
        }
        public static async ETTask<T[]> WaitAll<T>(Task<T>[] tasks, CancellationToken? cancellationToken = null)
        {
            if (tasks.Length == 0)
            {
                return default;
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(tasks.Length);

            cancellationToken?.Register(() =>
            {
                coroutineBlocker.Cancel();
            });

            foreach (var task in tasks)
            {
                RunOneTask(task).Coroutine();
            }

            var wait = coroutineBlocker.WaitAsync();
            await wait;

            async ETVoid RunOneTask(Task task)
            {
                await task;
                coroutineBlocker.CompleteTask();
            }


            return tasks.Select(e => e.Result).ToArray();
        }
        /// <summary>
        /// 等待全部任務完成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ETTask<T[]> WaitAll<T>(ETTask<T>[] tasks, CancellationToken? cancellationToken = null)
        {
            if (tasks.Length == 0)
            {
                return new T[tasks.Length];
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(tasks.Length);

            cancellationToken?.Register(() =>
            {
                coroutineBlocker.Cancel();
            });

            foreach (ETTask task in tasks)
            {
                RunOneTask(task).Coroutine();
            }

            var wait = coroutineBlocker.WaitAsync();
            await wait;

            async ETVoid RunOneTask(ETTask task)
            {
                await task;
                coroutineBlocker.CompleteTask();
            }

            return tasks.Select(e => e.Result).ToArray();
        }
        /// <summary>
        /// 等待全部任務完成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ETTask<T[]> WaitAll<T>(List<ETTask<T>> tasks, CancellationToken? cancellationToken = null)
        {
            if (tasks.Count == 0)
            {
                return new T[tasks.Count];
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(tasks.Count);

            cancellationToken?.Register(() =>
            {
                coroutineBlocker.Cancel();
            });

            foreach (ETTask task in tasks)
            {
                RunOneTask(task).Coroutine();
            }

            var wait = coroutineBlocker.WaitAsync();
            await wait;

            async ETVoid RunOneTask(ETTask task)
            {
                await task;
                coroutineBlocker.CompleteTask();
            }

            return tasks.Select(e => e.Result).ToArray();
        }

        private static class CanceledETTaskCache
        {
            public static readonly ETTask Task;

            static CanceledETTaskCache()
            {
                ETTaskCompletionSource tcs = new ETTaskCompletionSource();
                tcs.TrySetCanceled();
                Task = tcs.Task;
            }
        }

        private static class CanceledETTaskCache<T>
        {
            public static readonly ETTask<T> Task;

            static CanceledETTaskCache()
            {
                var taskCompletionSource = new ETTaskCompletionSource<T>();
                taskCompletionSource.TrySetCanceled();
                Task = taskCompletionSource.Task;
            }
        }
    }

    internal static class CompletedTasks
    {
        public static readonly ETTask<bool> True = ETTask.FromResult(true);
        public static readonly ETTask<bool> False = ETTask.FromResult(false);
        public static readonly ETTask<int> Zero = ETTask.FromResult(0);
        public static readonly ETTask<int> MinusOne = ETTask.FromResult(-1);
        public static readonly ETTask<int> One = ETTask.FromResult(1);
    }
}