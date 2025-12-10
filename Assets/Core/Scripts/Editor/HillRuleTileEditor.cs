using Game.Scripts.Map.Hills;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.EditorCustomize.HillEditor
{
    [CustomEditor(typeof(HillRuleTile))]
    public class HillRuleTileEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            HillRuleTile main = (HillRuleTile)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Generate From SourceA"))
            {
                main.GenerateRuleTile();
                EditorUtility.SetDirty(main);
            }
        }
    }
}

