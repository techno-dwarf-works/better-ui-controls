using Better.UIControls.Runtime;
using UnityEditor;

namespace Better.UIControls.EditorAddons
{
    [CustomEditor(typeof(ExtendedSlider))]
    public class ExtendedSliderEditor : ExtendedControlEditor
    {
        private readonly string[] _customProperties = new[] { ScriptPropertyName, InteractablePropertyName, NavigationPropertyName, "m_FillRect", "m_HandleRect", "m_Direction", "m_MinValue", "m_MaxValue", "m_WholeNumbers", "m_Value", TransitionPropertyName, };

        protected override string[] CustomProperties => _customProperties;
    }
}