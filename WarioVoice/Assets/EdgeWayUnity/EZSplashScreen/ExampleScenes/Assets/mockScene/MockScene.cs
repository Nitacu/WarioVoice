using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockScene : MonoBehaviour
{
    public CanvasGroup cgAlpha;
    bool fadeIn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            cgAlpha.alpha += Time.deltaTime;
            if (cgAlpha.alpha>=1)
            {
                fadeIn = false;
            }
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
    }
}
