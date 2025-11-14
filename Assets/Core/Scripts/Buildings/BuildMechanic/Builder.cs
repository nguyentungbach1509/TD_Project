using Game.Scripts.Player.Controller;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BuidlingLogic
{
    public class Builder : MonoBehaviour
    {
        private PlayerInputController inputCtrl => PlayerInputController.Instance;
        private Building currentBuild;
        private Queue<Building> queueBuildings;
        
        private bool isInit;
    }
}

