#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Game.Scripts.Map.Hills;

namespace Game.Scripts.EditorCustomize.HillEditor
{
    [CustomEditor(typeof(HillRuleTile))]
    public class HillRuleTileEditor : Editor
    {
        private HillRuleTile hillSO;

        private void OnEnable()
        {
            hillSO = (HillRuleTile)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Field RuleTile
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ruleTile"));

            GUILayout.Space(10);

            // Button Generate
            if (GUILayout.Button("Generate Rules From RuleTile"))
            {
                hillSO.GenerateRuleTile();
                EditorUtility.SetDirty(hillSO);
            }

            GUILayout.Space(10);

            // Draw each rule UI (call SO's method)
            var list = hillSO.TilingRules;
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].DrawRuleGUI();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

    }
}


#endif
