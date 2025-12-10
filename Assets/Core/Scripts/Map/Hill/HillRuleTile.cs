using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile;

namespace Game.Scripts.Map.Hills
{
    public enum EHillRuleType {
        Border, Inner
    }

    [CreateAssetMenu(fileName = "HillRuletile", menuName = "Data/Tile/HillRuletile")]
    public class HillRuleTile : ScriptableObject
    {
        [SerializeField] List<HillTilingRule> rules;
        [SerializeField] RuleTile ruleTile;
        public List<HillTilingRule> TilingRules => rules;
        public RuleTile RuleTile => ruleTile;
        
        public void GenerateRuleTile()
        {
            if (ruleTile == null)
            {
                Debug.LogWarning("RuleTile is missing!");
                return;
            }
            if(rules != null) rules.Clear();
            else rules = new List<HillTilingRule>();
            
            for(int i = 0; i < ruleTile.m_TilingRules.Count; i++)
            {
                rules.Add(new HillTilingRule(ruleTile.m_TilingRules[i]));
            }
        }

    }

    [System.Serializable] 
    public class HillTilingRule
    {
        [SerializeField] TilingRule tiling;
        [SerializeField] EHillRuleType tilingType;

        public TilingRule Tiling => tiling;
        public EHillRuleType TilingType => tilingType;

        public HillTilingRule(TilingRule rule)
        {
            tiling = rule;
        }
    }
}


