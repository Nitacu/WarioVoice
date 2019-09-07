using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlShifts : MonoBehaviour
{
    private bool turnEnemy = true;
    private bool turnPlayer = false;
    private LevelInformationPanel _informationPanel;
    private int numberCharacterLive = 0;
    private LamiaController _lamia;

    public bool TurnEnemy { get => turnEnemy; set => turnEnemy = value; }
    public bool TurnPlayer { get => turnPlayer; set => turnPlayer = value; }
    public int NumberCharacterLive { get => numberCharacterLive; set => numberCharacterLive = value; }

    private void Start()
    {
        _lamia = FindObjectOfType<LamiaController>();
        _informationPanel = FindObjectOfType<LevelInformationPanel>();
        _informationPanel.ControlShifts = GetComponent<ControlShifts>();
        _informationPanel.activeDialogue("Que la pelea comience");

        Invoke("playerTurn", 2);
    }

    public void playerTurn()
    {
        turnEnemy = false;
        TurnPlayer = true;
    }

    public void playerEnemy()
    {
        turnEnemy = true;
        TurnPlayer = false;
        _informationPanel.activeDialogue("");


        //ataca el bicho
        _lamia.Invoke("attack", 3);
    }

    public void dieCharacter()
    {
        numberCharacterLive--;

        if (numberCharacterLive <= FindObjectOfType<CharacterBuilder>().NumberCharacters)
        {
            SceneManager.LoadScene("WarioVoiceMenu");
        }
    }
}
