using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTitoMood : MonoBehaviour
{
    [Header("Mood Sprites")]
    [SerializeField] private Sprite _happy;
    [SerializeField] private Sprite _sad;
    [SerializeField] private Sprite _superHappy;
    [SerializeField] private Sprite _normal;
    [Header("Face")]
    [SerializeField] private GameObject _tito;
    private int _moodlevel;

    private void Awake()
    {
        _moodlevel = PlayerPrefs.GetInt("MoodValue");

        if (_moodlevel <= 100 && _moodlevel > 75)
        {
            GameManager.GetInstance().TitoMood = TitoMoodController.ENUM_TitoMood.SUPERHAPPY;
            _tito.GetComponent<Image>().sprite = _superHappy;
        }
        else if (_moodlevel <= 75 && _moodlevel > 50)
        {
            GameManager.GetInstance().TitoMood = TitoMoodController.ENUM_TitoMood.HAPPY;
            _tito.GetComponent<Image>().sprite = _happy;
        }
        else if (_moodlevel <= 50 && _moodlevel > 25)
        {
            GameManager.GetInstance().TitoMood = TitoMoodController.ENUM_TitoMood.NORMAL;
            _tito.GetComponent<Image>().sprite = _normal;
        }
        else if (_moodlevel <= 25 && _moodlevel >= 0)
        {
            GameManager.GetInstance().TitoMood = TitoMoodController.ENUM_TitoMood.SAD;
            _tito.GetComponent<Image>().sprite = _sad;
        }


    }

}
