using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace AdventureMage.Environment
{
    public class Door : MonoBehaviour
    {
        public string targetScene;
        private bool canUse = true;

        private void OnTriggerEnter2D(Collider2D obj) {
            if (CrossPlatformInputManager.GetAxis("Vertical") > 0) {
                canUse = false;
            }
        }

        private void OnTriggerStay2D(Collider2D obj) {
            if (canUse) {
                if (obj.gameObject.tag == "Player" && CrossPlatformInputManager.GetAxis("Vertical") > 0) {
                    foreach (Transform door in transform.parent) {
                        if (!System.Object.ReferenceEquals(door, transform)) {
                            obj.gameObject.transform.position = door.position;
                            GameObject mainCam = GameObject.FindWithTag("MainCamera");
                            mainCam.BroadcastMessage("centerNow");
                        }
                    }
                }
            }
            else if (CrossPlatformInputManager.GetAxis("Vertical") <= 0) {
                canUse = true;
            }
        }

        void Update()
        {
        }
    }
}
