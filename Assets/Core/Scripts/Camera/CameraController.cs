using Game.Scripts.Player.Controller;
using SubScript.Singleton;
using UnityEngine;

namespace Game.Scripts.Manager
{
    public class CameraController : SingletonBase<CameraController>
    {
        private PlayerController player;
        private float positionZ;
        private Vector3 velocity = Vector3.zero;

        public void Init(PlayerController player)
        {
            this.player = player;
            positionZ = transform.position.z;
        }

        public void FollowPlayer()
        {
            /*Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y, positionZ);
            transform.position = Vector3.Lerp(transform.position, target, 5 * Time.deltaTime);*/
            if (player == null) return;

            Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y, positionZ);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, .15f);
        }
    }
}


