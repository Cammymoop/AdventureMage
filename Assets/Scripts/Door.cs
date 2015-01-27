using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace AdventureMage.Environment
{
    public class Door : MonoBehaviour
    {
        private void Awake()
        {
        }

        private void OnTriggerStay2D(Collider2D obj) {
            if (obj.gameObject.tag == "Player" && CrossPlatformInputManager.GetAxis("Vertical") > 0) {
                Application.LoadLevel("insideHouse");
            }
        }

        void Update()
        {
        }
    }
}
