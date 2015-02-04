using UnityEngine;
using System.Collections;

public class basicAI : MonoBehaviour {

	
	
		public Transform target;
		public int moveSpeed;
		public int maxDistance;
		
		
		private Transform myTransform;
		
		void Awake() {
			myTransform = transform;
		}    
		
	
		void Start () {
			GameObject go = GameObject.FindGameObjectWithTag("Player");
			
			target = go.transform;
			
			maxDistance = 2;
		}        
		
		
		
		
		
		void FixedUpdate () {
			
			
			if (target.position.x < myTransform.position.x){ myTransform.position -= myTransform.right * moveSpeed * Time.deltaTime;} 
		else if (target.position.x > myTransform.position.x){ myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;} 
			
		
		}
		
	
	
}
