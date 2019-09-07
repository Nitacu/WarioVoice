using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LamiaController : MonoBehaviour
{
    [SerializeField]private float life;

    private HeroProperties[] _characters;

    public void lostLife(float damage)
    {
        Life -= damage;
        // animacion batalla
        GetComponent<Animator>().Play(Animator.StringToHash("Damage Lamia"));

        if (Life <= 0)
        {
            AttackGlossary.GetInstance()._difficultyLevel++;
            SceneManager.LoadScene("RPg_adventure");
            Debug.Log("ganaste wey");
        }
    }

    public void findCharacters()
    {
        _characters = FindObjectsOfType<HeroProperties>();
    }

    public void attack()
    {
        Debug.Log("ataque");
        float numberRandom = Random.Range(1, 5);

        switch (numberRandom)
        {
            case 1:
                weakAttack();
                break;

            case 2:
                normalAttack();
                break;

            case 3:
                normalAttack();
                break;

            case 4:
                strongAttack();
                break;
        }

        FindObjectOfType<ControlShifts>().playerTurn();
    }

    public bool weakAttack()
    {
        while (true)
        {
            int numberRandom = Random.Range(0, _characters.Length - 2);

            if (_characters[numberRandom].IsLive)
            {
                _characters[numberRandom].getDamage(70);
                return true;
            }

        }
    }

    public bool normalAttack()
    {
        while (true)
        {
            int numberRandom = Random.Range(0, _characters.Length - 2);

            if (_characters[numberRandom].IsLive)
            {
                _characters[numberRandom].getDamage(100);
                return true;
            }

        }
    }

    public bool strongAttack()
    {
        while (true)
        {
            int numberRandom = Random.Range(0, _characters.Length - 2);

            if (_characters[numberRandom].IsLive)
            {
                _characters[numberRandom].getDamage(200);
                return true;
            }

        }
    }

    public float Life { get => life; set => life = value; }
}
