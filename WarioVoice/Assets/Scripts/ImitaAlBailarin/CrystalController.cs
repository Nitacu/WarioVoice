using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalController : MonoBehaviour
{
    private const string BLUE = "BLUE";
    private const string GREEN = "GREEN";
    private const string YELLOW = "YELLOW";
    private const string PINK = "PINK";
    private const string ORANGE = "ORANGE";
    private const string BROWN = "BROWN";
    private const string LIME = "LIME";
    private const string SILVER = "SILVER";
    private const string MAGENTA = "MAGENTA";
    private const string WHITE = "WHITE";
    private const string TURQUOISE = "TURQUOISE";
    private const string BLACK = "BLACK";
    private const string INDIGO = "INDIGO";
    private const string RED = "RED";
    private const string FUCHSIA = "FUCHSIA";

    [HideInInspector]
    public AudioClip clip;
    public bool GUICrystal = false;
 

    public enum Colors
    {
        BLUE,
        GREEN,
        YELLOW,
        PINK,
        ORANGE,
        BROWN,
        LIME,
        SILVER,
        MAGENTA,
        WHITE,
        TURQUOISE,
        BLACK,
        INDIGO,
        RED,
        FUCHSIA
    };

    
    public bool isOn = false;
    [HideInInspector]
    public Colors crystalColor;
    private SpriteRenderer spriteRender;
    private GameObject dancingPlayer;
    private Image image;
    

    // Start is called before the first frame update
    void Start()
    {
        dancingPlayer = FindObjectOfType<DancerController>().gameObject;
        spriteRender = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if (!GUICrystal)
            {
                spriteRender.color = Color.white;
            }
            else
            {
                image.color = Color.white;
            }
        }
        else
        {
            if (!GUICrystal)
            {
                spriteRender.color = new Color(0.75f, 0.75f, 0.75f, 1);
            }
            else
            {
                image.color = Color.black;
            }
        }
    }

    

    public void changeCrystal(bool power, Crystal crystal)
    {
        isOn = power;
        if (!GUICrystal)
        {
            dancingPlayer.GetComponent<DancerController>().dancePlayer(crystal);
        }
    }

    public void idleAnimation()
    {
        dancingPlayer.GetComponent<Animator>().Play(Animator.StringToHash("Idle"));
    }

    public void lostPattern()
    {
        isOn = false;
        if (!GUICrystal)
        {
            dancingPlayer.GetComponent<Animator>().Play(Animator.StringToHash("Idle"));
        }
    }

    public void changeCrystalColor(Crystal crystal)
    {
        if (!GUICrystal)
        {
            GetComponent<SpriteRenderer>().sprite = crystal.crystalSprite;
            crystalColor = crystal.crystalColor;
            clip = crystal.clip;
        }
        else
        {
            changeCrystalUI(crystal);
        }
    }

    public void changeCrystalUI(Crystal crystal)
    {
        GetComponent<Image>().sprite = crystal.crystalSprite;
        crystalColor = crystal.crystalColor;
    }
}
