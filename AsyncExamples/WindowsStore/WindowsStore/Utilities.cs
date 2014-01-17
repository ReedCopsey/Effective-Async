namespace AsyncDemoStoreApp
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class Utilities
    {
        #region TimeOperation
      
        public static async Task<TimeSpan> TimeOperationAsync(Func<Task> operation)
        {
            var sw = Stopwatch.StartNew();
            await operation();
            return sw.Elapsed;
        }

        #endregion

        #region ContinueOnCapturedContext

        //public static async Task<TimeSpan> TimeOperationAsync(Func<Task> operation)
        //{
        //    var sw = Stopwatch.StartNew();
        //    await operation();
        //    return sw.Elapsed;
        //}

        public static Task<TimeSpan> PerformTimedWorkAsync()
        {
            return TimeOperationAsync(async () =>
                {
                    double value = 0.0;
                    for (int i = 1; i < 100000; ++i)
                    {
                        await Task.Run(() => Parallel.Invoke(Work, Work, Work, Work)).ConfigureAwait(continueOnCapturedContext: false);
                        value += Math.Log10(i);
                    }
                });
        }

        public static void Work()
        {
            double v = -1e4;
            while (v < 1e4)
            {
                v++;
            }            
        }

        #endregion
    }
}
