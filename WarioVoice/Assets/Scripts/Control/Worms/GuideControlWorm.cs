using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideControlWorm : MonoBehaviour
{
    [SerializeField]private SetActiveSpeechButton _speechButton;
    private ConvertAngles _convertAngles;
#pragma warning disable CS0649 // El campo 'GuideControlWorm._imageAngle' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _imageAngle;
#pragma warning restore CS0649 // El campo 'GuideControlWorm._imageAngle' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'GuideControlWorm._imagePower' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _imagePower;
#pragma warning restore CS0649 // El campo 'GuideControlWorm._imagePower' nunca se asigna y siempre tendrá el valor predeterminado null
    [Header("Textos de ejemplo")]
#pragma warning disable CS0649 // El campo 'GuideControlWorm._text' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TMP_Text _text;
#pragma warning restore CS0649 // El campo 'GuideControlWorm._text' nunca se asigna y siempre tendrá el valor predeterminado null
    private Color _colorTutorial = Color.gray;
    [SerializeField]private float _time = 0;
    private const string EXAMPLE_DEGREES = "Ejemplo: 20 Degrees";
    private const string EXAMPLE_PERCENT = "Ejemplo: 90 Percent";
    private const string EXAMPLE_REMINDER = "Ejemplo 20 Desgress ó 90 Percent.";
    private const string WARNING_FOR_STAND_STILL = "Comandante. Recuerde indicar grados y potencia.";

    private void Start()
    {
        _speechButton = FindObjectOfType<SetActiveSpeechButton>();
        _convertAngles = FindObjectOfType<ConvertAngles>();
        if (_convertAngles.TutorialMode)
        {
            _text.text = EXAMPLE_DEGREES;
            _text.color = _colorTutorial;
            _convertAngles.allowPoint();
        }
    }

    public void restarTime()
    {
        _time = 0;
        _text.color = Color.black;
    }

    private void Update()
    {
        if (!_convertAngles.TutorialMode)
        {
            _time += Time.deltaTime;

            if (_time>=30)
            {
                _imageAngle.SetActive(true);
                _imageAngle.GetComponentInChildren<TMP_Text>().text = WARNING_FOR_STAND_STILL;

                _text.text = EXAMPLE_REMINDER;
                _text.color = _colorTutorial;
            }
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

    public void resetColorText()
    {
        _text.color = Color.black;
    }

    public void activePower()
    {
        _speechButton.setButton(true);
        _imageAngle.SetActive(false);
        _imagePower.SetActive(true);

        _text.text = EXAMPLE_PERCENT;
        _text.color = _colorTutorial;
    }

    public void desactiveAll()
    {
        _speechButton.setButton(false);
        _imageAngle.SetActive(false);
        _imagePower.SetActive(false);
    }
}
