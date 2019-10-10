using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{

    public static bool _gameIsPaused;

    [SerializeField] private GameObject pauseContainer;

    [Header("Audio")]
    [SerializeField] private GameObject _musicButton;
    [SerializeField] private Sprite _musicButtonNormalSprite;
    [SerializeField] private Sprite _musicButtonPressedSprite;

    [SerializeField] private GameObject _sfxButton;
    [SerializeField] private Sprite _sfxButtonNormalSprite;
    [SerializeField] private Sprite _sfxButtonPressedSprite;

    #region AUDIO
    [SerializeField] private AudioMixer _mixer;
    public const string MUSICPARAMETER = "musicVol";
    public const string SFXPARAMETER = "sfxVol";

    private static float minVolValue = -80;
    private static float normalVolValue = 0;
    #endregion

    private void OnEnable()
    {
        pauseContainer.SetActive(false);
        Resume();


        if (AudioMixerControl.GetInstance().musicOn)
        {
            _mixer.SetFloat(MUSICPARAMETER, normalVolValue);
            _musicButton.GetComponent<Image>().sprite = _musicButtonNormalSprite;
        }
        else
        {
            _mixer.SetFloat(MUSICPARAMETER, minVolValue);
            _musicButton.GetComponent<Image>().sprite = _musicButtonPressedSprite;
        }

        if (AudioMixerControl.GetInstance().sfxOn)
        {
            _mixer.SetFloat(SFXPARAMETER, normalVolValue);
            _sfxButton.GetComponent<Image>().sprite = _sfxButtonNormalSprite;
        }
        else
        {
            _mixer.SetFloat(SFXPARAMETER, minVolValue);
            _sfxButton.GetComponent<Image>().sprite = _sfxButtonPressedSprite;
        }
    }

    public void Resume()
    {
        pauseContainer.SetActive(false);
        //activaranimacion
        Time.timeScale = 1;
        _gameIsPaused = false;
        AudioListener.pause = false;

    }

    public void Pause()
    {
        pauseContainer.SetActive(true);
        //activaranimacion
        Time.timeScale = 0;
        _gameIsPaused = true;

        AudioListener.pause = true;
    }

    public void exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(ChangeScene.SPIKINGLISHMENU);
    }

    //AUDIOCONTOL

    public void setupMusic()
    {
        Debug.Log("audiomixercontrol musicon: " + AudioMixerControl.GetInstance().musicOn);

        if (AudioMixerControl.GetInstance().musicOn)
        {
            _mixer.SetFloat(MUSICPARAMETER, minVolValue);
            _musicButton.GetComponent<Image>().sprite = _musicButtonPressedSprite;
            AudioMixerControl.GetInstance().musicOn = false;
        }
        else
        {
            _mixer.SetFloat(MUSICPARAMETER, normalVolValue);
            _musicButton.GetComponent<Image>().sprite = _musicButtonNormalSprite;
            AudioMixerControl.GetInstance().musicOn = true;
        }       
    }

    public void setupSFX()
    {
        if (AudioMixerControl.GetInstance().sfxOn)
        {
            _mixer.SetFloat(SFXPARAMETER, minVolValue);
            _sfxButton.GetComponent<Image>().sprite = _sfxButtonPressedSprite;
            AudioMixerControl.GetInstance().sfxOn = false;
        }
        else
        {
            _mixer.SetFloat(SFXPARAMETER, normalVolValue);
            _sfxButton.GetComponent<Image>().sprite = _sfxButtonNormalSprite;
            AudioMixerControl.GetInstance().sfxOn = true;
        }
    }
}
