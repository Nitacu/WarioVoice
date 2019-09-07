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

    public void changeCrystal(bool power, Crystal crystal)
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

    public void changeCrystalColor(Crystal crystal)
    {
        GetComponent<SpriteRenderer>().sprite = crystal.crystalSprite;
        crystalColor = crystal.crystalColor;
    }
}
