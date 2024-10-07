using System;
using Better.UIControls.Runtime.Transitions;
using UnityEngine.UI;

namespace Better.UIControls.Runtime.Extensions
{
    public static class AnimationTriggersExtensions
    {
        public static string GetTrigger(this AnimationTriggers self, TransitionState state)
        {
            return state switch
            {
                TransitionState.Normal => self.normalTrigger,
                TransitionState.Highlighted => self.highlightedTrigger,
                TransitionState.Pressed => self.pressedTrigger,
                TransitionState.Selected => self.selectedTrigger,
                TransitionState.Disabled => self.disabledTrigger,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}