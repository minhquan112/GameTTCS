using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameVSPLAYER;

namespace EmptyScriptVSPLAYER {

    public class EmptyScriptPVP : MonoBehaviour
    {
        public int id;
        public GameObject camera;

        private void OnMouseDown () {
            camera.GetComponent<GameScriptPVP> ().SpawnPVP (this.gameObject, id);

        }
    }
}