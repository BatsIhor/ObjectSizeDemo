using System;
using System.Threading;
using System.Threading.Tasks;

class Harness
{
    static void Main()
    {
        Console.ReadLine(); // wait until profiler attaches
        TestAsync().Wait();
    }

    static async Task TestAsync()
    {
#if true
        var token = CancellationToken.None;
        for (int i = 0; i < 100000; i++)
        {
#if true
            await Task.FromResult(42).WithCancellation1(token);
#else
            await Task.FromResult(42).WithCancellation2(token);
#endif
        }
#else
        var token = new CancellationTokenSource().Token;
        for (int i = 0; i < 100000; i++)
        {
            var tcs = new TaskCompletionSource<int>();
            var t = tcs.Task.WithCancellation2(token);
            tcs.SetResult(42);
            await t;
        }
#endif
    }
}

static class Extensions
{
    public static Task<T> WithCancellation2<T>(
        this Task<T> task, CancellationToken cancellationToken)
    {
        if (task.IsCompleted || !cancellationToken.CanBeCanceled)
            return task;
        else if (cancellationToken.IsCancellationRequested)
            return new Task<T>(() => default(T), cancellationToken);
        else
            return task.WithCancellation1(cancellationToken);
    }

    public static async Task<T> WithCancellation1<T>(
        this Task<T> task, CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource<bool>();
#if true
        using (cancellationToken.Register(() => tcs.TrySetResult(true)))
#else
        using (cancellationToken.Register(s_cancellationRegistration, tcs))
#endif
            if (task != await Task.WhenAny(task, tcs.Task))
                throw new OperationCanceledException(cancellationToken);
        return await task;
    }

    private static readonly Action<object> s_cancellationRegistration =
        s => ((TaskCompletionSource<bool>)s).TrySetResult(true);
}