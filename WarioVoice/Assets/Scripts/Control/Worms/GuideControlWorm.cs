using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideControlWorm : MonoBehaviour
{
    private SetActiveSpeechButton _speechButton;
    private ConvertAngles _convertAngles;
    [SerializeField] private GameObject _imageAngle;
    [SerializeField] private GameObject _imagePower;
    [SerializeField] private GameObject _panelInstruction;
    [SerializeField] private GameObject _boxHelp;
    private void Start()
    {
        _speechButton = FindObjectOfType<SetActiveSpeechButton>();
        _convertAngles = FindObjectOfType<ConvertAngles>();
        if (_convertAngles.TutorialMode)
        {
            _convertAngles.allowPoint();
        }
    }

    public void desactiveAction()
    {
        _boxHelp.SetActive(false);
        _convertAngles.AllowShoot = false;
        _convertAngles.AllowPoint = false;
        _panelInstruction.SetActive(true);
        _speechButton.setButton(false);
        _imageAngle.SetActive(false);
        _imagePower.SetActive(false);
    }

    public void activeKeepAction()
    {
        if (!_convertAngles.TutorialMode)
        {
            _boxHelp.SetActive(true);
            _speechButton.setButton(true);
        }
        else
        {
            activePower();
        }
    }

    public void activeAngle()
    {
        _panelInstruction.SetActive(false);
        _speechButton.setButton(true);
        _imageAngle.SetActive(true);
        _imagePower.SetActive(false);
    }

    public void activePower()
    {
        _panelInstruction.SetActive(false);
        _speechButton.setButton(true);
        _imageAngle.SetActive(false);
        _imagePower.SetActive(true);

    }

    public void desactiveAll()
    {
        _speechButton.setButton(false);
        _imageAngle.SetActive(false);
        _imagePower.SetActive(false);
        _boxHelp.SetActive(false);
    }
}
