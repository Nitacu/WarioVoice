using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideControlWorm : MonoBehaviour
{
    private SetActiveSpeechButton _speechButton;
    private ConvertAngles _convertAngles;
    [SerializeField] private GameObject _imageAngle;
    [SerializeField] private GameObject _imagePower;
    private void Start()
    {
        _speechButton = FindObjectOfType<SetActiveSpeechButton>();
        _convertAngles = FindObjectOfType<ConvertAngles>();
        if (_convertAngles.TutorialMode)
        {
            _convertAngles.allowPoint();
        }
    }

    public void activeKeepAction()
    {
        if (!_convertAngles.TutorialMode)
        {
            _speechButton.setButton(true);
        }
        else
        {
            activePower();
        }
    }

    public void activeAngle()
    {
        _speechButton.setButton(true);
        _imageAngle.SetActive(true);
        _imagePower.SetActive(false);
    }

    public void activePower()
    {
        _speechButton.setButton(true);
        _imageAngle.SetActive(false);
        _imagePower.SetActive(true);

    }

    public void desactiveAll()
    {
        _speechButton.setButton(false);
        _imageAngle.SetActive(false);
        _imagePower.SetActive(false);
    }
}
