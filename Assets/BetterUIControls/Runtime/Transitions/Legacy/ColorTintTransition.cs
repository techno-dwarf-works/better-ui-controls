using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Attributes.Runtime.Misc;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utility;
using Better.UIControls.Runtime.Extensions;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Better.UIControls.Runtime.Transitions
{
    [Serializable]
    public class ColorTintTransition : SelectableTransition<Graphic>
    {
        [HideLabel] 
        [SerializeField] private ColorBlock _colors = ColorBlock.defaultColorBlock;
        [SerializeField] private bool _ignoreTimeScale = true;
        [SerializeField] private bool _useAlpha = true;

        public override Task PlayAsync(TransitionState transitionState, CancellationToken token)
        {
            var color = _colors.GetMultipliedColor(transitionState);
            return StartColorTween(color, _colors.fadeDuration);
        }

        private Task StartColorTween(Color targetColor, float duration)
        {
            if (_target == null)
                return Task.CompletedTask;

            _target.CrossFadeColor(targetColor, duration, _ignoreTimeScale, _useAlpha);
            return TaskUtility.WaitForSeconds(duration);
        }

        public override void PlayInstant(TransitionState transitionState)
        {
            var color = _colors.GetColor(transitionState);
            StartColorTween(color, 0f).Forget();
        }
    }
}