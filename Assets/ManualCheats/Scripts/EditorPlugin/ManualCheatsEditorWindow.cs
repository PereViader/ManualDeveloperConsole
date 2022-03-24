using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ManualCheats.EditorPlugin
{
    public class ManualCheatsEditorWindow : EditorWindow
    {
        public static List<Action> OnGuiActions = new List<Action>();
        private Vector2 scrollPosition;

        private void OnGUI()
        {
            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.LabelField("Enter Play Mode first");
                return;
            }

            if (OnGuiActions.Count == 0)
            {
                EditorGUILayout.LabelField("No cheats registered");
                return;
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            for (int i = 0; i < OnGuiActions.Count; i++)
            {
                OnGuiActions[i].Invoke();
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
