using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FinalBoss : LamiaController
{
    private int random;
    private CharacterBuilder _builder;
    private HeroProperties _heroApplyDamage;

    [Header("todos los ataques el boss")]
    [SerializeField] private List<GameObject> _attacks = new List<GameObject>();
    private List<RelationshipAttacksCounters> _counters = new List<RelationshipAttacksCounters>();
    private List<RelationshipAttacksCounters> _countersUsed = new List<RelationshipAttacksCounters>();
    private List<AttackGlossary.attack> _countersAux = new List<AttackGlossary.attack>();

    public override void Start()
    {
        base.Start();
        _builder = FindObjectOfType<CharacterBuilder>();

        for (int i = 0; i < _attacks.Count / _builder.SPLIT_ATTACKS1; i++)
        {
            for (int e = 0; e < _builder.SPLIT_ATTACKS1; e++)
            {
                _countersAux.Add(_listAttacksUseful[(i * _builder.SPLIT_ATTACKS1) + e]._attack);
            }

            _counters.Add(new RelationshipAttacksCounters(_attacks[i], _countersAux.ToArray()));
            _countersAux.Clear();
        }
    }

    public override void addCharacter(HeroProperties hero)
    {
        base.addCharacter(hero);

        hero.AssociatedObject = false;

        for (int i = 0; i < _countersUsed.Count; i++)
        {
            if (_countersUsed[i].attackUseful(hero.CounterAttack))
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
        foreach (RelationshipAttacksCounters attacksCounters in _countersUsed)
        {
            if (attacksCounters.attackUseful(attackPlayer))
            {
                foreach (AttackGlossary.attack attack in attacksCounters._attackPlayer)
                { 
                    if (attack == attackPlayer)
                    {
                        //daño al jefe
                        Debug.Log("le pega al boss");
                        Destroy(_shifts.CurrentHero.GetComponentInChildren<FollowPoint>().gameObject);
                        return true;
                    }
                }
            }
        }

        _heroApplyDamage = _shifts.CurrentHero;
        StartCoroutine(applyDamage());
        return false;

    }

    public override void winEnemy()
    {
        Destroy(FindObjectOfType<speechContoller>().gameObject);

        //GetComponent<Animator>().Play(Animator.StringToHash("Win"));

        FindObjectOfType<ControlShifts>().Invoke("playerTurn", 1.5f);
    }

    
    IEnumerator applyDamage()
    {
        Debug.Log("llega");
        yield return new WaitForSeconds(2.5f);
        Debug.Log("crack");
        _heroApplyDamage.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
        //daño al player
        attack(_heroApplyDamage, 1, "Ataque directo");
        //revisa si mato a alguien
        removeHeroe();
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
            _lastHeroToHarm.CounterAttack = _counters[random]._attackPlayer[0];
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
    public AttackGlossary.attack[] _attackPlayer;

    public RelationshipAttacksCounters(GameObject enemy, AttackGlossary.attack[] player)
    {
        this._attackEnemy = enemy;
        this._attackPlayer = player;
    }

    public bool attackUseful(AttackGlossary.attack attack)
    {
        foreach (AttackGlossary.attack aux in _attackPlayer)
        {
            if (aux == attack)
            {
                return true;
            }
        }
        return false;
    }
}
