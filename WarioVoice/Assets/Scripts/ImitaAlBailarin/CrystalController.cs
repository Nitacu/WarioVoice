using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public Sprite blueCrystal;
    public Sprite greenCrystal;
    public Sprite yellowCrystal;
    public Sprite pinkCrystal;
    public Sprite brownCrystal;
    public Sprite orangeCrystal;
    public Sprite limeCrystal;
    public Sprite silverCrystal;
    public Sprite magentaCrystal;
    public Sprite whiteCrystal;
    public Sprite turquoiseCrystal;
    public Sprite blackCrystal;
    public Sprite indigoCrystal;
    public Sprite redCrystal;
    public Sprite fuchsiaCrystal;


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
    

    // Start is called before the first frame update
    void Start()
    {
        dancingPlayer = FindObjectOfType<DancerController>().gameObject;
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            spriteRender.color = Color.white;
        }
        else
        {
            spriteRender.color = Color.grey;
        }
    }

    public void changeCrystal(bool power, Colors crystal)
    {
        isOn = power;
        dancingPlayer.GetComponent<DancerController>().dancePlayer(crystal);
    }

    public void idleAnimation()
    {
        dancingPlayer.GetComponent<Animator>().Play(Animator.StringToHash("Idle"));
    }

    public void lostPattern()
    {
        isOn = false;
        dancingPlayer.GetComponent<Animator>().Play(Animator.StringToHash("Idle"));
    }

    public void changeCrystalColor(Colors color)
    {
        crystalColor = color;

        switch (color)
        {
            case Colors.BLUE:
                GetComponent<SpriteRenderer>().sprite = blueCrystal;
                break;
            case Colors.GREEN:
                GetComponent<SpriteRenderer>().sprite = greenCrystal;
                break;
            case Colors.YELLOW:
                GetComponent<SpriteRenderer>().sprite = yellowCrystal;
                break;
            case Colors.PINK:
                GetComponent<SpriteRenderer>().sprite = pinkCrystal;
                break;
            case Colors.ORANGE:
                GetComponent<SpriteRenderer>().sprite = orangeCrystal;
                break;
            case Colors.BROWN:
                GetComponent<SpriteRenderer>().sprite = brownCrystal;
                break;
            case Colors.LIME:
                GetComponent<SpriteRenderer>().sprite = limeCrystal;
                break;
            case Colors.SILVER:
                GetComponent<SpriteRenderer>().sprite = silverCrystal;
                break;
            case Colors.MAGENTA:
                GetComponent<SpriteRenderer>().sprite = magentaCrystal;
                break;
            case Colors.WHITE:
                GetComponent<SpriteRenderer>().sprite = whiteCrystal;
                break;
            case Colors.TURQUOISE:
                GetComponent<SpriteRenderer>().sprite = turquoiseCrystal;
                break;
            case Colors.BLACK:
                GetComponent<SpriteRenderer>().sprite = blackCrystal;
                break;
            case Colors.INDIGO:
                GetComponent<SpriteRenderer>().sprite = indigoCrystal;
                break;
            case Colors.RED:
                GetComponent<SpriteRenderer>().sprite = redCrystal;
                break;
            case Colors.FUCHSIA:
                GetComponent<SpriteRenderer>().sprite = fuchsiaCrystal;
                break;
        }
    }
}
