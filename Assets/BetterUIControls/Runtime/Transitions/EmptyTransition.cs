using System;
using System.Threading;
using System.Threading.Tasks;

namespace Better.UIControls.Runtime.Transitions
{
    [Serializable]
    public class EmptyTransition : SelectableTransition
    {
        public override Task PlayAsync(TransitionState transitionState, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public override void PlayInstant(TransitionState transitionState)
        {
            
        }
    }
}