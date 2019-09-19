using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporalWritingItem : MonoBehaviour
{
    public InputField input;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getInstrument()
    {
        FindObjectOfType<PatternCheckOrchesta>().parseCommand(input.text);
    }

    public void getWord()
    {
        FindObjectOfType<CheckAnswer>().parseCommand(input.text);
    }
}
