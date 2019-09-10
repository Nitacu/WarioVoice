using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickCrystalController : MonoBehaviour
{
    private AudioSource audioSource;
    private CrystalController crystalController;
    private GameObject screenMessage;
    private PatronController patronController;
    private bool isClicking = false;

    // Start is called before the first frame update
    void Start()
    {
        crystalController = GetComponent<CrystalController>();
        screenMessage = FindObjectOfType<ScreenMessage>().gameObject;
        patronController = FindObjectOfType<PatronController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (!isClicking)
        {
            isClicking = true;
            showCrystalData();           
        }
    }

    public void showCrystalData()
    {
        if (!patronController.getShowingPattern())
        {
            foreach (Transform crystal in patronController.getActiveCrystals().transform)
            {
                crystal.GetComponent<CrystalController>().isOn = false;
            }

            //audioSource.clip = crystalController.clip;
            //audioSource.Play();

            crystalController.isOn = true;
            screenMessage.GetComponent<TextMeshProUGUI>().text = crystalController.crystalColor.ToString();

            Invoke("turnOff", 2);
        }
    }

    public void turnOff()
    {
        isClicking = false;
        foreach (Transform crystal in patronController.getActiveCrystals().transform)
        {
            crystal.GetComponent<CrystalController>().isOn = false;
            screenMessage.GetComponent<TextMeshProUGUI>().text = " ";
        }
    }
}
