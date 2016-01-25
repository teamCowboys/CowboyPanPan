﻿using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
    
    public float speed;
    public Vector3 direction;
    public Transform trs;
    public bool isReady;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (isReady)
        {                
            Move();
            
            if(trs.position.y < -10)
            {
                Destroy(this.gameObject);
            }
        }
	}

    public void Init( Vector3 dir)
    {

        
        trs = this.gameObject.GetComponent<Transform>();
        isReady = true;
        direction = dir;
    }

    void Move()
    {
        this.direction.z = 0;
        //Debug.Log(direction);
        this.transform.position += this.direction*Time.deltaTime*speed;
        
        //this.transform.position = Vector3.Lerp(this.transform.position, direction, 0.01f);
        //this.transform.position = new Vector3(pos.x+direction.x * speed*Time.deltaTime , pos.y + direction.y * speed * Time.deltaTime, pos.z );
    }
}
