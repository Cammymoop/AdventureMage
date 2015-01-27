using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class impAI : MonoBehaviour {

	public Transform Target;
	public float UpdateRate = 2f;
	public Path path;
	public float speed = 300f;
	public ForceMode2D fMode;
	[HideInInspector]
	public bool pathIsEnded = false;
	
	public float nextWayPointDistance = 3;
	
	private Seeker seeker;
	private Rigidbody2D rb;
	private int currentWaypoint = 0;
	
	
	void Start () {
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		
		if (Target == null){
			Debug.Log("no player found!!!!");
			return;
		}
		
		seeker.StartPath (transform.position, Target.position, OnPathComplete );
		
		StartCoroutine (UpdatePath ());	
	}
	IEnumerator UpdatePath (){
		if (Target == null){
			return false;
		}
		seeker.StartPath (transform.position, Target.position, OnPathComplete );
		
		yield return new WaitForSeconds (1f/UpdateRate);
		StartCoroutine (UpdatePath());
		
	}
	
	public void OnPathComplete(Path p){
		//Debug.Log ("got a path, did it have an error?" +p.error);
		if (!p.error){
			path= p;
			currentWaypoint = 0;
		}
	}
	
	void FixedUpdate (){
		
		if(Target == null){
			//Поиск игрока
			GameObject searchPlayer = GameObject.FindGameObjectWithTag("Player");
			if (searchPlayer != null){
				Target = searchPlayer.transform;
				seeker.StartPath(transform.position,Target.position, OnPathComplete);
				StartCoroutine (UpdatePath());
			} else {
				return;
			}
		}
		if (path == null)
			return;
		
		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded)
				return;
			//Debug.Log("end reached.");
			pathIsEnded = true;
			return;
		}
		
		
		pathIsEnded = false;
		
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		
		rb.AddForce (dir, fMode);
		float dist = Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]);
		if (dist < nextWayPointDistance ){
			currentWaypoint++;
			return;
		}
		
	}
}
