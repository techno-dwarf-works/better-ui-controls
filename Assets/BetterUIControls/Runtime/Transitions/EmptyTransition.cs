using System;
using System.Threading;
using System.Threading.Tasks;

namespace Better.UIControls.Runtime.Transitions
{
    //TODO: add set beh to extended (???)
    [Serializable]
    public class EmptyTransition : TransitionBehaviour
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