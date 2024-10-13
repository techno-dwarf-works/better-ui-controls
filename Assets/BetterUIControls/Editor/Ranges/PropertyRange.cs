using Better.Commons.EditorAddons.Extensions;
using Better.Commons.Runtime.DataStructures.Ranges;
using UnityEditor;

namespace Better.UIControls.EditorAddons
{
    public class PropertyRange : Range<float>
    {
        private readonly SerializedProperty _minProperty;
        private readonly SerializedProperty _maxProperty;

        public override float Min => _minProperty.Verify() ? _minProperty.floatValue : 0;

        public override float Max => _maxProperty.Verify() ? _maxProperty.floatValue : 0;

        public PropertyRange(SerializedProperty minProperty, SerializedProperty maxProperty)
        {
            _minProperty = minProperty;
            _maxProperty = maxProperty;
        }

        public override Range<float> Clone()
        {
            return new PropertyRange(_minProperty, _maxProperty);
        }
    }
}