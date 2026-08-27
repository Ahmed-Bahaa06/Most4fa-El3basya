using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace KhosaryCode.Events.Editor
{
    // By targeting ScriptableObject with 'true' (editorForChildClasses), 
    // this editor applies to ALL ScriptableObjects. We then filter internally.
    [CustomEditor(typeof(ScriptableObject), true)]
    public class GenericEventChannelSOEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ScriptableObject scriptableObject = (ScriptableObject)target;
            Type type = scriptableObject.GetType();

            // Check if the ScriptableObject inherits from our GenericEventChannelSO<>
            if (type.BaseType != null && type.BaseType.IsGenericType &&
                type.BaseType.GetGenericTypeDefinition() == typeof(GenericEventChannelSO<>))
            {
                EditorGUILayout.Space();

                if (GUILayout.Button("Invoke"))
                {
                    // Use Reflection to get the private _testingValue field from the base class
                    FieldInfo valueField = type.BaseType.GetField("_testingValue", BindingFlags.NonPublic | BindingFlags.Instance);
                    
                    if (valueField != null)
                    {
                        object value = valueField.GetValue(scriptableObject);

                        // Find the public Invoke method and call it
                        MethodInfo method = type.GetMethod("Invoke");
                        method?.Invoke(scriptableObject, new object[] { value });
                    }
                    else
                    {
                        Debug.LogError("Could not find the private _testingValue field via reflection.");
                    }
                }
            }
        }
    }
}
