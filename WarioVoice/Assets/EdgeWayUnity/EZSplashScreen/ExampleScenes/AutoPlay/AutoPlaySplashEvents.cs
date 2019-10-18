using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlaySplashEvents : MonoBehaviour
{
    public GameObject mockScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    // Here for example, you could load a heavy scene  during the splash display
    public void OnFadedInSplash()
    {
        mockScene.SetActive(true);
    }

    // And then here, one could fade in the scene that was loaded
    public void OnCompletedSplash()
    {
        mockScene.GetComponent<MockScene>().FadeIn();
    }
}
