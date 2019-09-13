using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour
{

    private void OnDestroy()
    {
        if (FindObjectOfType<Ammunition>().Amnunition == 0)
            FindObjectOfType<ConfigurationWorms>().lostGame();
    }

}
