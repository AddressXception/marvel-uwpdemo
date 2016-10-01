using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCharacter : MonoBehaviour
{

    private GameObject SuperHero;

    public GameObject IronManPrefab;

    public GameObject CaptainPrefab;

    public GameObject ThorPrefab;

    public Camera MainCamera { get { return GetComponent<Camera>(); } }

	// Use this for initialization
	void Start ()
	{
	    SuperHero = IronManPrefab;
	    Instantiate(SuperHero);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
