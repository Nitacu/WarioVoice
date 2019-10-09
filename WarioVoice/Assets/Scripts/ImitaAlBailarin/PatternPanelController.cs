using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternPanelController : MonoBehaviour
{
    public GameObject panelObject;
    public GameObject partitura;
    private GameObject newInstrument;
    
    private List<GameObject> musicNotes = new List<GameObject>();

    public void patternCreator(Crystal[] crystals)
    {
        int cont = 0;
        partitura.SetActive(true);
        foreach (Crystal crystal in crystals)
        {
            Instantiate(panelObject, gameObject.transform);
        }

        foreach (Transform crystal in gameObject.transform)
        { 
            crystal.gameObject.GetComponent<CrystalController>().changeCrystalColor(crystals[cont]);
            cont++;
        }


    }

    

    public void musicPatternCreator(Instrument[] insturments)
    {
        foreach (Instrument instrument in insturments)
        {
            newInstrument = Instantiate(panelObject, gameObject.transform);
            newInstrument.GetComponent<MusicalNoteController>().instrumentSprite = instrument.sprite;
            musicNotes.Add(newInstrument);
        }
    }

    public void turnOnNote(int index)
    {
        musicNotes[index].GetComponent<MusicalNoteController>().isOn = true;
    }
}
