using System;

namespace MyServices.UIService.Realization
{
    public abstract class Command
    {
        public abstract void Execute();
        public event EventHandler DoneEvent;

        protected virtual void OnDone()
        {
            DoneEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
