using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoalaMoodMenuCheck : MonoBehaviour
{

    private ulong _lastMoodUpdateAction;
    private const string LAST_MOOD_UPDATE_KEY = "MoodUpdate";


    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance().initializeTiToMood();
        if (PlayerPrefs.GetString(LAST_MOOD_UPDATE_KEY) != null)
        {
            ulong.TryParse(PlayerPrefs.GetString(LAST_MOOD_UPDATE_KEY), out _lastMoodUpdateAction);
            Debug.Log(_lastMoodUpdateAction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
