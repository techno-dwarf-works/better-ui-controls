using System;
using System.Threading;
using Better.Attributes.Runtime.Misc;
using Better.Attributes.Runtime.Select;
using Better.Attributes.Runtime.Validation;
using Better.Commons.Runtime.Extensions;
using Better.UIControls.Runtime.Transitions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Better.UIControls.Runtime
{
    public class ExtendedToggle : Toggle
    {
        [Select] [NotNull] [Detailed] 
        [SerializeReference] private SelectableTransition _transition;

        [Select] [NotNull] [Detailed] 
        [SerializeReference] private CheckmarkTransition _toggleTransition;

        private CancellationTokenSource _transitionTokenSource;
        private CancellationTokenSource _checkmarkTransitionTokenSource;

        private UnityAction<bool> _valueChangeAction;
        
        protected override void Awake()
        {
            _valueChangeAction = OnValueChanged;
            onValueChanged.AddListener(_valueChangeAction);
            base.Awake();
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            if (!gameObject.activeInHierarchy)
                return;

            ValidateTransition();
            ValidateCheckmarkTransition();
            ValidateValueChange();
            ForceUpdateToken();
            var transitionState = GetTransitionState(state);
            if (instant)
            {
                _transition.PlayInstant(transitionState);
            }
            else
            {
                _transition.PlayAsync(transitionState, _transitionTokenSource.Token).Forget();
            }
        }

        private void ValidateValueChange()
        {
            _valueChangeAction ??= OnValueChanged;
            
            onValueChanged.RemoveListener(_valueChangeAction);
            onValueChanged.AddListener(_valueChangeAction);
        }

        public virtual void Rebuild()
        {
            ForceUpdateCheckmarkToken();
            ValidateValueChange();
            _toggleTransition?.PlayInstant(isOn);
        }

        protected override void InstantClearState()
        {
            //TODO: if base logic not breaking overriden transitions
            base.InstantClearState();

            DoStateTransition(SelectionState.Normal, true);
        }

        protected override void OnDisable()
        {
            ForceUpdateCheckmarkToken();
            ForceUpdateToken();
            base.OnDisable();
        }

        protected override void OnDestroy()
        {
            ForceUpdateCheckmarkToken();
            ForceUpdateToken();
            base.OnDestroy();
        }

        private void ForceUpdateToken()
        {
            ForceUpdateToken(ref _transitionTokenSource);
        }

        private void ForceUpdateCheckmarkToken()
        {
            ForceUpdateToken(ref _checkmarkTransitionTokenSource);
        }

        private void ForceUpdateToken(ref CancellationTokenSource source)
        {
            source?.Cancel(false);
            source = new CancellationTokenSource();
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

        private void OnValueChanged(bool value)
        {
            ForceUpdateCheckmarkToken();
            _toggleTransition?.PlayAsync(value, _checkmarkTransitionTokenSource.Token).Forget();
        }

        private void ValidateTransition()
        {
            if (_transition != null) return;

            var message = $"Extended button has no {nameof(_transition)}, using fallback";
            Debug.LogWarning(message, gameObject);
            _transition = new EmptyTransition();
        }
        
        private void ValidateCheckmarkTransition()
        {
            if (_transition != null) return;

            var message = $"Extended button has no {nameof(_toggleTransition)}, using fallback";
            Debug.LogWarning(message, gameObject);
            _toggleTransition = new EmptyCheckmarkTransition();
        }
    }
}