using Better.Commons.EditorAddons.Extensions;
using Better.Commons.Runtime.UIElements;
using Better.UIControls.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Better.UIControls.EditorAddons
{
    [CustomEditor(typeof(ExtendedSlider))]
    public class ExtendedSliderEditor : ExtendedControlEditor
    {
        private const string ValuePropertyName = "m_Value";
        private const string MinValuePropertyName = "m_MinValue";
        private const string MaxValuePropertyName = "m_MaxValue";

        private readonly string[] _customProperties = new[] { ScriptPropertyName, InteractablePropertyName, NavigationPropertyName, "m_FillRect", "m_HandleRect", "m_Direction", MinValuePropertyName, MaxValuePropertyName, "m_WholeNumbers", ValuePropertyName, TransitionPropertyName, };
        private RangeSliderFloat _valueSlider;

        protected override string[] CustomProperties => _customProperties;

        protected override VisualElement CreateField(SerializedProperty property)
        {
            if (property.propertyPath == ValuePropertyName)
            {
                var minValueProperty = serializedObject.FindProperty(MinValuePropertyName);
                var maxValueProperty = serializedObject.FindProperty(MaxValuePropertyName);
                var range = new PropertyRange(minValueProperty, maxValueProperty);
                _valueSlider = new RangeSliderFloat(range);
                _valueSlider.SetupFromProperty(property);
                return _valueSlider;
            }
            
            return base.CreateField(property);
        }

        protected override void OnSerializedObjectTrack(SerializedObject serializedObject)
        {
            _valueSlider.RefreshRange();
            base.OnSerializedObjectTrack(serializedObject);
        }
    }
}