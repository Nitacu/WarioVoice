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
    private HeroProperties[] _heroes = new HeroProperties[0];

    public bool TurnEnemy { get => turnEnemy; set => turnEnemy = value; }
    public bool TurnPlayer { get => turnPlayer; set => turnPlayer = value; }
    public int NumberCharacterLive { get => numberCharacterLive; set => numberCharacterLive = value; }

    private void Start()
    {
        _lamia = FindObjectOfType<LamiaController>();
        _informationPanel = FindObjectOfType<LevelInformationPanel>();
        _informationPanel.ControlShifts = GetComponent<ControlShifts>();
        _informationPanel.activeDialogue("Que la pelea comience");

        Invoke("playerTurn", 3);
    }

    public void playerTurn()
    {
        turnEnemy = false;
        TurnPlayer = true;

        if (_heroes.Length<=0)
        {
            _heroes = FindObjectsOfType<HeroProperties>();
        }

        Invoke("newChallenge", 1.5f);

    }

    public bool newChallenge()
    {
        while (true)
        {
            int numberRandom = Random.Range(0, _heroes.Length);
            if (_heroes[numberRandom].IsLive)
            {
                FindObjectOfType<SetActiveSpeechButton>().setButton(true);
                _heroes[numberRandom].showAttacks();
                return true;
            }
        }

    }

    public void playerEnemy()
    {
        turnEnemy = true;
        TurnPlayer = false;
        _informationPanel.activeDialogue("");
    }

    public void dieCharacter()
    {
        numberCharacterLive++;

        if (numberCharacterLive >= FindObjectOfType<CharacterBuilder>().NumberCharacters)
        {
            SceneManager.LoadScene("WarioVoiceMenu");
        }
    }
}
