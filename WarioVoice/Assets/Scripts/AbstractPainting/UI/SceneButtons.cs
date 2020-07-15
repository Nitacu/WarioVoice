using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SceneButtons : MonoBehaviour, IPointerDownHandler
{
#pragma warning disable CS0649 // El campo 'SceneButtons._text' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TextMeshProUGUI _text;
#pragma warning restore CS0649 // El campo 'SceneButtons._text' nunca se asigna y siempre tendrá el valor predeterminado null

    public enum ButtonAction
    {
        CHANGECOLOR, CHECKPAINT, LAUNCHNEXTLEVEL, TRYAGAIN
    }

#pragma warning disable CS0649 // El campo 'SceneButtons._buttonAction' nunca se asigna y siempre tendrá el valor predeterminado 
    [SerializeField] private ButtonAction _buttonAction;
#pragma warning restore CS0649 // El campo 'SceneButtons._buttonAction' nunca se asigna y siempre tendrá el valor predeterminado 

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (_buttonAction)
        {
            case ButtonAction.CHANGECOLOR:
                string color = _text.text.Remove(_text.text.Length - 1);
                //string color = _text.text;
                FindObjectOfType<AbstractPaintingManager>().parseCommand("", color);
                break;
            case ButtonAction.CHECKPAINT:
                //FindObjectOfType<AbstractPaintingManager>().evaluatePaint();
                break;
            case ButtonAction.LAUNCHNEXTLEVEL:
                FindObjectOfType<AbstractPaintingManager>().setLevel(FindObjectOfType<AbstractPaintingManager>().CurrentLevel);
                break;
            default:
                break;
        }

    }
}
