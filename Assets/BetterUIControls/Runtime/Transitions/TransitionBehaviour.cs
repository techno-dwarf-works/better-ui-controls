using System;
using System.Threading;
using System.Threading.Tasks;

namespace Better.UIControls.Runtime.Transitions
{
    [Serializable]
    public abstract class TransitionBehaviour<TState>
    {
        public abstract Task PlayAsync(TState transitionState, CancellationToken token);
        public abstract void PlayInstant(TState transitionState);
    }
}