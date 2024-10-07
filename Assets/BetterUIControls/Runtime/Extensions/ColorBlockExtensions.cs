using System;
using Better.UIControls.Runtime.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace Better.UIControls.Runtime.Extensions
{
    public static class ColorBlockExtensions
    {
        public static Color GetColor(this ColorBlock self, TransitionState state)
        {
            return state switch
            {
                TransitionState.Normal => self.normalColor,
                TransitionState.Highlighted => self.highlightedColor,
                TransitionState.Pressed => self.pressedColor,
                TransitionState.Selected => self.selectedColor,
                TransitionState.Disabled => self.disabledColor,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }

        public static Color GetMultipliedColor(this ColorBlock self, TransitionState state)
        {
            var color = self.GetColor(state);
            return color * self.colorMultiplier;
        }
    }
}