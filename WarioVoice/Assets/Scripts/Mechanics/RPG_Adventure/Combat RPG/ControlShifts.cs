using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class ControlShifts : MonoBehaviour
{
    private bool turnEnemy = true;
    private bool turnPlayer = false;
    private LevelInformationPanel _informationPanel;
    private int numberCharacterLive = 0;
    private LamiaController _lamia;
    private List<HeroProperties> _heroes = new List<HeroProperties>();
    private int _indexTurnHero = -1;
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
        _heroes = FindObjectsOfType<HeroProperties>().ToList();
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
            _indexTurnHero++;
            
            if (_heroes.Count <= _indexTurnHero)
            {
                
                _indexTurnHero = 0;
            }
            
            if (_heroes[_indexTurnHero].IsLive)
            {
                return _heroes[_indexTurnHero];
            }

        }
    }

    public void playerEnemy()
    {
        _lamia.selecAttack(); // ataca
        Invoke("playerTurn", 1.5f);
    }

    public void dieCharacter(HeroProperties hero)
    {
        numberCharacterLive++;
        _heroes.Remove(hero);

        if (numberCharacterLive >= FindObjectOfType<CharacterBuilder>().NumberCharacters)
        {
            GameManager.GetInstance().launchNextMinigame(false);
        }
    }
}
