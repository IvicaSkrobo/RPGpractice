﻿using System;
using UnityEditor;

namespace RPG.CameraUI
{
    // TODO consider changing to a property drawer
    [CustomEditor(typeof(CameraRaycaster))]
    public class CameraRaycasterEditor : Editor
    {
        bool isLayerPrioritiesUnfolded = true; // store the UI state

        public override void OnInspectorGUI()   //gui is called when inspector gui is called
        {
            serializedObject.Update(); // Serialize cameraRaycaster instance, puts it in the format so it can be saved on the disk

            isLayerPrioritiesUnfolded = EditorGUILayout.Foldout(isLayerPrioritiesUnfolded, "Layer Priorities");     //opened editorguiLayout with name Layer Priorites
            if (isLayerPrioritiesUnfolded)
            {
                EditorGUI.indentLevel++;
                {
                    BindArraySize();
                    BindArrayElements();
                }
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties(); // De-serialize back to cameraRaycaster (and create undo point)
        }



        void BindArraySize()
        {
            int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
            int requiredArraySize = EditorGUILayout.IntField("Size", currentArraySize);
            if (requiredArraySize != currentArraySize)
            {
                serializedObject.FindProperty("layerPriorities.Array.size").intValue = requiredArraySize;
            }
        }

        void BindArrayElements()
        {
            int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
            for (int i = 0; i < currentArraySize; i++)
            {
                var prop = serializedObject.FindProperty(string.Format("layerPriorities.Array.data[{0}]", i));
                EditorGUILayout.LayerField(string.Format("Layer {0}:", i), prop.intValue);
            }
        }
    }
}
