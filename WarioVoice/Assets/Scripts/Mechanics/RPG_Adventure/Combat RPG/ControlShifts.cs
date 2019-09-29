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
    private int _indexTurnHero = 0;
    private HeroProperties _currentHero; // heroe que va a atacar;

    // GET Y SET
    public bool TurnEnemy { get => turnEnemy; set => turnEnemy = value; }
    public bool TurnPlayer { get => turnPlayer; set => turnPlayer = value; }
    public int NumberCharacterLive { get => numberCharacterLive; set => numberCharacterLive = value; }

    private void Start()
    {
        _lamia = FindObjectOfType<LamiaController>();
        _informationPanel = FindObjectOfType<LevelInformationPanel>();
        _informationPanel.ControlShifts = GetComponent<ControlShifts>();
        _informationPanel.activeDialogue("- Que la pelea comience");
        _heroes = FindObjectsOfType<HeroProperties>();
        Invoke("playerEnemy", 3);
    }

    public void playerTurn()
    {
        //selecciona el hero y lo mueve
        _currentHero = newChallenge();
        _currentHero.GetComponent<MoveHeroe>();
        //muestre los ataques
        _currentHero.showAttacks();

    }

    public HeroProperties newChallenge()
    {
        while (true)
        {
            if (_heroes[_indexTurnHero].IsLive)
            {
                return _heroes[_indexTurnHero];
            }
            else
            {
                _indexTurnHero++;

                if (_heroes.Length == _indexTurnHero)
                {
                    _indexTurnHero = 0;
                }  
            }
        }
    }

    public void playerEnemy()
    {
        _lamia.selecAttack(); // ataca
        _lamia.herosAlive(); // comprueba cuales quedaron vivos luego de atacar
        Invoke("playerTurn", 1.5f);
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
