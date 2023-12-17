using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	[CustomPropertyDrawer(typeof(Optional<>))]
	internal sealed class OptionalDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			SerializedProperty hasValueProperty = property.FindPropertyRelative("_hasValue");
			SerializedProperty valueProperty = property.FindPropertyRelative("_value");

			using (new EditorGUI.PropertyScope(position, label, property)) {
				// Draw label
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

				using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel)) {
					// Calculate rects
					Rect toggleRect = new Rect(position.x, position.y, 15, position.height);
					float consumed = toggleRect.width + 5;
					Rect valueRect = new Rect(position.x + consumed, position.y, position.width - consumed, position.height);

					// Draw fields - pass GUIContent.none to each so they are drawn without labels
					EditorGUI.PropertyField(toggleRect, hasValueProperty, GUIContent.none);

					using (new EditorGUI.DisabledScope(hasValueProperty.boolValue == false)) {
						EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none);
					}
				}
			}
		}
	}
}