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
    private const string WRITENAME_SPA = "Como te llamas?";
    private const string CONTINUE_ENG = "What do you want to do?";
    private const string CONTINUE_SPA = "Que quieres hacer?";
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
    [SerializeField] private GameObject _deleteConfirmationUI;
    [SerializeField] private GameObject _moreDataUI;
    [SerializeField] private GameObject _vocabulary;

    private float _timeToHideSlots;
    private float _trackTimeToHide;

    private bool hideSlots;

    private PlayerInformation _playerInfSelected;
    public PlayerInformation PlayerInfSelected
    {
        get { return _playerInfSelected; }
        set { _playerInfSelected = value; }
    }

    private int _currentSlotSelected;
    public int CurrentSlotSelected
    {
        get { return _currentSlotSelected; }
        set { _currentSlotSelected = value; }
    }

    public enum Menus
    {
        SLOTS,
        SHOWSLOT,
        CREATESLOT,
        RESET,
        MOREDATA,
        VOCABULARY
    }

    private void Awake()
    {
        //GameManager.ResetInstance();
    }

    private void Start()
    {
        _mainTextEnglish.text = CHOOSEFILE_ENG;
        _mainTextSpanish.text = CHOOSEFILE_SPA;
        _timeToHideSlots = _fadeAppearClip.averageDuration;

        _slotContainer.SetActive(true);
        _createASlotUI.SetActive(false);
        _showDataUI.SetActive(false);
        _deleteConfirmationUI.SetActive(false);
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


        //_createASlotUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_DISAPPEAR));

        //showMore(_playerInfSelected, _playerInfSelected.slotNumber);
    }


    public IEnumerator showMenu(FileManager.Menus _menuType)
    {

        _slotContainer.GetComponent<Animator>().Play(Animator.StringToHash(FADE_DISAPPEAR));
        _createASlotUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_DISAPPEAR));
        _showDataUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_DISAPPEAR));
        _deleteConfirmationUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_DISAPPEAR));
        _moreDataUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_DISAPPEAR));
        _vocabulary.GetComponent<Animator>().Play(Animator.StringToHash(FADE_DISAPPEAR));

        yield return new WaitForSeconds(_timeToHideSlots);

        _slotContainer.SetActive(false);
        _createASlotUI.SetActive(false);
        _showDataUI.SetActive(false);
        _deleteConfirmationUI.SetActive(false);
        _moreDataUI.SetActive(false);
        _vocabulary.SetActive(false);


        switch (_menuType)
        {
            case Menus.SLOTS:
                _slotContainer.SetActive(true);
                _slotContainer.GetComponent<Animator>().Play(Animator.StringToHash(FADE_APPEAR));
                _mainTextEnglish.text = CHOOSEFILE_ENG;
                _mainTextSpanish.text = CHOOSEFILE_SPA;
                break;
            case Menus.SHOWSLOT:
                _showDataUI.SetActive(true);
                _showDataUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_APPEAR));
                _mainTextEnglish.text = CONTINUE_ENG;
                _mainTextSpanish.text = CONTINUE_SPA;
                break;
            case Menus.CREATESLOT:
                _createASlotUI.SetActive(true);
                _createASlotUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_APPEAR));
                _mainTextEnglish.text = WRITENAME_ENG;
                _mainTextSpanish.text = WRITENAME_SPA;
                break;
            case Menus.RESET:
                _deleteConfirmationUI.SetActive(true);
                _deleteConfirmationUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_APPEAR));
                break;
            case Menus.MOREDATA:
                _moreDataUI.SetActive(true);
                _moreDataUI.GetComponent<Animator>().Play(Animator.StringToHash(FADE_APPEAR));
                break;
            case Menus.VOCABULARY:
                _vocabulary.SetActive(true);
                _vocabulary.GetComponent<Animator>().Play(Animator.StringToHash(FADE_APPEAR));
                break;
        }
    }

    public void backToSlots()
    {
        StartCoroutine(showMenu(Menus.SLOTS));


    }

}
