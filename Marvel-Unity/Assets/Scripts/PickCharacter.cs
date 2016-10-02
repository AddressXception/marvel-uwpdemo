using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCharacter : MonoBehaviour
{

    private GameObject _superHero;

    public GameObject IronManPrefab;

    public GameObject CaptainPrefab;

    public GameObject ThorPrefab;

    public enum CharacterModel
    {
        Captainamerica,
        IronMan,
        Thor
    }

    public static CharacterModel SelectedCharacter = CharacterModel.IronMan;

    private static bool _shouldUpdateScene = false;

    public static void SetSelectedCharacter(CharacterModel character)
    {
        SelectedCharacter = character;
        _shouldUpdateScene = true;
    }

	// Use this for initialization
	void Start ()
	{
        _shouldUpdateScene = true;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!_shouldUpdateScene) return;

        _shouldUpdateScene = false;

        if (_superHero != null)
            Destroy(_superHero);

        switch (SelectedCharacter)
        {
            case CharacterModel.Captainamerica:
                _superHero = CaptainPrefab;
                break;
            case CharacterModel.IronMan:
                _superHero = IronManPrefab;
                break;
            case CharacterModel.Thor:
                _superHero = ThorPrefab;
                break;
        }

        Instantiate(_superHero);
    }
}
