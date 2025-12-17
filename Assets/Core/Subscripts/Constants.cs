using UnityEngine;
namespace Subscripts
{
    public static class Constants
    {
        #region Input Key
        public static readonly string Move = "Move";
        public static readonly string Left_Click = "Left Click";
        public static readonly string Right_Click = "Right Click";
        public static readonly string Mouse_Hover = "Mouse Hover";
        #endregion

        #region UI Keys
        public static readonly string ResourceTxt = "ResourceTxt";
        public static readonly string SlotHUD = "SlotHUD";
        #endregion

        #region Resource Obstacle Keys
        public static readonly string GoldOre = "GoldOre";
        #endregion
    }

    public static class BuildingKey
    {
        #region Wall Keys
        public static readonly string Wall_Up = "Wall Up";
        public static readonly string Wall_Down = "Wall Down";
        public static readonly string Wall_Left = "Wall Left";
        public static readonly string Wall_Right = "Wall Right";
        #endregion

        public static readonly string Farm = "Farm";
        public static readonly string Basement = "Basement";
    }

    public static class UnitKey
    {
        #region Builder Keys
        public static readonly string Player = "Player";
        #endregion
    }

}

