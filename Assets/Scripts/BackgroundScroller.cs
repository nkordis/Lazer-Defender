using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    [SerializeField] float backgroundScrollingSpeed = 0.5f;
    Material myMyterial;
    Vector2 offSet;

	// Use this for initialization
	void Start () {
        myMyterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, backgroundScrollingSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        myMyterial.mainTextureOffset += offSet * Time.deltaTime;
	}
}
