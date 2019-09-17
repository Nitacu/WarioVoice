using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideControlWorm : MonoBehaviour
{
    private SetActiveSpeechButton _speechButton;
    [SerializeField] private GameObject _imageAngle;
    [SerializeField] private GameObject _imagePower;

    private void Start()
    {
        _speechButton = FindObjectOfType<SetActiveSpeechButton>();
    }

    public void activeAngle()
    {
        Debug.Log("angulo");
        _speechButton.setButton(true);
        _imageAngle.SetActive(true);
        _imagePower.SetActive(false);
    }

    public void activePower()
    {
        Debug.Log("poder");
        _speechButton.setButton(true);
        _imageAngle.SetActive(false);
        _imagePower.SetActive(true);
    }

    public void desactiveAll()
    {
        Debug.Log("bye todo");
        _speechButton.setButton(false);
        _imageAngle.SetActive(false);
        _imagePower.SetActive(false);
    }
}
