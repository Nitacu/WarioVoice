using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SceneButtons : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI _text;

    public enum ButtonAction
    {
        CHANGECOLOR, CHECKPAINT, LAUNCHNEXTLEVEL, TRYAGAIN
    }

    [SerializeField] private ButtonAction _buttonAction;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (_buttonAction)
        {
            case ButtonAction.CHANGECOLOR:
                string color = _text.text.Remove(_text.text.Length - 1);
                //string color = _text.text;
                FindObjectOfType<AbstractPaintingManager>().parseCommand(color);
                break;
            case ButtonAction.CHECKPAINT:
                FindObjectOfType<AbstractPaintingManager>().evaluatePaint();
                break;
            case ButtonAction.LAUNCHNEXTLEVEL:
                FindObjectOfType<AbstractPaintingManager>().setLevel(FindObjectOfType<AbstractPaintingManager>().CurrentLevel);
                break;
            case ButtonAction.TRYAGAIN:
                FindObjectOfType<AbstractPaintingManager>().keepTrying();
                break;
            default:
                break;
        }

    }
}
