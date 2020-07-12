using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodInventoryController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _foodCanvas;
    [SerializeField] private Button _eatButton;
    [SerializeField] private GameObject _playToGetMoreText;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Text _leavesCount;
    [SerializeField] private Text _moodGiverText;

    private MoodActionsController _moodControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        _moodControllerScript = GetComponent<MoodActionsController>();
        _leavesCount.text = "Leaves: " + GameManager.GetInstance().Money;
    }

    public void openFoodPanel()
    {
        _foodCanvas.SetActive(true);
        _leavesCount.text = "Leaves: " + GameManager.GetInstance().Money;
        if(GameManager.GetInstance().Money == 0)
        {
            _eatButton.gameObject.SetActive(false);
            _playToGetMoreText.gameObject.SetActive(true);
        }
        else
        {
            _eatButton.gameObject.SetActive(true);
            _playToGetMoreText.gameObject.SetActive(false);
        }
    }

    public void eatLeaf()
    {
        ControlMoney.LoseMoney(1);
        _moodControllerScript.eatWithTito();
        backToGarden();
    }

    public void backToGarden()
    {
        _foodCanvas.SetActive(false);
    }
}
