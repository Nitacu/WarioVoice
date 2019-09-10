using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenMessage : MonoBehaviour
{
    private const string PAY_ATTENTION = "PAY ATTENTION";
    private const string REPEAT = "REPEAT";
    private const string SHOWCOLOR_ANIM = "ShowColor";
    private const string PAY_ATTENTION_ANIM = "PayAttention";
    private const string IDLE = "Idle";
    private const string GOODORBAD = "checkAnswer";

    public PatronController patronControl;
    private TextMeshProUGUI textPro;
    private Animator animator;
    public GameObject lostPanel;
    public GameObject speechCanvas;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        textPro = GetComponent<TextMeshProUGUI>();
        textPro.text = PAY_ATTENTION;
        animator.Play(Animator.StringToHash(PAY_ATTENTION_ANIM));
        Invoke("startPattern", 2);
    }

    private void startPattern()
    {
        textPro.text = " ";  
        patronControl.startGame();
    }

    public void repeatPattern()
    {
        textPro.text = "NOW SAY IT!";
    }

    public void showTextColor(Crystal crystal)
    {
        textPro.text = crystal.crystalColor.ToString();
        animator.Play(Animator.StringToHash(SHOWCOLOR_ANIM));
    }

    public void turnOffText()
    {
        textPro.text = " ";
        animator.Play(Animator.StringToHash(IDLE));
    }

    public void goodOrBad(bool result)
    {
        if (result)
        {
            textPro.text = "GOOD JOB!";
            animator.Play(Animator.StringToHash(GOODORBAD),-1,0f);
            Invoke("turnOffText", 1);
        }
        else
        {
            textPro.text = "NICE TRY";
            textPro.color = Color.red;
            lostPanel.SetActive(true);
            speechCanvas.SetActive(false);
        }

    }

    public void winScreen()
    {
        textPro.text = "YOU WON!";
        textPro.color = Color.green;
        lostPanel.SetActive(true);
        speechCanvas.SetActive(false);
    }
  


}
