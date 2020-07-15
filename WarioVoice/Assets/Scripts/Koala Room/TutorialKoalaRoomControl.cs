using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKoalaRoomControl : MonoBehaviour
{
    [Header("UI")]
#pragma warning disable CS0649 // El campo 'TutorialKoalaRoomControl._welcomePanel' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _welcomePanel;
#pragma warning restore CS0649 // El campo 'TutorialKoalaRoomControl._welcomePanel' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'TutorialKoalaRoomControl._firstPanel' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _firstPanel;
#pragma warning restore CS0649 // El campo 'TutorialKoalaRoomControl._firstPanel' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'TutorialKoalaRoomControl._secondPanel' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _secondPanel;
#pragma warning restore CS0649 // El campo 'TutorialKoalaRoomControl._secondPanel' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'TutorialKoalaRoomControl._thirdPanel' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _thirdPanel;
#pragma warning restore CS0649 // El campo 'TutorialKoalaRoomControl._thirdPanel' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'TutorialKoalaRoomControl._fourthPanel' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _fourthPanel;
#pragma warning restore CS0649 // El campo 'TutorialKoalaRoomControl._fourthPanel' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'TutorialKoalaRoomControl._tutorialButton' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _tutorialButton;
#pragma warning restore CS0649 // El campo 'TutorialKoalaRoomControl._tutorialButton' nunca se asigna y siempre tendrá el valor predeterminado null
    [Header("AntiPressPanels")]
#pragma warning disable CS0649 // El campo 'TutorialKoalaRoomControl._antiPressEat' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _antiPressEat;
#pragma warning restore CS0649 // El campo 'TutorialKoalaRoomControl._antiPressEat' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'TutorialKoalaRoomControl._antiPressShower' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _antiPressShower;
#pragma warning restore CS0649 // El campo 'TutorialKoalaRoomControl._antiPressShower' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'TutorialKoalaRoomControl._antiPressPlay' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _antiPressPlay;
#pragma warning restore CS0649 // El campo 'TutorialKoalaRoomControl._antiPressPlay' nunca se asigna y siempre tendrá el valor predeterminado null

    private int _currentActivePanel = 0;
    private List<GameObject> _tutorialPanels = new List<GameObject>();
    public static string TUTORIAL_COMPLETED_KEY = "KoalaRoomTutorial";

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString(TUTORIAL_COMPLETED_KEY) == "true")
        {
            _antiPressEat.SetActive(false);
            _antiPressPlay.SetActive(false);
            _antiPressShower.SetActive(false);
            _tutorialButton.SetActive(true);
            gameObject.SetActive(false);           
        }        
    }

    private void OnEnable()
    {
        restartTutorial();
    }

    private void fillPanelsInOrder()
    {
        _tutorialPanels.Add(_welcomePanel);
        _tutorialPanels.Add(_firstPanel);
        _tutorialPanels.Add(_secondPanel);
        _tutorialPanels.Add(_thirdPanel);
        _tutorialPanels.Add(_fourthPanel);
    }

    private void showCurrentPanel()
    {
        for(int i = 0; i < _tutorialPanels.Count; i++)
        {
            if(i == _currentActivePanel)
            {
                _tutorialPanels[i].SetActive(true);
                deactivateAntiPress(_tutorialPanels[i]);
            }
            else
            {
                _tutorialPanels[i].SetActive(false);
            }
        }
    }

    private void deactivateAntiPress(GameObject _object)
    {
        if (_object == _secondPanel)
        {
            _antiPressPlay.SetActive(false);
        }

        if (_object == _thirdPanel)
        {
            _antiPressShower.SetActive(false);
        }

        if (_object == _fourthPanel)
        {
            _antiPressEat.SetActive(false);
        }
    }

    public void goToNextPanel()
    {
        _currentActivePanel++;
        if(_currentActivePanel >= _tutorialPanels.Count)
        {
            PlayerPrefs.SetString(TUTORIAL_COMPLETED_KEY, "true");
            _tutorialButton.SetActive(true);
            gameObject.SetActive(false);           
        }
        else
        {
            showCurrentPanel();
        }
    }

    public void restartTutorial()
    {
        _antiPressEat.SetActive(true);
        _antiPressPlay.SetActive(true);
        _antiPressShower.SetActive(true);
        _currentActivePanel = 0;
        _tutorialPanels.Clear();
        fillPanelsInOrder();
        showCurrentPanel();
    }
}
