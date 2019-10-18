using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EdgeWay.Unity.EZSplashScreen;

public class DoubleSplash : MonoBehaviour
{
    public EZSplashScreen ezSplashScreen2;
    public GameObject mockScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Here we will start second splash screen called from EZSplashScreen1
    public void StartSecondSplash()
    {
        ezSplashScreen2.StartSplashScreen();
    }

    // == Called from EZSplashScreen2 ==

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
    // ==
}
