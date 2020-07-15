using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTitoMood : MonoBehaviour
{
    [Header("Mood Sprites")]
#pragma warning disable CS0649 // El campo 'LoadTitoMood._happy' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _happy;
#pragma warning restore CS0649 // El campo 'LoadTitoMood._happy' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'LoadTitoMood._sad' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _sad;
#pragma warning restore CS0649 // El campo 'LoadTitoMood._sad' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'LoadTitoMood._superHappy' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _superHappy;
#pragma warning restore CS0649 // El campo 'LoadTitoMood._superHappy' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'LoadTitoMood._normal' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _normal;
#pragma warning restore CS0649 // El campo 'LoadTitoMood._normal' nunca se asigna y siempre tendrá el valor predeterminado null
    [Header("Face")]
#pragma warning disable CS0649 // El campo 'LoadTitoMood._tito' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _tito;
#pragma warning restore CS0649 // El campo 'LoadTitoMood._tito' nunca se asigna y siempre tendrá el valor predeterminado null
    private int _moodlevel;

    private void Awake()
    {
        _moodlevel = PlayerPrefs.GetInt("MoodValue");

        if (_moodlevel <= 100 && _moodlevel > 75)
        {
            GameManager.GetInstance().TitoMood = TitoMoodController.ENUM_TitoMood.SUPERHAPPY;
            GameManager.GetInstance().maxNumberOfLives = 5;
            _tito.GetComponent<Image>().sprite = _superHappy;
        }
        else if (_moodlevel <= 75 && _moodlevel > 50)
        {
            GameManager.GetInstance().TitoMood = TitoMoodController.ENUM_TitoMood.HAPPY;
            GameManager.GetInstance().maxNumberOfLives = 4;
            _tito.GetComponent<Image>().sprite = _happy;
        }
        else if (_moodlevel <= 50 && _moodlevel > 25)
        {
            GameManager.GetInstance().TitoMood = TitoMoodController.ENUM_TitoMood.NORMAL;
            GameManager.GetInstance().maxNumberOfLives = 3;
            _tito.GetComponent<Image>().sprite = _normal;
        }
        else if (_moodlevel <= 25 && _moodlevel >= 0)
        {
            GameManager.GetInstance().TitoMood = TitoMoodController.ENUM_TitoMood.SAD;
            GameManager.GetInstance().maxNumberOfLives = 2;
            _tito.GetComponent<Image>().sprite = _sad;
        }

    }

}
