using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentClickControl : MonoBehaviour
{
    [HideInInspector]
    public bool isClicking = false;
    private InstrumentController iController;
    private TextScreenControl text;

    // Start is called before the first frame update
    void Start()
    {
        iController = GetComponent<InstrumentController>();
        text = FindObjectOfType<TextScreenControl>();
    }

    private void OnMouseDown()
    {
        if (!isClicking)
        {
            isClicking = true;
            showInstrumentData();
        }
    }

    private void showInstrumentData()
    {
        text.showInstrument(iController._instrument.ToString());
        Invoke("clearBoolean", 1.5f);
    }

    private void clearBoolean()
    {
        isClicking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
