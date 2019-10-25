using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElementaryController : LamiaController
{
    [SerializeField] private List<Color> _elementaryTypeList = new List<Color>();
    [Header("cuando debe tener mas tipos de ataques")]
    [SerializeField] private List<GameObject> _listVisualDamage = new List<GameObject>();
    private CharacterBuilder _characterBuilder;

    public override void Start()
    {
        base.Start();
        _characterBuilder = FindObjectOfType<CharacterBuilder>();
    }

    public VoiceAttacks assignAttack()
    {
        return null;
    }

    //lo llama la animacion
    public void createdExplotion()
    {
        Instantiate(_visualDamageOthers, transform);
        GetComponent<SpriteRenderer>().enabled = false;
        Camera.main.GetComponent<AudioSource>().enabled = false;
        FindObjectOfType<ControlShifts>().GetComponent<AudioSource>().Play();
        Invoke("finishDie", 0.7f);
    }

    public void finishDie()
    {
        Instantiate(_cofetti);
    }

    public override bool lostLife(float damage)
    {
        Life -= damage;
        _listVisualDamage.RemoveAt(0);

        for (int i = 0; i < _characterBuilder.SPLIT_ATTACKS1; i++)
        {
            _listAttacksUseful.RemoveAt(0);
        }

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
            GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
        }

        return true;
    }

    public override bool effectiveAttack(AttackGlossary.attack attack, RelationshipAttacksCounters counter = null)
    {
        for(int i = 0;i < _characterBuilder.SPLIT_ATTACKS1 ;i++)
        {
            if (attack == _listAttacksUseful[i]._attack)
            {
                return true;
            }
        }

        return false;
    }

    public override void winEnemy()
    {
        Destroy(FindObjectOfType<speechContoller>().gameObject);
        List<HeroProperties> heros = FindObjectsOfType<HeroProperties>().ToList();
        GetComponent<Animator>().Play(Animator.StringToHash("Win"));

        foreach (HeroProperties aux in heros)
        {
            Instantiate(_listVisualDamage[0], aux.transform.position, Quaternion.Euler(_listVisualDamage[0].transform.localEulerAngles.x, 0, 0));
        }
        FindObjectOfType<ControlShifts>().Invoke("playerTurn", 1.5f);
    }

    public void changeColor()
    {
        //cambia de color
        GetComponent<SpriteRenderer>().color = _elementaryTypeList[0];
        _elementaryTypeList.RemoveAt(0);
    }

    public override void selecAttack()
    {
        int random = Random.Range(1, 3);

        GetComponent<Animator>().Play(Animator.StringToHash("Attack"));

        // fijo
        switch (random)
        {
            case 1:
                random = Random.Range(0, Characters.Count);
                //visual del daño
                _lastHeroToHarm = heroWithMoreLife(Characters[random]);
                Instantiate(_listVisualDamage[0], _lastHeroToHarm.transform.position, Quaternion.Euler(_listVisualDamage[0].transform.localEulerAngles.x, 0, 0));
                _lastHeroToHarm.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
                //recibe el daño
                attack(_lastHeroToHarm, 1, "Ataque directo");
                break;

            case 2:
                // en area
                foreach (HeroProperties hero in Characters)
                {
                    Instantiate(_listVisualDamage[0], hero.transform.position, Quaternion.Euler(_listVisualDamage[0].transform.localEulerAngles.x, 0, 0));
                    hero.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
                    attack(hero, 1);
                }
                FindObjectOfType<LevelInformationPanel>().showDialogs("Daño en área", false);
                break;
        }

        /*
        if (random <= 70)
        {
            random = Random.Range(0, Characters.Count);
            //visual del daño
            _lastHeroToHarm = heroWithMoreLife(Characters[random]);
            Instantiate(_listVisualDamage[0], _lastHeroToHarm.transform.position,Quaternion.Euler(_listVisualDamage[0].transform.localEulerAngles.x,0,0));
            _lastHeroToHarm.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
            //recibe el daño
            attack(_lastHeroToHarm, 1, "Ataque directo");
        }
        else
        {
            // en area
            foreach (HeroProperties hero in Characters)
            {
                Instantiate(_listVisualDamage[0], hero.transform.position, Quaternion.Euler(_listVisualDamage[0].transform.localEulerAngles.x, 0, 0));
                hero.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
                attack(hero, 1);
            }
            FindObjectOfType<LevelInformationPanel>().showDialogs("Daño en área", false);
        }
        */
        //siguiente personaje en atacar
        _shifts.CurrentHero = _shifts.newChallenge();
        //revisa si mato a alguien
        removeHeroe();
    }
}
