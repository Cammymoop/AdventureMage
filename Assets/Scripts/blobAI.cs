using UnityEngine;
using System.Collections;

public class blobAI : MonoBehaviour {

		
		public float originX;
		public int platOffSet;
		public int length;
		public int speed;
		public int health = 10;
		void Start () {
			originX = transform.position.x;
		}
		public void takeDamage(AdventureMage.DamageType dmg)
			{
			health -= dmg.damage;
			}
		// Update is called once per frame
		void Update () {
			transform.position = new Vector3(Mathf.PingPong((Time.time* speed)+platOffSet , length) + originX, transform.position.y, transform.position.z);
		if(health <= 0){
			Destroy(gameObject);
			}
		}
	}
	
