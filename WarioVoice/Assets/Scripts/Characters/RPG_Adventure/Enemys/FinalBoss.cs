using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FinalBoss : LamiaController
{
    private GameObject _lastHeroeDie;
    private int random;
    private CharacterBuilder _builder;
    private HeroProperties _heroApplyDamage;
    private GameObject _reflectedAttack;
    [Header("objecto que lanza")]
    [SerializeField] private GameObject _jar;
    [SerializeField] private Sprite _bomb;
    [Header("LLuvia de jarras")]
    [SerializeField] private GameObject _rainJar;
    [Header("todos los ataques el boss")]
    [SerializeField] private List<GameObject> _attacks = new List<GameObject>();
    private List<RelationshipAttacksCounters> _counters = new List<RelationshipAttacksCounters>();
    private List<RelationshipAttacksCounters> _countersUsed = new List<RelationshipAttacksCounters>();
    private List<AttackGlossary.attack> _countersAux = new List<AttackGlossary.attack>();

    public List<RelationshipAttacksCounters> Counters { get => _counters; set => _counters = value; }

    public override void Start()
    {
        base.Start();
        _builder = FindObjectOfType<CharacterBuilder>();

        for (int i = 0; i < _attacks.Count; i++)
        {
            for (int e = 0; e < _builder.SPLIT_ATTACKS1; e++)
            {
                _countersAux.Add(_listAttacksUseful[(i * _builder.SPLIT_ATTACKS1) + e]._attack);
            }

            Counters.Add(new RelationshipAttacksCounters(_attacks[i], _countersAux.ToArray()));
            _countersAux.Clear();
        }
        Counters[Counters.Count - 1]._useOtherObj = true;
    }

    public override void addCharacter(HeroProperties hero)
    {
        base.addCharacter(hero);

        hero.AssociatedObject = false;
    }

    public override bool lostLife(float damage)
    {
        Life -= damage;

        //visual de que dañaron al jefe
        _reflectedAttack.GetComponent<MoveAttack>()._currentEndPosition =
            new Vector3(transform.position.x, _reflectedAttack.transform.position.y, 0);

        _reflectedAttack.GetComponent<MoveAttack>()._speed = 10;
        _reflectedAttack.GetComponent<MoveAttack>()._time = 0;
        StartCoroutine(visualDamage());


        if (Life <= 0)
        {
            Destroy(FindObjectOfType<speechContoller>().gameObject);
            GameManager.GetInstance().increaseDifficulty();
            Invoke("loadMenu", 6f);
            return false;
        }

        return true;
    }

    public override void winEnemy()
    {
        Destroy(FindObjectOfType<speechContoller>().gameObject);

        Instantiate(_rainJar);

        GetComponent<Animator>().Play(Animator.StringToHash("Win"));
        Invoke("loadMenu", 2);
    }

    IEnumerator visualDamage()
    {
        yield return new WaitForSeconds(1);

        if (_reflectedAttack.GetComponent<ActiveAttack>())
        {
            _reflectedAttack.GetComponent<ActiveAttack>().active();
            StartCoroutine(destroyStorm());
        }
        else
        {
            Destroy(_reflectedAttack);
        }

        GetComponent<AudioSource>().Play();
        GetComponent<Animator>().Play(Animator.StringToHash("Damage"));

        if (Life <= 0)
        {
            StartCoroutine(dieEnemy());
            StartCoroutine(finishLevel());
        }
    }

    IEnumerator dieEnemy()
    {
        yield return new WaitForSeconds(1);
        Instantiate(_visualDamage);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator finishLevel()
    {
        yield return new WaitForSeconds(2);
        Instantiate(_cofetti);
        Camera.main.GetComponent<AudioSource>().enabled = false;
        FindObjectOfType<ControlShifts>().GetComponent<AudioSource>().Play();
    }

    IEnumerator destroyStorm()
    {
        yield return new WaitForSeconds(1);
        Destroy(_reflectedAttack);
    }


    public override bool effectiveAttack(AttackGlossary.attack attackPlayer, RelationshipAttacksCounters counter)
    {

        if (counter.attackUseful(attackPlayer))
        {
            foreach (AttackGlossary.attack attack in counter._attackPlayer)
            {
                if (attack == attackPlayer)
                {
                    //daño al jefe
                    _heroApplyDamage = _shifts.CurrentHero;
                    _reflectedAttack = _heroApplyDamage.GetComponentInChildren<ParticleSystem>().gameObject;
                    _reflectedAttack.AddComponent<MoveAttack>();
                    return true;
                }
            }
        }

        _heroApplyDamage = _shifts.CurrentHero;
        StartCoroutine(applyDamage());
        return false;

    }


    IEnumerator applyDamage()
    {
        yield return new WaitForSeconds(2.5f);
        if (_heroApplyDamage.GetComponentInChildren<ActiveAttack>())
        {
            attack(_heroApplyDamage, _heroApplyDamage.GetComponentInChildren<ActiveAttack>()._damage, "Ataque directo");
            _heroApplyDamage.GetComponentInChildren<ActiveAttack>().active();
        }
        else
        {
            attack(_heroApplyDamage, 1, "Ataque directo");
        }

        //esto soluciona el bug de que si se cura y tiene la bomba es como si siguiera vivo
        if (_shifts.Heroes.Count == 1)
        {
            _shifts.CurrentHero = _shifts.newChallenge();
        }

        //daño al player
        _heroApplyDamage.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
        //revisa si mato a alguien
        removeHeroe();
    }

    public override void removeHeroe()
    {
        if (heroeIsAlive())
        {
            foreach (RelationshipAttacksCounters relationship in _countersUsed)
            {
                if (relationship._hero == heroeIsAlive())
                {

                    Counters.Add(relationship);
                    _countersUsed.Remove(relationship);
                    StartCoroutine(destroyAttackAssociation(heroeIsAlive()));
                    break;
                }
            }

            Characters.Remove(heroeIsAlive());
            removeHeroe();
        }
    }

    IEnumerator destroyAttackAssociation(HeroProperties hero)
    {
        _lastHeroeDie = hero.GetComponentInChildren<ParticleSystem>().gameObject;
        _lastHeroeDie.transform.parent = null;
        yield return new WaitForSeconds(1);
        Destroy(_lastHeroeDie);
    }

    public override void selecAttack()
    {
        //selecciona un player
        _lastHeroToHarm = _shifts.CurrentHero;
        if (_lastHeroToHarm != null)
        {
            if (!_lastHeroToHarm.AssociatedObject)
            {
                random = Random.Range(0, Counters.Count);

                GetComponent<Animator>().Play(Animator.StringToHash("Attack_final_boss"));
                GameObject aux = Instantiate(_jar, transform.position, Quaternion.identity);
                if (Counters[random]._useOtherObj)
                {
                    aux.GetComponent<SpriteRenderer>().sprite = _bomb;
                    aux.GetComponent<LaunchAttack>()._audioCrash = false;
                }
                aux.GetComponent<LaunchAttack>()._finalPosition = _lastHeroToHarm.transform;
                aux.GetComponent<LaunchAttack>().shoot();
            }
            else
            {
                _shifts.Invoke("playerTurn", 1.5f);
            }
        }
        else
        {
            Invoke("winEnemy", 1.5f);
        }
    }

    // se llama cuando la jarra se rompee
    public void activeAttack()
    {
        _shifts.Invoke("playerTurn", 1.5f);
        //le coloca el objeto
        _lastHeroToHarm.AssociatedObject = true;
        _lastHeroToHarm.CounterAttack = Counters[random]._attackPlayer[0];

        if (Counters[random]._useOtherObj)
            Instantiate(Counters[random]._attackEnemy, _lastHeroToHarm.transform).GetComponentInChildren<ActiveAttack>()._damage = 3;
        else
            Instantiate(Counters[random]._attackEnemy, _lastHeroToHarm.transform);

        //por si el heroe revive poder volver a usar ese ataque
        _lastHeroToHarm.Counters = Counters[random];
        Counters[random]._hero = _lastHeroToHarm;
        _countersUsed.Add(Counters[random]);
        Counters.RemoveAt(random);
        _lastHeroToHarm.IndexAttackAssociated = Counters.Count - 1;
        GetComponent<Animator>().Play(Animator.StringToHash("Idle_boss"));
    }
}

public class RelationshipAttacksCounters
{
    public GameObject _attackEnemy;
    public AttackGlossary.attack[] _attackPlayer;
    public HeroProperties _hero;
    public bool _useOtherObj = false;

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
