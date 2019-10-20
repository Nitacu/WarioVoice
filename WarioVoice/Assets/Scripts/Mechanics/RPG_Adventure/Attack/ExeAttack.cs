using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExeAttack : MonoBehaviour
{
    private List<VoiceAttacks> _riddle;
    private AttackGlossary.attack _typeAttack; //lo masnda el comanparse 
    private LamiaController _lamia;
    private List<VoiceAttacks> _listAttacks = new List<VoiceAttacks>();
    private HeroProperties _hero;
    private VoiceAttacks _currentAttack;
    private ControlShifts _controlShifts;
    [SerializeField] private GameObject _hearth; //corazon de gano vida
    [SerializeField] private GameObject _usedObject; //muestra que objeto uso

    private void Start()
    {
        _controlShifts = FindObjectOfType<ControlShifts>();
    }

    public void prepareAttack(List<VoiceAttacks> listAttacks, HeroProperties hero)
    {
        _listAttacks = listAttacks;
        _hero = hero;
    }

    //revisa si el heroe tiene el ataque 
    public bool characterContainsAttack()
    {
        foreach (VoiceAttacks attacks in _listAttacks)
        {
            if (attacks._attack == _typeAttack)
            {
                _currentAttack = attacks;
                return true;
            }
        }

        return false;
    }

    public void feeckbackGotLife()
    {
        HeroProperties[] aux = FindObjectsOfType<HeroProperties>();

        foreach (HeroProperties hero in aux)
        {
            Instantiate(_hearth, hero.transform).transform.position = hero.transform.position + new Vector3(0,1,0);
        }
    }

    IEnumerator createUsedObjectCoroutine()
    {
        yield return new WaitForEndOfFrame();

        GameObject usedObject = Instantiate(_usedObject);
        usedObject.GetComponent<SpriteRenderer>().sprite = _currentAttack._sprite;
        usedObject.GetComponent<AudioSource>().clip = _currentAttack._soundEffect;
        usedObject.GetComponent<AudioSource>().Play();
    }

    public void selectAttack(string word)
    {
        SaveSystem.increaseMicrophonePressedTime(true,word,ChangeScene.EspikinglishMinigames.RPG);

        if (characterContainsAttack())
        {
            // crea el objeto que muestra cual es que uso
            /*
            GameObject usedObject = Instantiate(_usedObject);
            usedObject.GetComponent<SpriteRenderer>().sprite = _currentAttack._sprite;
            usedObject.GetComponent<AudioSource>().clip = _currentAttack._soundEffect;
            usedObject.GetComponent<AudioSource>().Play();
            */
            StartCoroutine(createUsedObjectCoroutine());

            if (_currentAttack._cure)
            {
                HeroProperties[] aux =  FindObjectsOfType<HeroProperties>();
                List<HeroProperties> revive = new List<HeroProperties>();

                foreach (HeroProperties hero in aux)
                {
                    if (!hero.IsLive)
                        Lamia.addCharacter(hero);

                    hero.getCharacterStastic(1);  
                }

                //muestra que gano vida
                Invoke("feeckbackGotLife", 1);
                //para finalizar el turno
                _controlShifts.Invoke("playerEnemy", 4);
                //frase del ataque
                FindObjectOfType<LevelInformationPanel>().showDialogs(_currentAttack._sentenceToCompleteAttack, false);
            }
            else
            {
                
                if (Lamia.effectiveAttack(_typeAttack))
                {
                    
                    //aplcia el daño
                    if (Lamia.lostLife(_currentAttack._damage))
                        _controlShifts.Invoke("playerEnemy", 3);
                    // visual

                    //frase del ataque
                    FindObjectOfType<LevelInformationPanel>().showDialogs(_currentAttack._sentenceToCompleteAttack,false);
                }
                else
                {
                    _controlShifts.Invoke("playerEnemy", 3);
                    FindObjectOfType<LevelInformationPanel>().showDialogs(_currentAttack._sentencesToNotUseAttack,false);
                }
            }

            _hero.GetComponent<MoveHeroe>().Invoke("changeDirection", 1);
            _hero.GetComponent<HeroProperties>().Attacks.Remove(_currentAttack);
            _controlShifts.CurrentHero = _controlShifts.newChallenge();
        }
    }


    public AttackGlossary.attack TypeAttack { get => _typeAttack; set => _typeAttack = value; }
    public LamiaController Lamia { get => _lamia; set => _lamia = value; }
}
