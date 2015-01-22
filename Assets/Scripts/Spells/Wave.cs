using UnityEngine;

namespace AdventureMage.Actors.Spells
{
    public class Wave : MonoBehaviour
    {
        [SerializeField] private bool facingRight = true;

        private void Awake()
        {
            if (!facingRight) {
                facingRight = true;
                Flip();
            }

            Destroy(gameObject, 0.5f);
        }

        private void Update() {
            transform.position -= transform.up * 0.3f;

            Quaternion rot = transform.localRotation;
            rot.z = rot.z - ((facingRight) ? 0.03f : -0.03f);
            transform.localRotation = rot;
        }

        private void Flip()
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

            Quaternion rotation = transform.localRotation;
            rotation.z *= -1;
            transform.localRotation = rotation;
        }
    }
}
