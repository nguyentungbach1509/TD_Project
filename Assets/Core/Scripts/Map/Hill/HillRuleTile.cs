using System;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile;
using Color = UnityEngine.Color;

namespace Game.Scripts.Map.Hills
{
    public enum EHillRuleType { Border, Inner }

    [CreateAssetMenu(fileName = "HillRuletile", menuName = "Data/Tile/HillRuletile")]
    public class HillRuleTile : RuleTile
    {
        [SerializeField] private List<HillTilingRule> rules = new List<HillTilingRule>();
        [SerializeField] private RuleTile ruleTile;

        public HashSet<Vector3Int> borderList = new();

        public List<HillTilingRule> TilingRules => rules;

        // -------------------------
        //  GENERATE RULES
        // -------------------------
        public void GenerateRuleTile()
        {
            //m_TilingRules = new List<TilingRule>(ruleTile.m_TilingRules);
            
            if (ruleTile == null)
            {
                Debug.LogWarning("RuleTile is missing!");
                return;
            }

            rules.Clear();
            for (int i = 0; i < ruleTile.m_TilingRules.Count; i++)
            {
                rules.Add(new HillTilingRule(ruleTile.m_TilingRules[i]));
            }
        }

        #region Override RuleTile Methods
        /*public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject instantiatedGameObject)
        {
            bool baseResult = base.StartUp(position, tilemap, instantiatedGameObject);

            // Lấy rule thực sự đã match
            TilingRule matchedRule = GetMatchingRule(position, tilemap);
            
            if (matchedRule != null)
            {
                int index = m_TilingRules.IndexOf(matchedRule);
                HillTilingRule hillTiling = rules[index];

                if (hillTiling!=null && hillTiling.TilingType == EHillRuleType.Border)
                {
                    borderList.Add(position);
                    Debug.Log($"Tile at {position} uses RuleTile: {name}");
                }
            }

            return baseResult;
        }*/

        private TilingRule GetMatchingRule(Vector3Int position, ITilemap tilemap)
        {
            foreach (var rule in m_TilingRules)
            {
                Matrix4x4 dummy = Matrix4x4.identity;
                if (RuleMatches(rule, position, tilemap, ref dummy))
                    return rule;
            }
            return null;
        }

        public bool IsBorderTile(Vector3Int position, ITilemap tilemap)
        {
            Matrix4x4 dummy = Matrix4x4.identity;
            for (int i = 0; i < m_TilingRules.Count; i++)
            {
                if (RuleMatches(m_TilingRules[i], position, tilemap, ref dummy))
                {
                    return rules[i].TilingType == EHillRuleType.Border;
                }
            }
            return false;
        }

        #endregion
    }

    // =============================================================
    //  HILL TILING RULE
    // =============================================================
    [System.Serializable]
    public class HillTilingRule 
    {
        [SerializeField] private int id;
        [SerializeField] private TilingRule tilingRule;
        [SerializeField] private EHillRuleType tilingType;

        public int Id => id;
        public TilingRule Tiling => tilingRule;

        public EHillRuleType TilingType => tilingType;
            
        public HillTilingRule(TilingRule rule)
        {
            tilingRule = rule;
            id = rule.m_Id;
        }

        public void DrawSpriteThumbnail(float size = 64f)
        {
            Rect drawRect = GUILayoutUtility.GetRect(size, size, GUILayout.ExpandWidth(false)); // giới hạn width

            if (tilingRule.m_Sprites == null || tilingRule.m_Sprites.Length == 0 || tilingRule.m_Sprites[0] == null)
            {
                EditorGUI.DrawRect(drawRect, Color.gray);
                GUI.Label(drawRect, "No\nSprite", new GUIStyle()
                {
                    alignment = TextAnchor.MiddleCenter,
                    wordWrap = true
                });
                return;
            }

            Texture2D tex = tilingRule.m_Sprites[0].texture;
            Rect rect = tilingRule.m_Sprites[0].rect;

            // Crop chính giữa để vừa khung vuông
            float texAspect = rect.width / rect.height;
            Rect uv = new Rect();
            if (texAspect > 1f)
            {
                float cropWidth = rect.height;
                uv.width = cropWidth / tex.width;
                uv.height = rect.height / tex.height;
                uv.x = (rect.x + (rect.width - cropWidth) / 2f) / tex.width;
                uv.y = rect.y / tex.height;
            }
            else
            {
                float cropHeight = rect.width;
                uv.width = rect.width / tex.width;
                uv.height = cropHeight / tex.height;
                uv.x = rect.x / tex.width;
                uv.y = (rect.y + (rect.height - cropHeight) / 2f) / tex.height;
            }

            GUI.DrawTextureWithTexCoords(drawRect, tex, uv, true);
        }


        // -------------------------
        //  DRAW GUI ELEMENT OF A RULE (EDITOR WILL CALL)
        // -------------------------
#if UNITY_EDITOR
        public void DrawRuleGUI()
        {
            GUILayout.BeginHorizontal(EditorStyles.helpBox);

            // Thumbnail
            DrawSpriteThumbnail(64f);

            GUILayout.Space(4); // khoảng cách giữa thumbnail và fields

            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Rule", EditorStyles.boldLabel);
            RuleInspectorOnGUI();
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            GUILayout.Space(4);

        }

        private void RuleInspectorOnGUI()
        {
            GUI.enabled = false;

            id = (int)EditorGUILayout.IntField(new GUIContent("Id"), id);

            tilingRule.m_GameObject = (GameObject)EditorGUILayout.ObjectField(
                 new GUIContent("Game Object"),
                 tilingRule.m_GameObject,
                 typeof(GameObject),
                 false
            );

            tilingRule.m_ColliderType = (Tile.ColliderType)EditorGUILayout.EnumPopup(
                new GUIContent("Collider Type"),
                tilingRule.m_ColliderType
            );


            tilingRule.m_Output = (RuleTile.TilingRuleOutput.OutputSprite)EditorGUILayout.EnumPopup(
                new GUIContent("OutputSprite"),
                tilingRule.m_Output
            );


            if (tilingRule.m_Output == RuleTile.TilingRuleOutput.OutputSprite.Animation)
            {
                tilingRule.m_MinAnimationSpeed = EditorGUILayout.FloatField(
                    new GUIContent("Min Anim Speed"),
                    tilingRule.m_MinAnimationSpeed
                );

                tilingRule.m_MaxAnimationSpeed = EditorGUILayout.FloatField(
                    new GUIContent("Max Anim Speed"),
                    tilingRule.m_MaxAnimationSpeed
                );
            }

            if (tilingRule.m_Output == RuleTile.TilingRuleOutput.OutputSprite.Random)
            {
                tilingRule.m_PerlinScale =
                    EditorGUILayout.Slider(
                        new GUIContent("PerlinScale"),
                        tilingRule.m_PerlinScale, 0.001f, 0.999f);


                tilingRule.m_RandomTransform = (RuleTile.TilingRuleOutput.Transform)EditorGUILayout.EnumPopup(
                    new GUIContent("Random Transform"),
                    tilingRule.m_RandomTransform
                );

            }
            
            GUI.enabled = true;
            // Enum TilingType
            tilingType = (EHillRuleType)EditorGUILayout.EnumPopup(new GUIContent("Type"), tilingType);
        }
#endif
    }
}
