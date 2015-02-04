using UnityEngine;
using System.Collections;

namespace AdventureMage.Actors
{
    public class basicAI : MonoBehaviour 
    {
        public Transform target;
        public float moveSpeed = 1f;
        public float chargeSpeed = 7f;
        public float chargeTime = 1f;
        public float chargeDelay = 2f;

        public float minDistance = 3.5f;
        public float maxDistance = 15f;
        public float verticleIgnore = 7f;

        public int health = 15;
        public bool isRight = true;

        private float lastCharge = 0f;

        private string curState = "idle";
        private bool facingRight = true;

        private Animator anim;

        void Awake() {
            facingRight = isRight;

            anim = GetComponent<Animator>();
        }    

        void Start () {
            if (!target) {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }        
	
        public void takeDamage(AdventureMage.DamageType dmg)
        {
            if (curState == "defending") {
                return;
            }
			health -= dmg.damage;
        }

        void FixedUpdate () {
            float distance = Mathf.Abs(target.position.x - transform.position.x);
            float distanceY = Mathf.Abs(target.position.y - transform.position.y);
            if (distanceY >= verticleIgnore) {
                return;
            }

            float move = 0f;
            if (distance > maxDistance) {
                curState = "idle";
                anim.SetBool("Blocking", false);
                anim.SetBool("Attacking", false);
            }
            switch (curState)
            {
                case "defending":
                    faceTarget();
                    move = (facingRight ? -1 : 1) * moveSpeed;
                    if (Time.time - lastCharge > chargeDelay && distance > minDistance) {
                        setState("charging");
                        lastCharge = Time.time;
                    }
                    break;
                case "charging":
                    move = (facingRight ? 1 : -1) * chargeSpeed;
                    if (Time.time - lastCharge > chargeTime) {
                        setState("idle");
                        lastCharge = Time.time;
                    }
                    break;
                case "idle":
                    if (Time.time - lastCharge > chargeDelay && distance > minDistance && distance < maxDistance) {
                        setState("charging");
                        lastCharge = Time.time;
                    } else if (distance < maxDistance) {
                        setState("defending");
                    }
                    break;
                default:
                    setState("idle");
                    break;
            }
            rigidbody2D.velocity = new Vector2(move, rigidbody2D.velocity.y);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
		}

        private void setState(string state) {
            anim.SetBool("Blocking", (state == "defending"));
            anim.SetBool("Attacking", (state == "charging"));
            curState = state;
        }

        private void faceTarget() {
            if (target.position.x < transform.position.x) { 
                if (facingRight) {
                    Flip();
                }
            } else if (target.position.x > transform.position.x) {
                if (!facingRight) {
                    Flip();
                }
            } 
        }

        private void Flip() {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
