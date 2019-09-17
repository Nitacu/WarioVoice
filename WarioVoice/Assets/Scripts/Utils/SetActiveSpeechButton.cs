using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetActiveSpeechButton : MonoBehaviour
{
    [SerializeField] private GameObject _speechButton;
    [SerializeField] private GameObject _speechButtonImage;

    [SerializeField] private Color _deactiveColor;

    public void setButton(bool active)
    {
        if (active)
        {
            _speechButton.GetComponent<Button>().interactable = true;
            _speechButton.GetComponent<EventTrigger>().enabled = true;
            _speechButton.GetComponent<Image>().color = Color.white;
            _speechButtonImage.GetComponent<Image>().color = Color.white;
        }
        else
        {
            _speechButton.GetComponent<Button>().interactable = false;
            _speechButton.GetComponent<EventTrigger>().enabled = false;
            _speechButton.GetComponent<Image>().color = _deactiveColor;
            _speechButtonImage.GetComponent<Image>().color = _deactiveColor;
        }

    }
}
