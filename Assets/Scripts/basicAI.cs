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

        private float stopFactor = 0.7f;

        public int damage = 4;

        public int health = 15;
        public bool isRight = true;

        private float lastCharge = 0f;

        private string curState = "idle";
        private bool facingRight = true;

        private Animator anim;
        private AdventureMage.DamageType dmg;

        void Awake() {
            facingRight = isRight;

            anim = GetComponent<Animator>();
            dmg = new AdventureMage.DamageType(damage);

            moveSpeed = moveSpeed * rigidbody2D.mass;
            chargeSpeed = chargeSpeed * rigidbody2D.mass;
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
                        stopMoving();
                    }
                    break;
                case "idle":
                    if (Time.time - lastCharge > chargeDelay && distance > minDistance && distance < maxDistance) {
                        faceTarget();
                        setState("charging");
                        lastCharge = Time.time;
                    } else if (distance < maxDistance) {
                        setState("defending");
                        stopMoving();
                    }
                    break;
                default:
                    setState("idle");
                    break;
            }

            float velocityX = rigidbody2D.velocity.x;
            float diff = move - (velocityX * rigidbody2D.mass);
            move = diff;

            rigidbody2D.AddForce(new Vector2(move * 1.4f, 0));

            if (health <= 0)
            {
                Destroy(gameObject);
            }
		}

        private void OnCollisionEnter2D(Collision2D coll) {
            if (curState == "charging") {
                coll.gameObject.BroadcastMessage("takeDamage", dmg, SendMessageOptions.DontRequireReceiver);
            }
        }

        private void stopMoving() {
            Vector2 currentVelocity = rigidbody2D.velocity;
            Vector2 oppositeForce = -currentVelocity;
            rigidbody2D.AddRelativeForce(new Vector2(oppositeForce.x * stopFactor * rigidbody2D.mass, 0), ForceMode2D.Impulse);
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
