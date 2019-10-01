﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


public class LamiaController : MonoBehaviour
{
    [Header("los ataques que no sirven")]
    [SerializeField] private List<VoiceAttacks> _listAttacksUseless;
    [Header("Todos los ataques que si hacen daño")]
    [SerializeField] private List<VoiceAttacks> _listAttacksUseful;
    [Header("atque con el que destruye al enemigo")]
    [SerializeField] private List<VoiceAttacks> _listAttacksDefinitive;

    [SerializeField] private float life;
    [Header("Cuando me hacen daño")]
    [SerializeField] private GameObject _visualDamage;
    [Header("Cuando hago daño")]
    [SerializeField] private GameObject _visualDamageOthers;
    private List<HeroProperties> _characters = new List<HeroProperties>();
    [SerializeField] private List<GameObject> _listEyes = new List<GameObject>();
    [SerializeField] private GameObject _cofetti;
    private bool _weak = false; // para saber si esta debil el jefe

    private void Start()
    {
        Characters = FindObjectsOfType<HeroProperties>().ToList();
    }

    public bool lostLife(float damage)
    {
        Life -= damage;


        if (Life <= 0)
        {
            GetComponent<Animator>().enabled = false;
            GameManager.GetInstance().increaseDifficulty();
            Instantiate(_visualDamage, transform);
            Instantiate(_cofetti);
            Invoke("loadMenu", 1.5f);
            return false;
        }
        else
        {
            if(Life == 1)
                _weak = true;

            Instantiate(_visualDamage, _listEyes[0].transform);
            GetComponent<Animator>().SetBool("Damage", true);
            Invoke("destroyEye", 2);
        }

        return true;
    }

    public void destroyEye()
    {
        GetComponent<Animator>().SetBool("Damage", false);
        Destroy(_listEyes[0]);
        _listEyes.RemoveAt(0);
    }

    public void winEnemy()
    {
        List<HeroProperties> heros =  FindObjectsOfType<HeroProperties>().ToList();
        GameObject gObj;

        foreach (HeroProperties aux in heros)
        {
            gObj = Instantiate(_visualDamageOthers, aux.transform);
            Destroy(gObj.GetComponent<SelfDestroy>());
        }
    }

    public void loadMenu()
    {
        GameManager.GetInstance().finisBossBattle(true);
    }

    public void attack(HeroProperties hero, float damage,string sentenses)
    {
        //daño
        hero.getDamage(damage);
        //siguiente accion
        //FindObjectOfType<ControlShifts>().playerTurn();
        //frase que dice cuando ataca
        FindObjectOfType<LevelInformationPanel>().activeDialogue(sentenses);
    }

    // evalua que heroes estan vivos
    public void removeHeroe()
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

    public bool effectiveAttack(AttackGlossary.attack attack)
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

    public void selecAttack()
    {
        int random = Random.Range(1, 3);

        if (!_weak)
        {
            switch (random)
            {
                // fijo
                case 1:
                    random = Random.Range(0, Characters.Count);
                    //visual del daño
                    Instantiate(_visualDamageOthers, Characters[random].transform);
                    Characters[random].GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
                    //recibe el daño
                    attack(Characters[random], 1, "Ataque directo");
                    break;

                // en area
                case 2:
                    foreach (HeroProperties hero in Characters)
                    {
                        Instantiate(_visualDamageOthers, hero.transform);
                        hero.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
                        attack(hero, 1, "Ataque en area");
                    }
                    break;
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

        //revisa si mato a alguien
        removeHeroe();
    }


    public float Life { get => life; set => life = value; }
    public List<VoiceAttacks> ListAttacksUseless { get => _listAttacksUseless; set => _listAttacksUseless = value; }
    public List<VoiceAttacks> ListAttacksUseful { get => _listAttacksUseful; set => _listAttacksUseful = value; }
    public List<VoiceAttacks> ListAttacksDefinitive { get => _listAttacksDefinitive; set => _listAttacksDefinitive = value; }
    public List<HeroProperties> Characters { get => _characters; set => _characters = value; }
}
