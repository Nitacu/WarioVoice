using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPlayer : MonoBehaviour
{
    private void OnDestroy()
    {
        FindObjectOfType<ConfigurationWorms>().lostGame();
    }
}
