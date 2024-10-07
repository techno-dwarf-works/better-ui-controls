using System;
using Better.Attributes.Runtime.Validation;
using UnityEngine;

namespace Better.UIControls.Runtime.Transitions
{
    [Serializable]
    public abstract class SelectableTransition : TransitionBehaviour<TransitionState>
    {
        
    }

    [Serializable]
    public abstract class SelectableTransition<TTarget> : SelectableTransition
    {
        [NotNull]
        [SerializeField] protected TTarget _target;
    }
}