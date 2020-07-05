using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitoMoodController : MonoBehaviour
{
    [Header("Mood Sprites")]
    [SerializeField] private Sprite _happy;
    [SerializeField] private Sprite _sad;
    [SerializeField] private Sprite _superHappy;
    [SerializeField] private Sprite _normal;
    [Header("Tito")]
    [SerializeField] private GameObject _titoHead;
    [Tooltip("Mood level goes from 0 - 100, indicating what is the current mood of Tito, Sad (0-25), Normal (26-50), Happy (51-75), SuperHappy (76-100)")]
    [Range(0,100)]
    [SerializeField] private float _moodlevel = 51;
    [Header("MoodBar")]
    [SerializeField] private Image _headIcon;
    [SerializeField] private Image _moodBar;

    public enum ENUM_TitoMood
    {
        HAPPY,
        SAD,
        SUPERHAPPY,
        NORMAL
    }

    private Animator _animator;
    private ENUM_TitoMood _mood;

    private const string SUPER_HAPPY_ANIMATION = "SuperHappy_Tito";
    private const string HAPPY_ANIMATION = "Happy_Tito";
    private const string NORMAL_ANIMATION = "Normal_Tito";
    private const string SAD_ANIMATION = "Sad_Tito";
    private const string TITO_MOOD_KEY = "MoodValue";

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _moodlevel = PlayerPrefs.GetInt(TITO_MOOD_KEY);
        setMood();
        StartCoroutine(waitForFillingBar());
    }

    public void addMoodPoints(float moodPoints)
    {
        _moodlevel += moodPoints;
        if (_moodlevel > 100)
        {
            _moodlevel = 100;
        }
        if(_moodlevel < 0)
        {
            _moodlevel = 0;
        }
        PlayerPrefs.SetInt(TITO_MOOD_KEY, (int)_moodlevel);
        setMood();
    }
    private void setMood()
    {
        if(_moodlevel <= 100 && _moodlevel > 75)
        {
            _mood = ENUM_TitoMood.SUPERHAPPY;
        }else if (_moodlevel <= 75 && _moodlevel > 50)
        {
            _mood = ENUM_TitoMood.HAPPY;
        }
        else if (_moodlevel <= 50 && _moodlevel > 25)
        {
            _mood = ENUM_TitoMood.NORMAL;
        }
        else if (_moodlevel <= 25 && _moodlevel >= 0)
        {
            _mood = ENUM_TitoMood.SAD;
        }

        
        switchMoodHeadSprite(_mood);
        switchMoodAnimation(_mood);
        updateMoodBar();
    }
    private void updateMoodBar()
    {
        Debug.Log(_moodlevel);
        _moodBar.fillAmount = (float)(_moodlevel/ 100);
    }
    private void switchMoodAnimation(ENUM_TitoMood mood)
    {
        switch (mood)
        {
            case ENUM_TitoMood.HAPPY:
                _animator.Play(Animator.StringToHash(HAPPY_ANIMATION), -1, 0f);
                break;
            case ENUM_TitoMood.SAD:
                _animator.Play(Animator.StringToHash(SAD_ANIMATION), -1, 0f);
                break;
            case ENUM_TitoMood.SUPERHAPPY:
                _animator.Play(Animator.StringToHash(SUPER_HAPPY_ANIMATION), -1, 0f);
                break;
            case ENUM_TitoMood.NORMAL:
                _animator.Play(Animator.StringToHash(NORMAL_ANIMATION), -1, 0f);
                break;
        }
    }
    private void switchMoodHeadSprite(ENUM_TitoMood mood)
    {
        switch (mood)
        {
            case ENUM_TitoMood.HAPPY:
                _titoHead.GetComponent<SpriteRenderer>().sprite = _happy;
                _headIcon.sprite = _happy;
                break;
            case ENUM_TitoMood.SAD:
                _titoHead.GetComponent<SpriteRenderer>().sprite = _sad;
                _headIcon.sprite = _sad;
                break;
            case ENUM_TitoMood.SUPERHAPPY:
                _titoHead.GetComponent<SpriteRenderer>().sprite = _superHappy;
                _headIcon.sprite = _superHappy;
                break;
            case ENUM_TitoMood.NORMAL:
                _titoHead.GetComponent<SpriteRenderer>().sprite = _normal;
                _headIcon.sprite = _normal;
                break;
        }
    }

    IEnumerator waitForFillingBar()
    {
        yield return new WaitForEndOfFrame();
        updateMoodBar();
    }
}
