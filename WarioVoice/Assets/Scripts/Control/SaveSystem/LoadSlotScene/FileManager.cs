using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FileManager : MonoBehaviour
{
    #region DIALOGS
    private const string CHOOSEFILE_ENG = "Choose a File";
    private const string CHOOSEFILE_SPA = "Elige un archivo";
    private const string WRITENAME_ENG = "What's your name?";
    private const string WRITENAME_SPA = "Cómo te llamas?";
    private const string CONTINUE_ENG = "What do you want to do?";
    private const string CONTINUE_SPA = "Qué quieres hacer?";
    #endregion

    #region ClipNames
    private const string FADE_DISAPPEAR = "Disappear";
    private const string FADE_APPEAR = "Appear";
    #endregion

    [Header("TEXTS")]
    [SerializeField] private TextMeshProUGUI _mainTextEnglish;
    [SerializeField] private TextMeshProUGUI _mainTextSpanish;

    [Header("UI ELEMENTS")]
    [SerializeField] private AnimationClip _fadeAppearClip;
    [SerializeField] private GameObject _slotContainer;
    [SerializeField] private GameObject _createASlotUI;
    [SerializeField] private GameObject _showDataUI;

    private float _timeToHideSlots;
    private float _trackTimeToHide;

    private bool hideSlots;

    private PlayerInformation _playerInfSelected;
    public PlayerInformation PlayerInfSelected
    {
        get { return _playerInfSelected; }
    }

    private int _currentSlotSelected;
    public int CurrentSlotSelected
    {
        get { return _currentSlotSelected; }
    }

    private void Start()
    {
        _mainTextEnglish.text = CHOOSEFILE_ENG;
        _mainTextSpanish.text = CHOOSEFILE_SPA;
        _timeToHideSlots = _fadeAppearClip.averageDuration;

        _slotContainer.SetActive(true);
        _createASlotUI.SetActive(false);
        _showDataUI.SetActive(false);
    }

    private void Update()
    {
        /*if (hideSlots)
        {
            if (_trackTimeToHide > 0)
            {
                _trackTimeToHide -= Time.deltaTime;

                float alpha = _trackTimeToHide / _timeToHideSlots;

                _slotContainer.GetComponent<CanvasGroup>().alpha = alpha;


            }
            else
            {
                _slotContainer.SetActive(false);

                hideSlots = false;
            }
        }*/
    }

    public void createNewSlot(string name)
    {
        PlayerInformation newSlot = new PlayerInformation();
        newSlot.slotNumber = _currentSlotSelected;
        newSlot.playerName = name;
        newSlot.bossesDefeated = 0;

        string json = JsonUtility.ToJson(newSlot);

        string newKey = SaveSystem.PLAYERDATA_PLAYERPREFCODE + newSlot.slotNumber.ToString();

        PlayerPrefs.SetString(newKey, json);

        _playerInfSelected = newSlot;
        _createASlotUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_DISAPPEAR));

        showMore(_playerInfSelected, _playerInfSelected.slotNumber);
    }
    

    public void showMore(PlayerInformation playerInformationToShow, int slotSelected)
    {
        //hideSlots = true;
        //_trackTimeToHide = _timeToHideSlots;

        _slotContainer.GetComponent<Animator>().Play(Animator.StringToHash(FADE_DISAPPEAR));

        if (playerInformationToShow != null)
        {
            _playerInfSelected = playerInformationToShow;
            _currentSlotSelected = _playerInfSelected.slotNumber;
            StartCoroutine(chargeShowMore(true));

            //_mainTextEnglish.text = CONTINUE_ENG;
            //_mainTextSpanish.text = CONTINUE_SPA;
        }
        else
        {
            _currentSlotSelected = slotSelected;
            StartCoroutine(chargeShowMore(false));

            //_mainTextEnglish.text = WRITENAME_ENG;
            //_mainTextSpanish.text = WRITENAME_SPA;
        }

    }
    

    IEnumerator chargeShowMore(bool hasPlayerInformation)
    {
        yield return new WaitForSeconds(_timeToHideSlots);
        _slotContainer.SetActive(false);

        if (hasPlayerInformation)
        {
            _showDataUI.SetActive(true);
            _showDataUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_APPEAR));
            _mainTextEnglish.text = CONTINUE_ENG;
            _mainTextSpanish.text = CONTINUE_SPA;
        }
        else
        {
            _createASlotUI.SetActive(true);
            _createASlotUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_APPEAR));
            _mainTextEnglish.text = WRITENAME_ENG;
            _mainTextSpanish.text = WRITENAME_SPA;
        }

    }

    public void backToSlots()
    {
        try
        {
            _createASlotUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_DISAPPEAR));
        }
        catch (System.Exception)
        {

            throw;
        }   
        try
        {
            _showDataUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_DISAPPEAR));
        }
        catch (System.Exception)
        {
            throw;
        }

        StartCoroutine(chargeBackToSlots());
    }

    IEnumerator chargeBackToSlots()
    {
        yield return new WaitForSeconds(_timeToHideSlots);

        _createASlotUI.SetActive(false);
        _showDataUI.SetActive(false);

        _mainTextEnglish.text = CHOOSEFILE_ENG;
        _mainTextSpanish.text = CHOOSEFILE_SPA;
        _slotContainer.SetActive(true);
        _slotContainer.GetComponent<Animator>().Play(Animator.StringToHash(FADE_APPEAR));
    }
}
