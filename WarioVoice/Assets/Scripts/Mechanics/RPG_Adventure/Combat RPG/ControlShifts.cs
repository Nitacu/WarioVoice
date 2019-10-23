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
    [SerializeField]private HeroProperties _currentHero; // heroe que va a atacar;
    private bool _firtsTurn = true;
    // GET Y SET
    public bool TurnEnemy { get => turnEnemy; set => turnEnemy = value; }
    public bool TurnPlayer { get => turnPlayer; set => turnPlayer = value; }
    public int NumberCharacterLive { get => numberCharacterLive; set => numberCharacterLive = value; }
    public HeroProperties CurrentHero { get => _currentHero; set => _currentHero = value; }

    private void Start()
    {
        _lamia = FindObjectOfType<LamiaController>();
        _informationPanel = FindObjectOfType<LevelInformationPanel>();
        _informationPanel.ControlShifts = GetComponent<ControlShifts>();
        _heroes = FindObjectsOfType<HeroProperties>().ToList();
        CurrentHero = newChallenge();
    }

    public void playerTurn()
    {
        if (_firtsTurn)
        {
            _informationPanel.showDialogs("Que la pelea comience", false);
            _firtsTurn = false;
        }

        if (numberCharacterLive >= FindObjectOfType<CharacterBuilder>().NumberCharacters)
        {
            GameManager.GetInstance().finisBossBattle(false);
        }

        //selecciona el hero y lo mueve
        CurrentHero.GetComponent<MoveHeroe>().changeDirection();
        //muestre los ataques
        CurrentHero.showAttacks();

    }

    public HeroProperties newChallenge()
    {
        if (FindObjectOfType<FinalBoss>())
        {
            if (_heroes.Count > 1)
            {
                while (true)
                {
                    _indexTurnHero++;

                    if (_heroes.Count <= _indexTurnHero)
                    {
                        _indexTurnHero = 0;
                    }

                    if (_heroes[_indexTurnHero].IsLive && _heroes.Count > 1)
                    {
                        Debug.Log("cambia ?");
                        return _heroes[_indexTurnHero];
                    }
                }
            }
        }
        else
        {
            if (_heroes.Count > 0)
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
                        Debug.Log("cambia ?");
                        return _heroes[_indexTurnHero];
                    }
                }
            }
        }


        return null;
    }

    public void playerEnemy()
    {
        if (_firtsTurn)
        {
            _informationPanel.showDialogs("Que la pelea comience", false);
            _firtsTurn = false;
        }

        _lamia.selecAttack(); // ataca

        if (!FindObjectOfType<FinalBoss>())
        {
            if (_heroes.Count == 0)
                _lamia.Invoke("winEnemy", 1.5f);
            else
                Invoke("playerTurn", 1.5f);
        }
        else
        {
            
            if (_heroes.Count == 0)
                _lamia.Invoke("winEnemy", 1.5f);     
        }

    }

    public void reviveHero(HeroProperties hero)
    {
        _heroes.Add(hero);
        numberCharacterLive--;
    }

    public void dieCharacter(HeroProperties hero)
    {
        numberCharacterLive++;
        _heroes.Remove(hero);
    }
}
