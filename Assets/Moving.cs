using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(PrintAxis());
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.S)) this.transform.Translate(Vector2.down * 0.1f);
        if (Input.GetKey(KeyCode.W)) this.transform.Translate(Vector2.up * 0.1f);
        if (Input.GetKey(KeyCode.A)) this.transform.Translate(Vector2.left * 0.1f);
        if (Input.GetKey(KeyCode.D)) this.transform.Translate(Vector2.right * 0.1f);
    }

    IEnumerator PrintAxis()
    {
        while (true)
        {
            Debug.Log(this.transform.position.x + ", " + this.transform.position.y + ", " + this.transform.position.z);
            yield return new WaitForSeconds(1.0f);
        }

    }
}
