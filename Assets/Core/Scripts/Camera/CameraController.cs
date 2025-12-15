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
        private SurvivalMode survivalMode;

        public void Init()
        {
            survivalMode = SurvivalMode.Instance;
            positionZ = transform.position.z;
        }

        public void FollowPlayer()
        {
            if (survivalMode.SelectedUnit == null) return;

            Vector3 target = new Vector3(survivalMode.SelectedUnit.transform.position.x, survivalMode.SelectedUnit.transform.position.y, positionZ);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, .15f);
        }
    }
}


