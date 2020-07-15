using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{


    [SerializeField] private Animator _anim;
    private const string ANIMATION_APPEAR = "PMAppear";
    private const string ANIMATION_DESAPPEAR = "PMDesappear";
#pragma warning disable CS0649 // El campo 'PauseMenu._appearAnimation' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] AnimationClip _appearAnimation;
#pragma warning restore CS0649 // El campo 'PauseMenu._appearAnimation' nunca se asigna y siempre tendrá el valor predeterminado null

    public static bool _gameIsPaused;

#pragma warning disable CS0649 // El campo 'PauseMenu.pauseContainer' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject pauseContainer;
#pragma warning restore CS0649 // El campo 'PauseMenu.pauseContainer' nunca se asigna y siempre tendrá el valor predeterminado null

    [Header("Audio")]
#pragma warning disable CS0649 // El campo 'PauseMenu._musicButton' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _musicButton;
#pragma warning restore CS0649 // El campo 'PauseMenu._musicButton' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'PauseMenu._musicButtonNormalSprite' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _musicButtonNormalSprite;
#pragma warning restore CS0649 // El campo 'PauseMenu._musicButtonNormalSprite' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'PauseMenu._musicButtonPressedSprite' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _musicButtonPressedSprite;
#pragma warning restore CS0649 // El campo 'PauseMenu._musicButtonPressedSprite' nunca se asigna y siempre tendrá el valor predeterminado null

#pragma warning disable CS0649 // El campo 'PauseMenu._sfxButton' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _sfxButton;
#pragma warning restore CS0649 // El campo 'PauseMenu._sfxButton' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'PauseMenu._sfxButtonNormalSprite' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _sfxButtonNormalSprite;
#pragma warning restore CS0649 // El campo 'PauseMenu._sfxButtonNormalSprite' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'PauseMenu._sfxButtonPressedSprite' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _sfxButtonPressedSprite;
#pragma warning restore CS0649 // El campo 'PauseMenu._sfxButtonPressedSprite' nunca se asigna y siempre tendrá el valor predeterminado null

    #region AUDIO
#pragma warning disable CS0649 // El campo 'PauseMenu._mixer' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioMixer _mixer;
#pragma warning restore CS0649 // El campo 'PauseMenu._mixer' nunca se asigna y siempre tendrá el valor predeterminado null
    public const string MUSICPARAMETER = "musicVol";
    public const string SFXPARAMETER = "sfxVol";

    private static float minVolValue = -80;
    private static float normalVolValue = 0;
    #endregion

    private void OnEnable()
    {
        pauseContainer.SetActive(false);
        Resume();

        _anim =  GetComponent<Animator>();

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
        //activaranimacion
        Time.timeScale = 1;
        _gameIsPaused = false;
        AudioListener.pause = false;

        _anim.Play(Animator.StringToHash(ANIMATION_DESAPPEAR));

        StartCoroutine(ResumeCoroutine());
    }

    IEnumerator ResumeCoroutine()
    {
        yield return new WaitForSeconds(_appearAnimation.averageDuration);

        pauseContainer.SetActive(false);
    }


    public void Pause()
    {
        pauseContainer.SetActive(true);
        Debug.Log("Animator: " + _anim.gameObject.name);
        _anim.Play(Animator.StringToHash(ANIMATION_APPEAR));

        StartCoroutine(PauseCoroutine());
    }

    IEnumerator PauseCoroutine()
    {
        yield return new WaitForSeconds(_appearAnimation.averageDuration);

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
