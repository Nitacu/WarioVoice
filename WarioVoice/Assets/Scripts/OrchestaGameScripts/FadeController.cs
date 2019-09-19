using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    private const string FADESCREEN = "FadeScreen";
    private const string UNFADESCREEN = "UnFadeScreen";
    private const string IDLE = "IdleFade";
    public GameObject backToMenu;
    public GameObject lostButton;
    public GameObject speech;
    public GameObject musicNotes;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void permanentFade()
    {
        FindObjectOfType<BotInput>().gameObject.SetActive(false);
        Invoke("finalFade", 2);
        Invoke("backToMenuButton", 3);
    }

    private void finalFade()
    {
        _anim.Play(Animator.StringToHash(FADESCREEN), -1, 0f);
    }

    public void backToMenuButton()
    {
        backToMenu.SetActive(true);
    }

    public void lostLevel()
    {
        lostButton.SetActive(true);
    }

    public void playFade()
    {
        _anim.Play(Animator.StringToHash(FADESCREEN), -1, 0f);
        Invoke("unFade", 3);
    }

    private void unFade()
    {
        _anim.Play(Animator.StringToHash(UNFADESCREEN), -1, 0f);
        Invoke("idleFade", 1);
    }

    private void idleFade()
    {
        _anim.Play(Animator.StringToHash(IDLE), -1, 0f);
        speech.SetActive(true);
        musicNotes.SetActive(true);
    }
}
