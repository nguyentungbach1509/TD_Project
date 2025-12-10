using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile;

namespace Game.Scripts.Map.Hills
{
    public enum EHillRuleType { Border, Inner }

    [CreateAssetMenu(fileName = "HillRuletile", menuName = "Data/Tile/HillRuletile")]
    public class HillRuleTile : ScriptableObject
    {
        [SerializeField] private List<HillTilingRule> rules = new List<HillTilingRule>();
        [SerializeField] private RuleTile ruleTile;

        public List<HillTilingRule> TilingRules => rules;
        public RuleTile RuleTile => ruleTile;

        // -------------------------
        //  GENERATE RULES
        // -------------------------
        public void GenerateRuleTile()
        {
            if (ruleTile == null)
            {
                Debug.LogWarning("RuleTile is missing!");
                return;
            }

            rules.Clear();
            foreach (var r in ruleTile.m_TilingRules)
            {
                rules.Add(new HillTilingRule(r));
            }
        }

        // -------------------------
        //  DRAW GUI ELEMENT OF A RULE (EDITOR WILL CALL)
        // -------------------------
#if UNITY_EDITOR
        public void DrawRuleGUI(HillTilingRule rule, int index)
        {
            GUILayout.BeginHorizontal(EditorStyles.helpBox);

            // Preview avatar
            Texture2D tex = rule.GetSpriteTexture();
            if (tex != null)
                GUILayout.Label(tex, GUILayout.Width(50), GUILayout.Height(50));
            else
                GUILayout.Label("No\nSprite", GUILayout.Width(50), GUILayout.Height(50));

            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Rule " + index, EditorStyles.boldLabel);

            rule.TilingType = (EHillRuleType)EditorGUILayout.EnumPopup("Type", rule.TilingType);

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Space(4);
        }
#endif
    }

    // =============================================================
    //  HILL TILING RULE
    // =============================================================
    [System.Serializable]
    public class HillTilingRule
    {
        [SerializeField] private TilingRule tiling;
        [SerializeField] private EHillRuleType tilingType;

        public TilingRule Tiling => tiling;

        public EHillRuleType TilingType
        {
            get => tilingType;
            set => tilingType = value;
        }

        public HillTilingRule(TilingRule rule)
        {
            tiling = rule;
        }

        // Convert sprite -> Texture
        public Texture2D GetSpriteTexture()
        {
            if (tiling.m_Sprites == null || tiling.m_Sprites.Length == 0)
                return null;

            Sprite sprite = tiling.m_Sprites[0];
            if (sprite == null)
                return null;

            Rect r = sprite.rect;
            Texture2D tex = new Texture2D((int)r.width, (int)r.height);
            Color[] colors = sprite.texture.GetPixels(
                (int)r.x, (int)r.y, (int)r.width, (int)r.height
            );

            tex.SetPixels(colors);
            tex.Apply();
            return tex;
        }
    }
}
