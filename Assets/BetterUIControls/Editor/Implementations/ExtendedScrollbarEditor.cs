using Better.Commons.EditorAddons.Extensions;
using Better.Commons.Runtime.DataStructures.Ranges;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using Better.Commons.Runtime.UIElements;
using Better.UIControls.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace Better.UIControls.EditorAddons
{
    [CustomEditor(typeof(ExtendedScrollbar))]
    public class ExtendedScrollbarEditor : ExtendedControlEditor
    {
        private const string ValuePropertyName = "m_Value";
        private const string SizePropertyName = "m_Size";
        private const string NumberOfStepsPropertyName = "m_NumberOfSteps";

        private string[] _customProperties = new[] { ScriptPropertyName, InteractablePropertyName, NavigationPropertyName, "m_HandleRect", "m_Direction", ValuePropertyName, SizePropertyName, NumberOfStepsPropertyName, TransitionPropertyName, };

        protected override string[] CustomProperties => _customProperties;

        protected override VisualElement CreateField(SerializedProperty property)
        {
            if (property.propertyPath == ValuePropertyName || property.propertyPath == SizePropertyName)
            {
                var range = new SerializedRange<float>(0, 1);
                var slider = new RangeSliderFloat(range);
                slider.SetupFromProperty(property);
                return slider;
            }

            if (property.propertyPath == NumberOfStepsPropertyName)
            {
                var range = new SerializedRange<int>(0, 11);
                var slider = new RangeSliderInt(range);
                slider.SetupFromProperty(property);
                return slider;
            }

            return base.CreateField(property);
        }
    }
}