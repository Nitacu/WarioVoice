using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SimulationCheck : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private BuildingsManager _buildManager;
    [SerializeField] private TextMeshProUGUI _text;

    public void OnPointerDown(PointerEventData eventData)
    {
        _buildManager.parseCommandSimulation(_text.text);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
