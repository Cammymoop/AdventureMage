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
        }

        private void OnTriggerEnter2D(Collider2D obj) {
            obj.gameObject.BroadcastMessage("takeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
