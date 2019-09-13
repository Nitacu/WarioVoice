using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LamiaController : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private GameObject _visualDamage;
    private HeroProperties _characters;
    [SerializeField] private float _damage;
    [SerializeField] private GameObject _cofetti;

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
        return true;
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("WarioVoiceMenu");
    }

    public void attack(HeroProperties hero)
    {
        _characters = hero;

        //daño
        _visualDamage.SetActive(true);
        _visualDamage.transform.position = _characters.transform.position;
        _characters.getDamage(_damage);
        //siguiente accion
        _characters.GetComponent<MoveHeroe>().changeDirection();
        FindObjectOfType<ControlShifts>().playerTurn();

        FindObjectOfType<LevelInformationPanel>().activeDialogue("Te regreso el ataque");
    }

    public float Life { get => life; set => life = value; }
    public HeroProperties Characters { get => _characters; set => _characters = value; }
    public float Damage { get => _damage; set => _damage = value; }
}
