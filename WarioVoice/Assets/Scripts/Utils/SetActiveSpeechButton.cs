using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetActiveSpeechButton : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'SetActiveSpeechButton._speechButton' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _speechButton;
#pragma warning restore CS0649 // El campo 'SetActiveSpeechButton._speechButton' nunca se asigna y siempre tendrá el valor predeterminado null
    //[SerializeField] private GameObject _speechButtonImage;

#pragma warning disable CS0649 // El campo 'SetActiveSpeechButton._deactiveColor' nunca se asigna y siempre tendrá el valor predeterminado 
    [SerializeField] private Color _deactiveColor;
#pragma warning restore CS0649 // El campo 'SetActiveSpeechButton._deactiveColor' nunca se asigna y siempre tendrá el valor predeterminado 

    public void setButton(bool active)
    {
        if (active)
        {
            _speechButton.GetComponent<Button>().interactable = true;
            _speechButton.GetComponent<EventTrigger>().enabled = true;
            _speechButton.GetComponent<Image>().color = Color.white;
            //_speechButtonImage.GetComponent<Image>().color = Color.white;
        }
        else
        {
            _speechButton.GetComponent<Button>().interactable = false;
            _speechButton.GetComponent<EventTrigger>().enabled = false;
            _speechButton.GetComponent<Image>().color = _deactiveColor;
            //_speechButtonImage.GetComponent<Image>().color = _deactiveColor;
        }

    }
}
