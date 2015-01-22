using UnityEngine;

namespace AdventureMage.Actors.Spells
{
    public class Wave : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject, 0.5f);
        }

        private void Update() {
            transform.position -= transform.up * 0.2f;

            float angle = transform.localEulerAngles.z;
            float sign = (angle > 0 && angle <= 180) ? -1f : 1f;
            angle = angle + (2.6f * sign);
            transform.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}
