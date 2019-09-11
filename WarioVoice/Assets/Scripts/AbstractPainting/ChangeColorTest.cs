using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ChangeColorTest : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI _text;

    public void OnPointerDown(PointerEventData eventData)
    {
        string color = _text.text.Remove(_text.text.Length - 1);

        FindObjectOfType<AbstractPaintingManager>().parseCommand(color);
    }
}
