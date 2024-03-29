using Foundation;
using UnityEditor;
using UnityEngine;

namespace Foundation.Editors {
	[CustomPropertyDrawer(typeof(PropertyObserver<>))]
	internal sealed class PropertyObserverDrawer : PropertyDrawer {
		//private bool hasWillSet = default;
		//private bool hasDidSet = default;

		// public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		//     //hasWillSet = propertyObserver.HasWillSetFunction;
		//     //hasDidSet = propertyObserver.HasDidSetFunction;
		//
		//     return base.GetPropertyHeight(property, label);
		// }

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			SerializedProperty valueProperty = property.FindPropertyRelative("_value");

			using (var scope = new EditorGUI.PropertyScope(position, label, property)) {
				// Draw label
				label = scope.content;
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

				using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel)) {
					// Calculate rects
					float consumed = 0;
					//Rect willSetRect = new Rect(position.x, position.y, 8, position.height);
					//if (hasWillSet) {
					//    consumed += willSetRect.width;
					//}
					//Rect didSetRect = new Rect(position.x + consumed, position.y, 8, position.height);
					//if (hasDidSet) {
					//    consumed += didSetRect.width;
					//}
					Rect valueRect = new Rect(position.x + consumed, position.y, position.width - consumed, position.height);

					// Draw
					//if (hasWillSet) {
					//    EditorGUI.LabelField(willSetRect, "W", EditorStyles.boldLabel);
					//}
					//if (hasDidSet) {
					//    EditorGUI.LabelField(didSetRect, "D", EditorStyles.boldLabel);
					//}
					EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none);
				}
			}
		}
	}
}