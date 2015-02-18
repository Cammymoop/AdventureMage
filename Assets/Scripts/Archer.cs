using UnityEngine;
using System.Collections;

namespace AdventureMage.Actors
{
    public class Archer : MonoBehaviour 
    {
        public Transform target;
        public float moveSpeed = 1f;
        public float shotDelay = 0.8f;

        public float minDistance = 6.5f;
        public float maxDistance = 19f;
        public float verticleIgnore = 7f;

        public Transform projectile;

        public float projDeltaX = 2.2f;
        public float projDeltaY = 0f;

        private float evadeDistance = 2.5f;

        private float stopFactor = 0.7f;

        public int damage = 4;

        public int health = 10;
        public bool isRight = true;

        private float lastShot = 0f;

        private string curState = "idle";
        private bool facingRight = true;

        private Animator anim;
        private AdventureMage.DamageType dmg;

        void Awake() {
            facingRight = isRight;

            anim = GetComponent<Animator>();
            dmg = new AdventureMage.DamageType(damage);

            moveSpeed = moveSpeed * rigidbody2D.mass;
        }    

        void Start () {
            if (!target) {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }        
	
        public void takeDamage(AdventureMage.DamageType dmg)
        {
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
                setState("idle");
                stopMoving();
            }
            switch (curState)
            {
                case "evading":
                    faceTarget();
                    Flip();
                    move = (facingRight ? 1 : -1) * moveSpeed;
                    if (distance > evadeDistance + minDistance) {
                        setState("attacking");
                        faceTarget();
                        stopMoving();
                    }
                    break;
                case "attacking":
                    if (Time.time - lastShot > shotDelay && distance > minDistance) {
                        startShooting();
                        lastShot = Time.time;
                    }
                    break;
                case "idle":
                    if (distance < maxDistance) {
                        setState("attacking");
                    }
                    break;
                default:
                    setState("idle");
                    break;
            }
            if (curState != "evading" && distance < minDistance) {
                setState("evading");
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
        
		private void stopShooting() {
			anim.SetBool("Shooting", false);
		}
		
		private void startShooting() {
			anim.SetBool("Shooting", true);
		}

        private void shootAThing() {
            float sign = (facingRight ? 1f : -1f);
            Vector3 position = new Vector3(transform.position.x + (projDeltaX * sign), transform.position.y + projDeltaY, transform.position.z);

            Quaternion rot = Quaternion.identity;
            float angle = Vector2.Angle(target.position - position, new Vector2(1, 0)) * sign;
            rot.eulerAngles = new Vector3(0, 0, angle * sign);

            GameObject obj = Instantiate(projectile, position, rot) as GameObject;
            obj.BroadcastMessage("Fire");
        }

        private void stopMoving() {
            Vector2 currentVelocity = rigidbody2D.velocity;
            Vector2 oppositeForce = -currentVelocity;
            rigidbody2D.AddRelativeForce(new Vector2(oppositeForce.x * stopFactor * rigidbody2D.mass, 0), ForceMode2D.Impulse);
        }

        private void setState(string state) {
            anim.SetBool("Evading", (state == "evading"));
            anim.SetBool("Attacking", (state == "attacking"));
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
