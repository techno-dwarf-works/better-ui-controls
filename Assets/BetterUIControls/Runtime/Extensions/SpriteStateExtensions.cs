using System;
using Better.UIControls.Runtime.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace Better.UIControls.Runtime.Extensions
{
    public static class SpriteStateExtensions
    {
        public static Sprite GetSprite(this SpriteState self, TransitionState state)
        {
            return state switch
            {
                TransitionState.Normal => null,
                TransitionState.Highlighted => self.highlightedSprite,
                TransitionState.Pressed => self.pressedSprite,
                TransitionState.Selected => self.selectedSprite,
                TransitionState.Disabled => self.disabledSprite,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}