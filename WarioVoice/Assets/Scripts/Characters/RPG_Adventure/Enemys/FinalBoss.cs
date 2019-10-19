using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : LamiaController
{
    [Header("todos los ataques el boss")]
    [SerializeField] private List<GameObject> _attacks = new List<GameObject>();
    private List<RelationshipAttacksCounters> _counters = new List<RelationshipAttacksCounters>();

    public override void Start()
    {
        base.Start();

        for(int i = 0; i<_attacks.Count;i++)
        {
            _counters.Add(new RelationshipAttacksCounters(_attacks[i],_listAttacksUseful[i]._attack));
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
            GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
        }

        return true;
    }

    public override bool effectiveAttack(AttackGlossary.attack attack)
    {

        return false;
    }

    public override void winEnemy()
    {
        Destroy(FindObjectOfType<speechContoller>().gameObject);

        GetComponent<Animator>().Play(Animator.StringToHash("Win"));

        FindObjectOfType<ControlShifts>().Invoke("playerTurn", 1.5f);
    }

    public override void selecAttack()
    {
        int random = Random.Range(1, 101);

        GetComponent<Animator>().Play(Animator.StringToHash("Attack"));

        // fijo
        if (random <= 70)
        {
            random = Random.Range(0, Characters.Count);
            //visual del daño
            _lastHeroToHarm = heroWithMoreLife(Characters[random]);
            //COLOCAR COMO SE VE EL DAÑO
            _lastHeroToHarm.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
            //recibe el daño
            attack(_lastHeroToHarm, 1, "Ataque directo");
        }
        else
        {
            // en area
            foreach (HeroProperties hero in Characters)
            {
                //COMO SE VE EL DAÑO
                hero.GetComponent<Animator>().Play(Animator.StringToHash("Damage"));
                attack(hero, 1);
            }
            FindObjectOfType<LevelInformationPanel>().showDialogs("Daño en área", false);
        }

        //revisa si mato a alguien
        removeHeroe();
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
