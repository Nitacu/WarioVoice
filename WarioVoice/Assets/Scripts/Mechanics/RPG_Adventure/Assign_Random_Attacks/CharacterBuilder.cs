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
    private List<VoiceAttacks> _listHealingObjects = new List<VoiceAttacks>();
    [Header("Todos los cuerpos de personajes")]
    [SerializeField] private List<TypeHeroeRPG> _listHeroes;
    private int NUMBER_ATTACKS_USELESS = 2;
    private int NUMBER_ATTACKS_USEFUL = 2;
    private int NUMBER_ATTACKS_DEFINITIVE = 1;
    private int NUMBER_HEALING_OBJECTS = 1;
    private int SPLIT_ATTACKS = 0;
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
    public List<VoiceAttacks> ListHealingObjects { get => _listHealingObjects; set => _listHealingObjects = value; }
    public int NUMBER_ATTACKS_USELESS1 { get => NUMBER_ATTACKS_USELESS; set => NUMBER_ATTACKS_USELESS = value; }
    public int NUMBER_ATTACKS_USEFUL1 { get => NUMBER_ATTACKS_USEFUL; set => NUMBER_ATTACKS_USEFUL = value; }
    public int NUMBER_ATTACKS_DEFINITIVE1 { get => NUMBER_ATTACKS_DEFINITIVE; set => NUMBER_ATTACKS_DEFINITIVE = value; }
    public int NUMBER_HEALING_OBJECTS1 { get => NUMBER_HEALING_OBJECTS; set => NUMBER_HEALING_OBJECTS = value; }
    public int SPLIT_ATTACKS1 { get => SPLIT_ATTACKS; set => SPLIT_ATTACKS = value; }

    public void createdCharacters()
    {
        _configureRPG = FindObjectOfType<ConfigureRPG>();
        GameObject aux;
        int numberRandom;
        //crea cada tarjeta de persona
        for (int i = 0; i < NumberCharacters; i++)
        {
            aux = Instantiate(_character, _contentCharacters[NumberCharacters - 1].GetComponentsInChildren<Transform>()[i + 1].position, Quaternion.identity);

            // se le agrega un icono
            numberRandom = Random.Range(0, _listHeroes.Count - 1);
            aux.GetComponent<HeroProperties>().getIdentity(_listHeroes[numberRandom]._standing, _listHeroes[numberRandom]._die, _listHeroes[numberRandom]._icon, _listHeroes[numberRandom]._iconDie);
            aux.GetComponent<HeroProperties>().getCharacterStastic(_configureRPG.configurationCharacters());
            _listHeroes.RemoveAt(numberRandom);

            //le agrega los ataques aca personajes y saca estos ataques de la lista
            //ataques inutiles
            for (int e = 0; e < NUMBER_ATTACKS_USELESS; e++)
            {
                numberRandom = Random.Range(0, ListAttacksUseless.Count);
                aux.GetComponent<HeroProperties>().getAttack(ListAttacksUseless[numberRandom]);
                ListAttacksUseless.RemoveAt(numberRandom);
            }

            // ataque util
            for (int e = 0; e < NUMBER_ATTACKS_USEFUL; e++)
            {
                if (SPLIT_ATTACKS == 0)
                {
                    numberRandom = Random.Range(0, ListAttacksUseful.Count);
                    aux.GetComponent<HeroProperties>().getAttack(ListAttacksUseful[numberRandom]);
                    ListAttacksUseful.RemoveAt(numberRandom);
                }
                else
                {
                    aux.GetComponent<HeroProperties>().getAttack(ListAttacksUseful[(SPLIT_ATTACKS*e)+i]);
                }
            }

            //ataque definitivo
            for (int e = 0; e < NUMBER_ATTACKS_DEFINITIVE; e++)
            {
                if (ListAttacksDefinitive.Count > 0)
                {
                    aux.GetComponent<HeroProperties>().getAttack(ListAttacksDefinitive[e]);
                    ListAttacksDefinitive.RemoveAt(e);
                }
            }

            //CURA
            
            numberRandom = Random.Range(0, ListHealingObjects.Count);
            aux.GetComponent<HeroProperties>().getAttack(ListHealingObjects[numberRandom]);
            ListHealingObjects.RemoveAt(numberRandom);
            


            aux.GetComponent<HeroProperties>().Attacks = messlist(aux.GetComponent<HeroProperties>().Attacks);
        }

    }

    public List<VoiceAttacks> messlist(List<VoiceAttacks> input)
    {
        List<VoiceAttacks> arr = input;
        List<VoiceAttacks> arrDes = new List<VoiceAttacks>();

        while (arr.Count > 0)
        {
            int val = Random.Range(0, arr.Count);
            arrDes.Add(arr[val]);
            arr.RemoveAt(val);
        }

        return arrDes;
    }
}
