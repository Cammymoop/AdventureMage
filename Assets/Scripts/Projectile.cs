using UnityEngine;

namespace AdventureMage.Actors
{
    public class Projectile : MonoBehaviour
    {
        public int damage = 4;

        public float speed = 30.6f;
        public float duration = 20f;

        [SerializeField] public bool stick = true;

        private AdventureMage.DamageType dmg;
        private bool isActive = false;

        private void Awake()
        {
            Destroy(gameObject, duration);

            dmg = new AdventureMage.DamageType(damage);
            setInactive();
        }

        private void Fire() {
            setActive();
            rigidbody2D.AddRelativeForce(new Vector2(speed, 0), ForceMode2D.Impulse);
        }

        private void setActive() {
            isActive = true;
            rigidbody2D.isKinematic = false;
        }

        private void setInactive() {
            isActive = false;
            rigidbody2D.isKinematic = true;
        }

        private void FixedUpdate() {
        }

        private void OnTriggerEnter2D(Collider2D obj) {
            if (isActive) {
                obj.gameObject.BroadcastMessage("takeDamage", dmg, SendMessageOptions.DontRequireReceiver);

                if (stick) {
                    setInactive();
                    transform.parent = obj.transform;
                }
            }
        }
    }
}
