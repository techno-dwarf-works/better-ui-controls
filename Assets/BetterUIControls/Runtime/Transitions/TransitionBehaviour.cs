using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Attributes.Runtime.Validation;
using UnityEngine;

namespace Better.UIControls.Runtime.Transitions
{
    [Serializable]
    public abstract class TransitionBehaviour
    {
        public abstract Task PlayAsync(TransitionState transitionState, CancellationToken token);
        public abstract void PlayInstant(TransitionState transitionState);
    }

    [Serializable]
    public abstract class TransitionBehaviour<TTarget> : TransitionBehaviour
    {
        [NotNull]
        [SerializeField] protected TTarget _target;
    }
}