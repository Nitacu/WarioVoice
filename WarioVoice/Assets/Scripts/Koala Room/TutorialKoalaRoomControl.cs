using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKoalaRoomControl : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _welcomePanel;
    [SerializeField] private GameObject _firstPanel;
    [SerializeField] private GameObject _secondPanel;
    [SerializeField] private GameObject _thirdPanel;
    [SerializeField] private GameObject _fourthPanel;
    [SerializeField] private GameObject _tutorialButton;
    [Header("AntiPressPanels")]
    [SerializeField] private GameObject _antiPressEat;
    [SerializeField] private GameObject _antiPressShower;
    [SerializeField] private GameObject _antiPressPlay;

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
