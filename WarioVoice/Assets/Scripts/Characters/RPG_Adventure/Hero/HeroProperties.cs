using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class HeroProperties : MonoBehaviour
{

    private List<VoiceAttacks> _attacks = new List<VoiceAttacks>(); // mis ataques
    private ExeAttack _exeAttack; // donde va a preparar el ataque
    private LevelInformationPanel _levelInformationPanel; // panel que muestra la informacion del nivel
    private LamiaController _lamia; //enemigo

    private float _life = 0;
    private float _mana = 0;
    private bool _isLive = true;

    private GameObject _PanelData; // por si al final queda el panel y el personajes como 2 GO diferentes
    private Sprite _icon; // icono del rostro

    public bool IsLive { get => _isLive; set => _isLive = value; }
    public List<VoiceAttacks> Attacks { get => _attacks; set => _attacks = value; }
    public float Life { get => _life; set => _life = value; }

    private void Start()
    {
        _exeAttack = FindObjectOfType<ExeAttack>();
        _levelInformationPanel = FindObjectOfType<LevelInformationPanel>();
        _lamia = FindObjectOfType<LamiaController>();
    }

    public void getIdentity(Sprite face, Sprite icon)
    {
        GetComponent<SpriteRenderer>().sprite = face;
        _PanelData = FindObjectOfType<StatisticsContentPanel>().activePanel();
        _icon = icon;
        _PanelData.GetComponent<CharacterStatistics>().Icon.sprite = icon;
    }

    public void getDamage(float damage)
    {
        Life -= damage;

        if (Life < 0)
            Life = 0;

        _PanelData.GetComponent<CharacterStatistics>().reloadStatistics(Life);

        if (Life <= 0)
        {
            
            IsLive = false;
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().color = Color.grey;
            FindObjectOfType<ControlShifts>().dieCharacter(GetComponent<HeroProperties>());
            _PanelData.GetComponent<CharacterStatistics>().reloadStatistics(0);
        }


    }

    public void getCharacterStastic(float life)
    {
        Life += life;
        _PanelData.GetComponent<CharacterStatistics>().reloadStatistics(Life);

        if (!IsLive && Life>0)
        {
            reviveHeroe();
        }
    }

    public void reviveHeroe()
    {
        IsLive = true;
        GetComponent<Animator>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;
        FindObjectOfType<ControlShifts>().reviveHero(GetComponent<HeroProperties>());        
    }

    public void getAttack(VoiceAttacks attack)
    {
        Attacks.Add(attack);
       
    }

    //activa el panel de ataques y muestra los ataques de este personaje
    public void showAttacks()
    {
        _exeAttack.prepareAttack(Attacks, GetComponent<HeroProperties>());

        for (int i = 0; i < _levelInformationPanel.Attacks.Count; i++)
        {
            if (i < Attacks.Count)
            {
                _levelInformationPanel.Attacks[i].SetActive(true);
                // palabra 
                _levelInformationPanel.Attacks[i].GetComponentInChildren<TMP_Text>().text = Attacks[i]._verb;
                _levelInformationPanel.Attacks[i].GetComponent<AudioSource>().clip = Attacks[i]._pronunciation;
                //imagen de la palabra
                _levelInformationPanel.Images[i].GetComponent<Image>().sprite = Attacks[i]._sprite;
            }
            else
            {
                _levelInformationPanel.Attacks[i].SetActive(false);
            }

        }

        
       _levelInformationPanel.showDialogs("¿Con cual objeto deberia atacar?" , true);

        //mueve al personaje
        GetComponent<MoveHeroe>().changeDirection();
    }

}
