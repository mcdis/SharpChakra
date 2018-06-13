using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChakra
{
    internal class JsSemaphore
    {
        private readonly object _lock = new object();
        private readonly ConcurrentStack<JsContext> _contextLockStack = new ConcurrentStack<JsContext>();
        private readonly ConcurrentDictionary<IntPtr, ManualResetEvent> _refResetEvents = new ConcurrentDictionary<IntPtr, ManualResetEvent>();
        private IntPtr _currentRef = IntPtr.Zero;
        private int _amount;

        public void WaitOne(JsContext context)
        {
            Monitor.Enter(_lock);

            if (_currentRef == IntPtr.Zero)
            {
                _currentRef = context.Reference;
            }

            if (_currentRef != context.Reference)
            {
                var resetEvent = _refResetEvents.GetOrAdd(context.Reference, _ =>
                {
                    _contextLockStack.Push(context);
                    return new ManualResetEvent(false);
                });

                Monitor.Exit(_lock);
                resetEvent.WaitOne();
            }
            else
            {
                Monitor.Exit(_lock);
            }

            Interlocked.Increment(ref _amount);
        }

        public void Release()
        {
            if (Interlocked.Decrement(ref _amount) != 0)
            {
                return;
            }

            Monitor.Enter(_lock);

            if (_contextLockStack.TryPop(out var context))
            {
                _currentRef = context.Reference;

                if (_refResetEvents.TryRemove(context.Reference, out var resetEvent))
                {
                    resetEvent.Set();
                }
            }
            else
            {
                _currentRef = IntPtr.Zero;
            }

            Monitor.Exit(_lock);
        }
    }
}

