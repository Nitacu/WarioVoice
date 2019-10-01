using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTestDeveloper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.GetInstance().DeveloperMode)
        {
            gameObject.SetActive(false);
        }
    }
    
}
