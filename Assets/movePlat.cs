using UnityEngine;
using System.Collections;

public class movePlat : MonoBehaviour {

	public float originX;
	public int platOffSet;
	public int length;
	public int speed;
	void Start () {
		originX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(Mathf.PingPong((Time.time* speed)+platOffSet , length) + originX, transform.position.y, transform.position.z);
	}
}
