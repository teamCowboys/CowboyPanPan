using UnityEngine;
using System.Collections;

public class CloudMove : MonoBehaviour {

	float speed;
	Vector3 pos;
	// Use this for initialization
	void Start () 
	{
		speed = Random.Range (1,4);
		pos = new Vector3 (speed,0,0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.gameObject.transform.position += pos * Time.deltaTime;
		if (this.gameObject.transform.position.x > 20){Destroy(this.gameObject);}
	}
}
