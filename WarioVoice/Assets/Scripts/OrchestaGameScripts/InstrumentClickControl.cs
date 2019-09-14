using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentClickControl : MonoBehaviour
{
    [HideInInspector]
    public bool isClicking = false;
    private InstrumentController iController;
    private TextScreenControl text;
    private MasterAudioController audioC;

    // Start is called before the first frame update
    void Start()
    {
        iController = GetComponent<InstrumentController>();
        text = FindObjectOfType<TextScreenControl>();
        audioC = FindObjectOfType<MasterAudioController>();
    }

    private void OnMouseDown()
    {

        if (!audioC.isPlayingSound)
        {
            audioC.isPlayingSound = true;
            showInstrumentData();
        }
    }

    private void showInstrumentData()
    {
        text.showInstrument(iController._instrument.ToString(), iController.getNameTime());
        iController.playName();
        Invoke("clearBoolean", iController.getNameTime());
    }

    private void clearBoolean()
    {
        audioC.isPlayingSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
