using UnityEngine;

namespace AdventureMage.Actors
{
    public class PunchingBag : MonoBehaviour
    {
        [SerializeField] private bool facingRight = true;
		public int health = 10;
        private void Awake()
        {
            if (!facingRight) {
                Flip();
            }
        }

        private void Flip()
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    void Update ()
    {
    	if (health <=  0)
    		{
    		Destroy(gameObject);
    		}
    	}
    }
}
