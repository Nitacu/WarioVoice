using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementaryController : LamiaController
{
    [SerializeField] private List<Color> _elementaryTypeList = new List<Color>();

    public override bool lostLife(float damage)
    {
        Life -= damage;

        if (Life <= 0)
        {
            Destroy(FindObjectOfType<speechContoller>().gameObject);
            GetComponent<Animator>().enabled = false;
            GameManager.GetInstance().increaseDifficulty();
            Instantiate(_visualDamage, transform);
            Instantiate(_cofetti);
            Invoke("loadMenu", 4f);
            return false;
        }
        else
        {
            if (Life == 1)
            {
                _weak = true;
            }

            //visual de que dañaron al golem
            GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
        }

        return true;
    }

    public void changeColor()
    {
        //cambia de color
        GetComponent<SpriteRenderer>().color = _elementaryTypeList[0];
        _elementaryTypeList.RemoveAt(0);
    }

    public override void selecAttack()
    {
        Debug.Log(Characters.Count);
        int random = Random.Range(1, 101);
        GetComponent<Animator>().Play(Animator.StringToHash("Attack"));
        // fijo
        if (random <= 70)
        {
            random = Random.Range(0, Characters.Count);
            //visual del daño
            _lastHeroToHarm = heroWithMoreLife(Characters[random]);
            Instantiate(_visualDamageOthers, _lastHeroToHarm.transform).GetComponent<ParticleSystem>().startColor = GetComponent<SpriteRenderer>().color;
            _lastHeroToHarm.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
            //recibe el daño
            attack(_lastHeroToHarm, 1, "Ataque directo");
        }
        else
        {
            // en area
            foreach (HeroProperties hero in Characters)
            {
                Instantiate(_visualDamageOthers, hero.transform).GetComponent<ParticleSystem>().startColor = GetComponent<SpriteRenderer>().color; 
                hero.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
                attack(hero, 1);
            }
            FindObjectOfType<LevelInformationPanel>().showDialogs("Daño en área", false);
        }

        //revisa si mato a alguien
        removeHeroe();
    }
}
