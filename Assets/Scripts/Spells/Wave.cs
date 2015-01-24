using UnityEngine;

namespace AdventureMage.Actors.Spells
{
    public class Wave : MonoBehaviour
    {
    
    	public float turnSpeed = 2.6f;
    	public float moveSpeed = 0.2f;
    	public float Duration = 0.5f;
        private void Awake()
        {
            Destroy(gameObject, Duration);
        }

        private void FixedUpdate() {
            transform.position -= transform.up * moveSpeed;

            float angle = transform.localEulerAngles.z;
            float sign = (angle > 0 && angle <= 180) ? -1f : 1f;
            angle = angle + (turnSpeed * sign);
            transform.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}
