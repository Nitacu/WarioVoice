using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroProperties : MonoBehaviour
{
    [SerializeField]private List<VoiceAttacks> _attacks = new List<VoiceAttacks>(); // mis ataques
    private ExeAttack _exeAttack; // donde va a preparar el ataque
    private LevelInformationPanel _levelInformationPanel; // panel que muestra la informacion del nivel
    private LamiaController _lamia; //enemigo

    private float _life = 0;
    private float _mana = 0;
    private bool _isLive = true;

    private GameObject _PanelData; // por si al final queda el panel y el personajes como 2 GO diferentes
    private Sprite _icon; // icono del rostro

    public bool IsLive { get => _isLive; set => _isLive = value; }

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
        _life -= damage;
        _PanelData.GetComponent<CharacterStatistics>().changeColor(Color.red);
        _PanelData.GetComponent<CharacterStatistics>().reloadStatistics(_life);

        if (_life <= 0)
        {
            IsLive = false;
            GetComponent<SpriteRenderer>().color = Color.grey;
            FindObjectOfType<ControlShifts>().dieCharacter();
            _PanelData.GetComponent<CharacterStatistics>().reloadStatistics(0);
        }


    }

    public void getCharacterStastic(float life)
    {
        _life += life;
        _PanelData.GetComponent<CharacterStatistics>().reloadStatistics(_life);
    }

    public void getAttack(VoiceAttacks attack)
    {
        _attacks.Add(attack);
    }

    //activa el panel de ataques y muestra los ataques de este personaje
    public void showAttacks()
    {
        _exeAttack.prepareAttack(_attacks, GetComponent<HeroProperties>());
        _lamia.Characters = gameObject.GetComponent<HeroProperties>();

        for (int i = 0; i < _attacks.Count; i++)
        {
            _levelInformationPanel.Attacks[i].GetComponentInChildren<TMP_Text>().text = _attacks[i]._verb;
            _levelInformationPanel.Attacks[i].GetComponent<AudioSource>().clip = _attacks[i]._pronunciation;
        }

        int random = Random.Range(0,_attacks.Count);

        _exeAttack.CorrectAttack = _attacks[random]._attack;
       _levelInformationPanel.activePanelAttacks(_attacks[random]._riddle);

        //mueve al personaje
        GetComponent<MoveHeroe>().changeDirection();
    }

}
