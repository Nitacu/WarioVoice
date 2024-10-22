﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


public class LamiaController : MonoBehaviour
{
    [Header("los ataques que no sirven")]
    [SerializeField] protected List<VoiceAttacks> _listAttacksUseless;
    [Header("objetos que curan")]
    [SerializeField] protected List<VoiceAttacks> _listHealingObjects;
    [Header("Todos los ataques que si hacen daño")]
    [SerializeField] protected List<VoiceAttacks> _listAttacksUseful;
    [Header("atque con el que destruye al enemigo")]
    [SerializeField] protected List<VoiceAttacks> _listAttacksDefinitive;

    protected ControlShifts _shifts;
    protected float life;
    [Header("Cuando me hacen daño")]
    [SerializeField] protected GameObject _visualDamage;
    [Header("Cuando hago daño")]
    [SerializeField] protected GameObject _visualDamageOthers;
    protected List<HeroProperties> _characters = new List<HeroProperties>();
    [SerializeField] private List<GameObject> _listEyes = new List<GameObject>();
    [SerializeField] protected GameObject _cofetti;
    protected bool _weak = false; // para saber si esta debil el jefe
    protected HeroProperties _lastHeroToHarm; // la creo aca para no estar la creando muchas veces 
#pragma warning disable CS0649 // El campo 'LamiaController._eyeWeak' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Sprite _eyeWeak;
#pragma warning restore CS0649 // El campo 'LamiaController._eyeWeak' nunca se asigna y siempre tendrá el valor predeterminado null

    public virtual void Start()
    {
        Characters = FindObjectsOfType<HeroProperties>().ToList();
        _shifts = FindObjectOfType<ControlShifts>();
    }

    public virtual void addCharacter(HeroProperties hero)
    {
        Characters.Add(hero);
    }

    public virtual bool lostLife(float damage)
    {
        Life -= damage;


        if (Life <= 0)
        {
            Destroy(FindObjectOfType<speechContoller>().gameObject);
            GetComponent<Animator>().enabled = false;
            GameManager.GetInstance().increaseDifficulty();
            Instantiate(_visualDamage, transform).GetComponent<AudioSource>().enabled = false;
            Invoke("destroyEye", 2);
            Invoke("loadMenu", 5f);
            return false;
        }
        else
        {
            if (Life == 1)
            {
                _weak = true;
                GetComponent<SpriteRenderer>().sprite = _eyeWeak;
            }

            Instantiate(_visualDamage, _listEyes[0].transform);
            GetComponent<Animator>().SetBool("Damage", true);
            Invoke("destroyEye", 2);
        }

        return true;
    }

    public void destroyEye()
    {
        if (_listEyes.Count > 0)
        {
            GetComponent<Animator>().SetBool("Damage", false);
            _listEyes[0].SetActive(false);
            _listEyes.RemoveAt(0);
        }
        else
        {
            Instantiate(_cofetti);
            ControlMoney.EarnMoney(Random.Range(10, 15));
            Camera.main.GetComponent<AudioSource>().enabled = false;
            FindObjectOfType<ControlShifts>().GetComponent<AudioSource>().Play();
            GetComponentInChildren<ParticleSystem>().Stop();
            GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    public virtual void winEnemy()
    {
        Destroy(FindObjectOfType<speechContoller>().gameObject);
        List<HeroProperties> heros = FindObjectsOfType<HeroProperties>().ToList();
        GameObject gObj;
        GetComponent<Animator>().Play(Animator.StringToHash("Win"));

        foreach (HeroProperties aux in heros)
        {
            gObj = Instantiate(_visualDamageOthers, aux.transform);
            Destroy(gObj.GetComponent<SelfDestroy>());
        }
        FindObjectOfType<ControlShifts>().Invoke("playerTurn", 1.5f);
    }

    public void loadMenu()
    {
        GameManager.GetInstance().finisBossBattle(true);
    }

    public void attack(HeroProperties hero, float damage, string sentenses = null)
    {
        //daño
        hero.getDamage(damage);
        //frase que dice cuando ataca
        if (sentenses != null)
        {
            FindObjectOfType<LevelInformationPanel>().showDialogs(sentenses, false);
        }

    }

    // evalua que heroes estan vivos
    public virtual void removeHeroe()
    {
        if (heroeIsAlive())
        {
            Characters.Remove(heroeIsAlive());
            removeHeroe();
        }
    }

    public HeroProperties heroeIsAlive()
    {
        foreach (HeroProperties hero in Characters)
        {
            if (!hero.IsLive)
            {
                return hero;
            }
        }

        return null;
    }

    public virtual bool effectiveAttack(AttackGlossary.attack attack, RelationshipAttacksCounters counter = null)
    {
        if (_weak)
        {
            foreach (VoiceAttacks voiceAttacks in ListAttacksDefinitive)
            {
                if (attack == voiceAttacks._attack)
                {
                    return true;
                }
            }
        }
        else
        {
            foreach (VoiceAttacks voiceAttacks in ListAttacksUseful)
            {

                if (attack == voiceAttacks._attack)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // evitar que ataque siempre a un solo heroe
    public HeroProperties heroWithMoreLife(HeroProperties possibleHero)
    {
        if (Characters.Count > 0)
        {
            foreach (HeroProperties hero in Characters)
            {
                if (possibleHero.Life < hero.Life)
                {
                    return hero;
                }
            }
        }

        return possibleHero;
    }


    public virtual void selecAttack()
    {
        int random = Random.Range(1, 101);

        if (!_weak)
        {
            // fijo
            if (random <= 70)
            {
                random = Random.Range(0, Characters.Count);
                //visual del daño
                _lastHeroToHarm = heroWithMoreLife(Characters[random]);
                Instantiate(_visualDamageOthers, _lastHeroToHarm.transform);
                _lastHeroToHarm.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
                //recibe el daño
                attack(_lastHeroToHarm, 1, "Ataque directo");
            }
            else
            {
                // en area
                foreach (HeroProperties hero in Characters)
                {
                    Instantiate(_visualDamageOthers, hero.transform);
                    hero.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
                    attack(hero, 1);
                }
                FindObjectOfType<LevelInformationPanel>().showDialogs("Daño en área", false);
            }
        }
        else
        {
            // aca deberia ser mas mortal 
            random = Random.Range(0, Characters.Count - 1);
            //visual del daño
            Instantiate(_visualDamageOthers, Characters[random].transform);
            Instantiate(_visualDamageOthers, Characters[random].transform);
            Characters[random].GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
            //recibe el daño
            attack(Characters[random], 2, "Ataque cargado  ");
        }

        //siguiente personaje en atacar
        _shifts.CurrentHero = _shifts.newChallenge();
        //revisa si mato a alguien
        removeHeroe();
    }


    public float Life { get => life; set => life = value; }
    public List<VoiceAttacks> ListAttacksUseless { get => _listAttacksUseless; set => _listAttacksUseless = value; }
    public List<VoiceAttacks> ListAttacksUseful { get => _listAttacksUseful; set => _listAttacksUseful = value; }
    public List<VoiceAttacks> ListAttacksDefinitive { get => _listAttacksDefinitive; set => _listAttacksDefinitive = value; }
    public List<HeroProperties> Characters { get => _characters; set => _characters = value; }
    public List<VoiceAttacks> ListHealingObjects { get => _listHealingObjects; set => _listHealingObjects = value; }
}
