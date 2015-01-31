using UnityEngine;

namespace AdventureMage.Actors.Spells
{
    public class Wave : MonoBehaviour
    {
    
    	public float turnSpeed = 2.6f;
    	public float moveSpeed = 0.2f;
    	public float Duration = 0.5f;

        public float pushForce = 5f;
        private float currentForce;
        private float elapsed = 0f;

        private void Awake()
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            currentForce = pushForce;
            Destroy(gameObject, Duration);
        }

        private void FixedUpdate() {
            transform.position += (transform.right * moveSpeed) * transform.localScale.x;

            float angle = transform.localEulerAngles.z;
            float sign = (angle > 0 && angle <= 180) ? -1f : 1f;
            angle = angle + (turnSpeed * sign);
            transform.localEulerAngles = new Vector3(0, 0, angle);

            elapsed += Time.deltaTime;
            currentForce = pushForce * ((Duration - elapsed) / Duration);
        }

        private void OnTriggerEnter2D(Collider2D obj) {
            float angle = transform.rotation.eulerAngles.z - (Mathf.Sign(transform.localScale.x) * 90);
            obj.attachedRigidbody.AddForce(vectorFromAngle(angle, currentForce), ForceMode2D.Impulse);

            obj.gameObject.BroadcastMessage("takeDamage", 1, SendMessageOptions.DontRequireReceiver);
        }

        private Vector2 vectorFromAngle(float angle, float magnitude) {
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 XYZdirection = rotation * new Vector3(0.0f, magnitude, 0.0f);
            return XYZdirection; //Implicitly converts to Vector2
        }
    }
}
