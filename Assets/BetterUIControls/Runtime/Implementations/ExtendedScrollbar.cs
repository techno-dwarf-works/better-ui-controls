using System;
using System.Threading;
using Better.Attributes.Runtime.Misc;
using Better.Attributes.Runtime.Select;
using Better.Attributes.Runtime.Validation;
using Better.Commons.Runtime.Extensions;
using Better.UIControls.Runtime.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace Better.UIControls.Runtime
{
    public class ExtendedScrollbar : Scrollbar
    {
        [Select] [NotNull] [Detailed] 
        [SerializeReference] protected SelectableTransition _transition;

        private CancellationTokenSource _transitionTokenSource;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            if (!gameObject.activeInHierarchy)
                return;

            ValidateTransition();

            CancelToken();
            var transitionState = GetTransitionState(state);
            if (instant)
            {
                _transition.PlayInstant(transitionState);
            }
            else
            {
                _transitionTokenSource = new CancellationTokenSource();
                _transition.PlayAsync(transitionState, _transitionTokenSource.Token).Forget();
            }
        }

        protected override void InstantClearState()
        {
            //TODO: if base logic not breaking overriden transitions
            base.InstantClearState();

            DoStateTransition(SelectionState.Normal, true);
        }

        protected override void OnDisable()
        {
            CancelToken();
            base.OnDisable();
        }

        protected override void OnDestroy()
        {
            CancelToken();
            base.OnDestroy();
        }

        private void ValidateTransition()
        {
            if (_transition == null)
            {
                var message = $"Extended button has no {nameof(_transition)}, using fallback";
                Debug.LogWarning(message, gameObject);
                _transition = new EmptyTransition();
            }
        }

        private void CancelToken()
        {
            _transitionTokenSource?.Cancel(false);
            _transitionTokenSource = null;
        }

        private TransitionState GetTransitionState(SelectionState state)
        {
            return state switch
            {
                SelectionState.Normal => TransitionState.Normal,
                SelectionState.Highlighted => TransitionState.Highlighted,
                SelectionState.Pressed => TransitionState.Pressed,
                SelectionState.Selected => TransitionState.Selected,
                SelectionState.Disabled => TransitionState.Disabled,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}