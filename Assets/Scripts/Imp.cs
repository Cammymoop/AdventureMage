using UnityEngine;

namespace AdventureMage.Actors
{
    public class Imp : MonoBehaviour
    {
        [SerializeField] private bool facingRight = true;
		public int health = 10;

        private impAI AI;
        private Animator anim;
        private void Awake()
        {
            AI = GetComponent<impAI>();
            anim = GetComponentInChildren<Animator>();
            if (!facingRight) {
                Flip();
            }
        }

        public void takeDamage(int dmg)
        {
            Debug.Log("taking " + dmg);
            health -= dmg;
        }

        private void Flip()
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
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
