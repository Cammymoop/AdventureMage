using UnityEngine;
using System.Collections;

public class blobAI : MonoBehaviour {

		
		public float originX;
		public int platOffSet;
		public int length;
		public int speed;
		public int health = 10;
		public int damage = 1;
		private AdventureMage.DamageType dmg; 
		void Awake (){
		dmg = new AdventureMage.DamageType(damage);
		}
		void Start () {
			originX = transform.position.x;
		}
		public void takeDamage(AdventureMage.DamageType dmg)
        {
			health -= dmg.damage;
        }
		
		private void OnCollisionEnter2D(Collision2D coll) {
            coll.gameObject.BroadcastMessage("takeDamage", dmg, SendMessageOptions.DontRequireReceiver);
        }
		
		void Update () {
			transform.position = new Vector3(Mathf.PingPong((Time.time* speed)+platOffSet , length) + originX, transform.position.y, transform.position.z);
            if(health <= 0){
                Destroy(gameObject);
			}
		}
	}
	
