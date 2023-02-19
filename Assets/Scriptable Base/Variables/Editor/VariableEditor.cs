using UnityEngine;
using UnityEditor; // This is required to allow us to inherent from the 'Editor Class'

namespace Variable
{
    //This allows us to edit any children / inherited under the BaseVariable Class, in this case only a single scriptable object. (need to verify)
    [CustomEditor(typeof(BaseVariable), true)]
    [CanEditMultipleObjects]
                                 // using UnityEditor is required here.
    public class VariableEditor : Editor
    {
        //Have a private version of the BaseVariable Data
        private SerializedProperty persistentMode;
        private SerializedProperty runtimeMode;
        private SerializedProperty initialValue;
        private SerializedProperty runtimeValue;
        private void OnEnable()
        {
            //Connect our version of the data to the actual data of Basevariable
            persistentMode = serializedObject.FindProperty("_persistenceMode");
            runtimeMode = serializedObject.FindProperty("_runtimeMode");
            initialValue = serializedObject.FindProperty("_initialValue");
            runtimeValue = serializedObject.FindProperty("_runtimeValue");
        }
        //This function specifically stops things from being seen in the inspector
        public override void OnInspectorGUI()
        {
            //display any value changes between two / update each other.
            serializedObject.Update();

            //This will only display the variable infomation in the inspector, which can be modified in the inspector
            EditorGUILayout.PropertyField(persistentMode);
            EditorGUILayout.PropertyField(runtimeMode);
            EditorGUILayout.PropertyField(initialValue);

            //This allows the variable information to be shown, but unable to be edited.
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(runtimeValue);
            EditorGUI.EndDisabledGroup();

            //Runtime Save Button interaction toggle
            EditorGUI.BeginDisabledGroup(persistentMode.boolValue == true);
            //This creates a button that can be pressed in the inspector.
            if (GUILayout.Button("Save Runtime to Initial Value"))
            {
                (target as BaseVariable).SaveToInitialValue();
            }
            EditorGUI.EndDisabledGroup();


            //This allows the variables / fields to actually be changed
            if (target)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }

}
