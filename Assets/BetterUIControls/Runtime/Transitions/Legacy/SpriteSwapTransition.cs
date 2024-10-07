using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Attributes.Runtime.Misc;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utility;
using Better.UIControls.Runtime.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Better.UIControls.Runtime.Transitions
{
    [Serializable]
    public class SpriteSwapTransition : SelectableTransition<Image>
    {
        [HideLabel] 
        [SerializeField] private SpriteState _sprites;

        public override Task PlayAsync(TransitionState transitionState, CancellationToken token)
        {
            var sprite = _sprites.GetSprite(transitionState);
            return SetSprite(sprite);
        }

        private Task SetSprite(Sprite targetSprite)
        {
            if (_target == null)
                return Task.CompletedTask;
            _target.overrideSprite = targetSprite;
            return Task.CompletedTask;
        }

        public override void PlayInstant(TransitionState transitionState)
        {
            var sprite = _sprites.GetSprite(transitionState);
            SetSprite(sprite).Forget();
        }
    }
}