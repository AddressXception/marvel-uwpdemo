using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCharacter : MonoBehaviour
{

    private GameObject _superHero;

    public GameObject IronManPrefab;

    public GameObject CaptainPrefab;

    public GameObject ThorPrefab;

	// Use this for initialization
	void Start ()
	{
	    _superHero = IronManPrefab;

	    Instantiate(_superHero);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
