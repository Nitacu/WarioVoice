using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroProperties : MonoBehaviour
{
    private List<VoiceAttacks> _attacks = new List<VoiceAttacks>(); // mis ataques
    private ExeAttack _exeAttack; // donde va a preparar el ataque
    private LevelInformationPanel _levelInformationPanel; // panel que muestra la informacion del nivel

    private float _life = 0;
    private float _mana = 0;
    private bool _isLive = true;

    [Header("Panel Datos")]
    [SerializeField] private GameObject _PanelData; // por si al final queda el panel y el personajes como 2 GO diferentes
    [SerializeField] private GameObject _icon; // icono del rostro

    public bool IsLive { get => _isLive; set => _isLive = value; }

    private void Start()
    {
        _exeAttack = FindObjectOfType<ExeAttack>();
        _levelInformationPanel = FindObjectOfType<LevelInformationPanel>();
    }

    public void getIdentity(Sprite face)
    {
        _icon.GetComponent<Image>().sprite = face;
    }

    public void getDamage(float damage)
    {
        _life -= damage;

        if (_life <= 0)
        {
            IsLive = false;
            FindObjectOfType<ControlShifts>().dieCharacter();
        }
        _PanelData.GetComponent<CharacterStatistics>().changeColor(Color.red);
        _PanelData.GetComponent<CharacterStatistics>().reloadStatistics(_life, _mana);
    }

    public void getCharacterStastic(float life, float mana)
    {
        _life += life;
        _mana += mana;

        _PanelData.GetComponent<CharacterStatistics>().reloadStatistics(_life, _mana);
    }

    public void getAttack(VoiceAttacks attack)
    {
        _attacks.Add(attack);
    }

    //activa el panel de ataques y muestra los ataques de este personaje
    public void showAttacks()
    {
        _exeAttack.prepareAttack(_attacks,GetComponent<HeroProperties>());

        for (int i = 0; i < _attacks.Count; i++)
        {
            _levelInformationPanel.Attacks[i].GetComponentInChildren<TMP_Text>().text = _attacks[i]._verb;
            _levelInformationPanel.Attacks[i].GetComponent<AudioSource>().clip = _attacks[i]._pronunciation;
        }

        _levelInformationPanel.activePanelAttacks();
    }

}
