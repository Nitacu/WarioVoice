using System.Collections;
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

    private float life;
    [SerializeField] private GameObject _visualDamage;
    [SerializeField]private List<HeroProperties> _characters = new List<HeroProperties>();
    [SerializeField] private GameObject _cofetti;
    private bool _weak = false; // para saber si esta debil el jefe

    private void Start()
    {
        _characters = FindObjectsOfType<HeroProperties>().ToList();
    }

    public bool lostLife(float damage)
    {
        Life -= damage;

        // animacion batalla
        //GetComponent<Animator>().Play(Animator.StringToHash("Damage Lamia"));

        if (Life <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GameManager.GetInstance().increaseDifficulty();
            Instantiate(_cofetti);
            Invoke("loadMenu", 1.5f);
            return false;
        }
        else if (Life == 1)
        {
            _weak = true;
        }
        return true;
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("WarioVoiceMenu");
    }

    public void attack(HeroProperties hero, float damage, string sentenses)
    {
        //daño
        _visualDamage.SetActive(true);
        _visualDamage.transform.position = hero.transform.position;
        hero.getDamage(damage);
        //siguiente accion
        //FindObjectOfType<ControlShifts>().playerTurn();
        //frase que dice cuando ataca
        FindObjectOfType<LevelInformationPanel>().activeDialogue(sentenses);
    }

    // evalua que heroes estan vivos
    public void herosAlive(HeroProperties hero)
    {
        _characters.Remove(hero);
    }

    public bool effectiveAttack(AttackGlossary.attack attack)
    {
        if (_weak)
        {
            foreach(VoiceAttacks voiceAttacks in ListAttacksDefinitive)
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
                Debug.Log("ataques que funcionan" + voiceAttacks._verb);

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
                    random = Random.Range(1, _characters.Count - 1);
                    attack(_characters[random], 1, "ataque directo");
                    break;

                // en area
                case 2:
                    if (_characters.Count == 1)
                    {
                        foreach (HeroProperties hero in _characters)
                        {
                            attack(hero, 1, "ataque en area");
                        }
                    }
                    attack(_characters[0], 1, "ataque directo");
                    break;
            }
        }
        else
        {
            // aca deberia ser mas mortal 
            random = Random.Range(1, _characters.Count-1);
            attack(_characters[random], 2, "ataque cargado  ");
        }
    }


    public float Life { get => life; set => life = value; }
    public List<VoiceAttacks> ListAttacksUseless { get => _listAttacksUseless; set => _listAttacksUseless = value; }
    public List<VoiceAttacks> ListAttacksUseful { get => _listAttacksUseful; set => _listAttacksUseful = value; }
    public List<VoiceAttacks> ListAttacksDefinitive { get => _listAttacksDefinitive; set => _listAttacksDefinitive = value; }
}
