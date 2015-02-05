using UnityEngine;

namespace AdventureMage.Actors
{
    public class Imp : MonoBehaviour
    {
        [SerializeField] private bool facingRight = true;
		public int health = 10;
		public int damage = 3;
        private impAI AI;
        private Animator anim;
		private AdventureMage.DamageType dmg; 
		
		
		private void Awake()
        {
			dmg = new AdventureMage.DamageType(damage);
			AI = GetComponent<impAI>();
            anim = GetComponentInChildren<Animator>();
            if (!facingRight) {
                Flip();
            }
        }

        public void takeDamage(AdventureMage.DamageType dmg)
        {
            health -= dmg.damage;
        }

        private void Flip()
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
		
		private void OnCollisionEnter2D(Collision2D coll) {
			Debug.Log ("did damage");
			coll.gameObject.BroadcastMessage("takeDamage", dmg, SendMessageOptions.DontRequireReceiver);
		}
		
		
        void Update()
        {
            anim.SetFloat("targetDistance", Vector3.Distance(transform.position, AI.Target.transform.position));
            if (health <=  0)
            {
                Destroy(gameObject);
            }
        }
    }
}
