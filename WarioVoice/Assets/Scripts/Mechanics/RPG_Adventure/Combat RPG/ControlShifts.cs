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
    public List<HeroProperties> Heroes { get => _heroes; set => _heroes = value; }

    private void Start()
    {
        _lamia = FindObjectOfType<LamiaController>();
        _informationPanel = FindObjectOfType<LevelInformationPanel>();
        _informationPanel.ControlShifts = GetComponent<ControlShifts>();
        Heroes = FindObjectsOfType<HeroProperties>().ToList();
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
            if (Heroes.Count > 1)
            {
                while (true)
                {
                    _indexTurnHero++;

                    if (Heroes.Count <= _indexTurnHero)
                    {
                        _indexTurnHero = 0;
                    }

                    if (Heroes[_indexTurnHero].IsLive && Heroes.Count > 1)
                    {
                        return Heroes[_indexTurnHero];
                    }
                }
            }
            else
            {
                _indexTurnHero = 0;
                return Heroes[0];
            }
        }
        else
        {
            if (Heroes.Count > 0)
            {
                while (true)
                {
                    _indexTurnHero++;

                    if (Heroes.Count <= _indexTurnHero)
                    {
                        _indexTurnHero = 0;
                    }

                    if (Heroes[_indexTurnHero].IsLive)
                    {
                        return Heroes[_indexTurnHero];
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
            if (Heroes.Count == 0)
                _lamia.Invoke("winEnemy", 1.5f);
            else
                Invoke("playerTurn", 1.5f);
        }
        else
        {

            if (Heroes.Count == 0)
                _lamia.Invoke("winEnemy", 1.5f);     
        }

    }

    public void reviveHero(HeroProperties hero)
    {
        Heroes.Add(hero);
        numberCharacterLive--;
    }

    public void dieCharacter(HeroProperties hero)
    {
        numberCharacterLive++;
        Heroes.Remove(hero);
    }
}
