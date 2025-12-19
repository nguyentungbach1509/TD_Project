using Game.Scripts.GamePlay;
using Game.Scripts.Player.Controller;
using Game.Scripts.StatsCharacter;
using SubScripts.Singleton;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

namespace Game.Scripts.Manager
{
    public class CameraController : SingletonBase<CameraController>
    {
        private float positionZ;
        private Vector3 velocity = Vector3.zero;
        private GameMode mode;

        public void Init()
        {
            mode = GameManager.Instance.CurrentMode;
            positionZ = transform.position.z;
        }

        public void FollowPlayer()
        {
            if (mode.SelectedUnit == null) return;

            Vector3 target = new Vector3(mode.SelectedUnit.transform.position.x, mode.SelectedUnit.transform.position.y, positionZ);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, .15f);
        }
    }
}


