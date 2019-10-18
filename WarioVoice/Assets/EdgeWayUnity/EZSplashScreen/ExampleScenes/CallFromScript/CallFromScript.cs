using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EdgeWay.Unity.EZSplashScreen;

public class CallFromScript : MonoBehaviour
{
    public GameObject mockScene;
    public EZSplashScreen ezSplashScreen;
    // Start is called before the first frame update
    void Start()
    {
        // Set callbacks
        ezSplashScreen.onFadedIn += OnFadedInSplash;
        ezSplashScreen.onComplete += OnCompletedSplash;
        // start the splash screen from script
        ezSplashScreen.StartSplashScreen();
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
