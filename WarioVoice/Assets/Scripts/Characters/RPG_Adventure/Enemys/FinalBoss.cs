using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : LamiaController
{
    private ControlShifts _shifts;

    private int random;

    [Header("todos los ataques el boss")]
    [SerializeField] private List<GameObject> _attacks = new List<GameObject>();
    private List<RelationshipAttacksCounters> _counters = new List<RelationshipAttacksCounters>();
    private List<RelationshipAttacksCounters> _countersUsed = new List<RelationshipAttacksCounters>();

    public override void Start()
    {
        base.Start();

        _shifts = FindObjectOfType<ControlShifts>();

        for (int i = 0; i < _attacks.Count; i++)
        {
            _counters.Add(new RelationshipAttacksCounters(_attacks[i], _listAttacksUseful[i]._attack));
        }
    }

    public override void addCharacter(HeroProperties hero)
    {
        base.addCharacter(hero);

        hero.AssociatedObject = false;

        for(int i =0; i<_countersUsed.Count;i++)
        {
            if(_countersUsed[i]._attackPlayer == hero.CounterAttack)
            {
                _counters.Add(_countersUsed[i]);
                _countersUsed.RemoveAt(i);
            }
        }
    }

    public override bool lostLife(float damage)
    {
        Life -= damage;

        if (Life <= 0)
        {
            Destroy(FindObjectOfType<speechContoller>().gameObject);
            GetComponent<Animator>().Play(Animator.StringToHash("Die"));
            GameManager.GetInstance().increaseDifficulty();
            Invoke("loadMenu", 5f);
            return false;
        }
        else
        {
            //visual de que dañaron al golem
            //GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
        }

        return true;
    }

    public override bool effectiveAttack(AttackGlossary.attack attackPlayer)
    {
        if (attackPlayer == _shifts.CurrentHero.CounterAttack)
        {
            //daño al jefe
            Debug.Log("le pega al boss");
            Destroy(_shifts.CurrentHero.GetComponentInChildren<FollowPoint>().gameObject);
            _shifts.CurrentHero.AssociatedObject = false;
            return true;
        }
        else
        {
            //daño al player
            attack(_shifts.CurrentHero, 1, "Ataque directo");
            //revisa si mato a alguien
            removeHeroe();
            Debug.Log("se pega el");
            return false;
        }
    }

    public override void winEnemy()
    {
        Destroy(FindObjectOfType<speechContoller>().gameObject);

        //GetComponent<Animator>().Play(Animator.StringToHash("Win"));

        FindObjectOfType<ControlShifts>().Invoke("playerTurn", 1.5f);
    }

    public override void selecAttack()
    {
        //selecciona un player
        _lastHeroToHarm = _shifts.CurrentHero;

        if (!_lastHeroToHarm.AssociatedObject)
        {
            //le coloca el objeto
            random = Random.Range(0, _counters.Count);
            _lastHeroToHarm.AssociatedObject = true;
            _lastHeroToHarm.CounterAttack = _counters[random]._attackPlayer;
            Instantiate(_counters[random]._attackEnemy, _lastHeroToHarm.transform).GetComponent<FollowPoint>()._position = _lastHeroToHarm.transform;
            //por si el heroe revive poder volver a usar ese ataque
            _countersUsed.Add(_counters[random]);
            _counters.RemoveAt(random);
        }
        
    }
}


public class RelationshipAttacksCounters
{
    public GameObject _attackEnemy;
    public AttackGlossary.attack _attackPlayer;

    public RelationshipAttacksCounters(GameObject enemy, AttackGlossary.attack player)
    {
        this._attackEnemy = enemy;
        this._attackPlayer = player;
    }
}
