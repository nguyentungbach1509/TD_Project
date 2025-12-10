#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Game.Scripts.Map.Hills;

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
                hillSO.DrawRuleGUI(list[i], i);
                // draw TilingRule inspector UI
                DrawTilingRuleInspector(list[i].Tiling);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawTilingRuleInspector(RuleTile.TilingRule rule)
    {
        if (rule == null) return;

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.LabelField("Tiling Rule", EditorStyles.boldLabel);


        EditorGUILayout.EndVertical();
    }
}
#endif
