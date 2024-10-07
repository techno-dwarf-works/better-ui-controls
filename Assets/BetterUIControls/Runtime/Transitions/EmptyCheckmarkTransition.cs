using System;
using System.Threading;
using System.Threading.Tasks;

namespace Better.UIControls.Runtime.Transitions
{
    [Serializable]
    public class EmptyCheckmarkTransition : CheckmarkTransition
    {
        public override Task PlayAsync(bool transitionState, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public override void PlayInstant(bool transitionState)
        {
            
        }
    }
}