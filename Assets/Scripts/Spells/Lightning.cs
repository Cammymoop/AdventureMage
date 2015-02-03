using UnityEngine;

namespace AdventureMage.Actors.Spells
{
    public class Lightning : MonoBehaviour
    {
    
    	public int damage = 4;
        public float duration = 0.6f;

        private void Awake()
        {
            Destroy(gameObject, duration);
			transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }

        private void OnTriggerEnter2D(Collider2D obj) {
            obj.gameObject.BroadcastMessage("takeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
