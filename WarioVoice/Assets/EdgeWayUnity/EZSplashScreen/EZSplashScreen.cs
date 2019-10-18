using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace EdgeWay.Unity.EZSplashScreen
{

    public class EZSplashScreen : MonoBehaviour
    {

        public Action onFadedIn;
        public Action onComplete;


        [Header("Drag your splash image here")]
        public Sprite splashImage = null;
        public enum AspectRatio
        {
            StretchToFill,
            Center
        }
        public AspectRatio aspectRatio = AspectRatio.Center;
        public Color backgroundColor;


        [Header("Display values (seconds)")]
        public float fadeInTime = 1;
        public float displayTime = 2;
        public float fadeOutTime = 1;

        [Header("Auto play splash screen")]
        public bool autoPlay = true;

        [Header("Destroy splash screen object after completion")]
        public bool destroyAfterCompletion = true;

    
        [Header("Called after splash has faded in before display time")]
        public UnityEngine.Events.UnityEvent OnFadedIn;
        [Header("Called after splash has completed")]
        public UnityEngine.Events.UnityEvent OnComplete;

      



        public enum SplashStatus
        {
            init,
            fadeinngin,
            displayed,
            fadeingout,
            completed


        }
        [HideInInspector]
        public SplashStatus splashStatus = SplashStatus.init;

        private Image _imageCover;
        private Image _imageBackgroundColor;
        private Image _imageSprite;
        private CanvasGroup _cgAlphaCover, _cgAlphaPanelSplash;
        private float fadeInCTR;
        private float displayCTR;
        private float fadeOutCTR;

        private void Awake()
        {

            DontDestroyOnLoad(this.gameObject);

            // get canvas
            Canvas canvas = GetComponentInChildren<Canvas>();
            canvas.sortingOrder = 32767; // set max sort order to make sure in front

            // get cover and set to background color
            Transform cover = GetComponentInChildren<Canvas>().gameObject.transform.GetChild(0);
            _imageCover = cover.gameObject.GetComponent<Image>();
            _imageCover.color = backgroundColor;

            // get cover canvas group
            _cgAlphaCover = cover.gameObject.GetComponent<CanvasGroup>();
         

            // get panel image
            Transform panelSplash = GetComponentInChildren<Canvas>().gameObject.transform.GetChild(1);

            // get panel splash canvas group
            _cgAlphaPanelSplash = panelSplash.gameObject.GetComponent<CanvasGroup>();

            // get background color image
            _imageBackgroundColor = panelSplash.GetChild(0).gameObject.GetComponent<Image>();
            _imageBackgroundColor.color = backgroundColor;


            // get splash sprite 
            GameObject g = panelSplash.GetChild(1).gameObject;

            /*
            if (aspectRatio == AspectRatio.Center) 
            {
                if (splashImage != null)
                {
                    if (Screen.height > Screen.width)
                    {
                        float r = (float)splashImage.texture.width / (float)splashImage.texture.height;
                        g.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, ((float)Screen.width / 2) / r);
                    }
                    else
                    {
                        float r = (float)splashImage.texture.width / (float)splashImage.texture.height;
                        g.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.height / 2) * r, Screen.height / 2);
                    }
                }
            }
            else 
            {
                g.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
            }*/
            _imageSprite = g.GetComponent<Image>();

            _imageSprite.sprite = splashImage;


            fadeInCTR = fadeInTime;
            displayCTR = displayTime;
            fadeOutCTR = fadeOutTime;



        }

        // Start is called before the first frame update
        void Start()
        {
            // if auto play make cover visible and fadein right away
            if (autoPlay)
            {
                _cgAlphaCover.alpha = 1;
                splashStatus = SplashStatus.fadeinngin;
            }
            // if not auto play hide cover
            else
            {
                _cgAlphaCover.alpha = 0;
            }

  
        }

        // Update is called once per frame
        void Update()
        {
            // fading in
            if (splashStatus == SplashStatus.fadeinngin)
            {
                fadeInCTR += -Time.deltaTime;
                _cgAlphaPanelSplash.alpha += Time.deltaTime / fadeInTime;
                if (fadeInCTR < 0)
                {
                    _cgAlphaPanelSplash.alpha = 1;
                    splashStatus = SplashStatus.displayed;

                    if (OnFadedIn != null)
                    {
                        OnFadedIn.Invoke();
                    }

                    if (onFadedIn != null)
                    {
                        onFadedIn();
                    }

                }

            }

            // displayed
            if (splashStatus == SplashStatus.displayed)
            {
                displayCTR += -Time.deltaTime;
                if (displayCTR < 0)
                {
                    splashStatus = SplashStatus.fadeingout;
                }
            }

            // fading out
            if (splashStatus == SplashStatus.fadeingout)
            {
                fadeOutCTR -= Time.deltaTime;
                _cgAlphaPanelSplash.alpha -= Time.deltaTime / fadeInTime;
                _cgAlphaCover.alpha -= Time.deltaTime / fadeInTime;
                if (fadeOutCTR < 0)
                {
                    splashStatus = SplashStatus.completed;
                    _cgAlphaPanelSplash.alpha = 0;
                    if (OnComplete != null)
                    {
                        OnComplete.Invoke();
                    }

                    if (onComplete != null)
                    {
                        onComplete();
                    }

                    if (destroyAfterCompletion)
                    {
                          Destroy(gameObject, 0.1f);
                    }
                    
                }
            }


        }

        // start splash screen
        public void StartSplashScreen()
        {
            _cgAlphaCover.alpha = 1;
            fadeInCTR = fadeInTime;
            displayCTR = displayTime;
            fadeOutCTR = fadeOutTime;
            splashStatus = SplashStatus.fadeinngin;

        }
    }
}
