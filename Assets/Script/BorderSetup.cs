using UnityEngine;
using System.Collections;

public class BorderSetup : MonoBehaviour {

    public BoxCollider2D bot;
    public BoxCollider2D right;
    public BoxCollider2D left;


	// Use this for initialization
	void Start () {
        Camera myCam = Camera.main;
        float screenLengthInWorld = (myCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)) - myCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f))).magnitude;
        float screenHeightInWorld = (myCam.ScreenToWorldPoint(new Vector3(Screen.height, 0f, 0f)) - myCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f))).magnitude;
        bot.gameObject.transform.position = myCam.ScreenToWorldPoint(new Vector3(Screen.width / 2f, 0f, 0f));
        bot.gameObject.transform.position -= Vector3.up / 2f;
        bot.gameObject.transform.position += Vector3.forward *10f;
        bot.size = new Vector3(screenLengthInWorld, 1f, 1f);

        right.gameObject.transform.position = myCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2f, 0f));
        right.gameObject.transform.position += Vector3.right/ 2f;
        right.size = new Vector3(1f, screenHeightInWorld, 1f);
        right.gameObject.transform.position += Vector3.forward * 10f;

        left.gameObject.transform.position = myCam.ScreenToWorldPoint(new Vector3(0f, Screen.height / 2f, 0f));
        left.gameObject.transform.position += Vector3.left / 2f;
        left.size = new Vector3(1f, screenHeightInWorld, 1f);
        left.gameObject.transform.position += Vector3.forward * 10f;
	}
	
}
