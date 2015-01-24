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
            currentForce = pushForce;
            Destroy(gameObject, Duration);
        }

        private void FixedUpdate() {
            transform.position -= transform.up * moveSpeed;

            float angle = transform.localEulerAngles.z;
            float sign = (angle > 0 && angle <= 180) ? -1f : 1f;
            angle = angle + (turnSpeed * sign);
            transform.localEulerAngles = new Vector3(0, 0, angle);

            elapsed += Time.deltaTime;
            currentForce = pushForce * ((Duration - elapsed) / Duration);
        }

        private void OnTriggerEnter2D(Collider2D obj) {
            obj.attachedRigidbody.AddForce(vectorFromAngle(transform.rotation.eulerAngles.z - 90, currentForce), ForceMode2D.Impulse);
        }

        private Vector2 vectorFromAngle(float angle, float magnitude) {
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 XYZdirection = rotation * new Vector3(magnitude, 0.0f, 0.0f);
            return XYZdirection; //Implicitly converts to Vector2
        }
    }
}
