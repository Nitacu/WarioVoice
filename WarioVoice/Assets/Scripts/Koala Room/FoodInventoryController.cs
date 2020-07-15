using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodInventoryController : MonoBehaviour
{
    [Header("UI")]
#pragma warning disable CS0649 // El campo 'FoodInventoryController._foodCanvas' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _foodCanvas;
#pragma warning restore CS0649 // El campo 'FoodInventoryController._foodCanvas' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'FoodInventoryController._eatButton' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Button _eatButton;
#pragma warning restore CS0649 // El campo 'FoodInventoryController._eatButton' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'FoodInventoryController._playToGetMoreText' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _playToGetMoreText;
#pragma warning restore CS0649 // El campo 'FoodInventoryController._playToGetMoreText' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Button _closeButton;
#pragma warning disable CS0649 // El campo 'FoodInventoryController._leavesCount' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Text _leavesCount;
#pragma warning restore CS0649 // El campo 'FoodInventoryController._leavesCount' nunca se asigna y siempre tendrá el valor predeterminado null
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
