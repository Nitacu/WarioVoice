using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterBuilder : MonoBehaviour
{
    [Header("los ataques que no sirven")]
    private List<VoiceAttacks> _listAttacksUseless = new List<VoiceAttacks>();
    [Header("Todos los ataques que si hacen daño")]
    private List<VoiceAttacks> _listAttacksUseful = new List<VoiceAttacks>();
    [Header("atque con el que destruye al enemigo")]
    private List<VoiceAttacks> _listAttacksDefinitive = new List<VoiceAttacks>();
    [Header("Todos los cuerpos de personajes")]
    [SerializeField] private List<TypeHeroeRPG> _listHeroes;
    private const int NUMBER_ATTACKS_USELESS = 1;
    private const int NUMBER_ATTACKS_USEFUL = 2;
    private const int NUMBER_ATTACKS_DEFINITIVE = 1;
    private int _numberCharacters;
    [Header("Objeto que contendra todos los personajes")]
    [SerializeField] private List<GameObject> _contentCharacters;
    [Header("prefab de personaje")]
    [SerializeField] private GameObject _character;

    private ConfigureRPG _configureRPG;
    public int NumberCharacters { get => _numberCharacters; set => _numberCharacters = value; }
    public List<VoiceAttacks> ListAttacksUseless { get => _listAttacksUseless; set => _listAttacksUseless = value; }
    public List<VoiceAttacks> ListAttacksUseful { get => _listAttacksUseful; set => _listAttacksUseful = value; }
    public List<VoiceAttacks> ListAttacksDefinitive { get => _listAttacksDefinitive; set => _listAttacksDefinitive = value; }

    public void createdCharacters()
    {
        _configureRPG = FindObjectOfType<ConfigureRPG>();
        GameObject aux;
        int numberRandom;

        //crea cada tarjeta de persona
        for (int i = 0; i < NumberCharacters; i++)
        {
            aux = Instantiate(_character, _contentCharacters[NumberCharacters - 1].GetComponentsInChildren<Transform>()[i+1].position,Quaternion.identity);
           
            // se le agrega un icono
            numberRandom = Random.Range(0, _listHeroes.Count - 1);
            aux.GetComponent<HeroProperties>().getIdentity(_listHeroes[numberRandom]._standing, _listHeroes[numberRandom]._icon);
            aux.GetComponent<HeroProperties>().getCharacterStastic(_configureRPG.configurationCharacters());
            _listHeroes.RemoveAt(numberRandom);

            //le agrega los ataques aca personajes y saca estos ataques de la lista
            //ataques inutiles
            for (int e = 0; e < NUMBER_ATTACKS_USELESS; e++)
            {
                numberRandom = Random.Range(0, ListAttacksUseless.Count - 1);
                aux.GetComponent<HeroProperties>().getAttack(ListAttacksUseless[numberRandom]);
                ListAttacksUseless.RemoveAt(numberRandom);
            }

            // ataque util
            for (int e = 0; e < NUMBER_ATTACKS_USEFUL; e++)
            {
                numberRandom = Random.Range(0, ListAttacksUseful.Count - 1);
                aux.GetComponent<HeroProperties>().getAttack(ListAttacksUseful[numberRandom]);
                ListAttacksUseful.RemoveAt(numberRandom);
            }

            //ataque definitivo
            for (int e = 0; e < NUMBER_ATTACKS_DEFINITIVE; e++)
            {
                if (ListAttacksDefinitive.Count> 0)
                {
                    numberRandom = Random.Range(0, ListAttacksDefinitive.Count - 1);
                    aux.GetComponent<HeroProperties>().getAttack(ListAttacksDefinitive[numberRandom]);
                    ListAttacksDefinitive.RemoveAt(numberRandom);
                }
            }

        }

    }
}
