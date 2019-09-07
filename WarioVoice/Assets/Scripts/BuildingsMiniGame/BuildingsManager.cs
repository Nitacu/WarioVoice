using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public static class BuildingVocabulary
{
    #region VOCABULARIOCHARS
    public const string POLICEMAN = "POLICEMAN";
    public const string DOCTOR = "DOCTOR";
    public const string STUDENT = "STUDENT";
    public const string CLOWN = "CLOWN";
    public const string FIREMAN = "FIREMAN";
    public const string CHEF = "CHEF";
    public const string PILOT = "PILOT";
    public const string NUN = "NUN";
    public const string SECRETARY = "SECRETARY";
    public const string FOOTBALLER = "FOOTBALLER";
    public const string ACTRESS = "ACTRESS";
    public const string MUSICIAN = "MUSICIAN";

    #endregion

    #region VOCABULARIOBUILDS
    public const string POLICESTATION = "POLICE STATION";
    public const string HOSPITAL = "HOSPITAL";
    public const string SCHOOL = "SCHOOL";
    public const string CIRCUS = "CIRCUS";
    public const string FIRESTATION = "FIRESTATION";
    public const string RESTAURANT = "RESTAURANT";
    public const string AIRPORT = "AIRPORT";
    public const string CHURCH = "CHURCH";
    public const string OFFICE = "OFFICE";
    public const string STADIUM = "STADIUM";
    public const string CINEMA = "CINEMA";
    public const string MUSICSTAGE = "MUSIC STAGE";

    #endregion

    public enum PairType
    {
        POLICE, STUDENT, DOCTOR, CLOWN, FIREMAN, CHEF, PILOT, NUN, SECRETARY, FOOTBALLER, ACTRESS, MUSICIAN
    }
}

[System.Serializable]
public class Pairs
{
    [SerializeField] private GameObject _buildPrefab;
    public GameObject BuildPrefab
    {
        get { return _buildPrefab; }
    }
    [SerializeField] private GameObject _charPrefab;
    public GameObject CharPrefab
    {
        get { return _charPrefab; }
    }
}

[System.Serializable]
public class Level
{
    [SerializeField] private int _pairsCount;
    public int NumberOfPairs
    {
        get { return _pairsCount; }
    }
    [SerializeField] private List<Pairs> _pairs = new List<Pairs>();
    public List<Pairs> Pairs
    {
        get { return _pairs; }
    }

}

public class BuildingsManager : CommandParser
{

    [SerializeField] private List<Transform> _charTransforms = new List<Transform>();
    [SerializeField] private List<Transform> _buildTransforms = new List<Transform>();

    [SerializeField] private List<Level> _levels = new List<Level>();
    private int _currentLevel = 0;
    private int _pairsMatched = 0;

    private GameObject _firstSelection;
    private GameObject _secondSelection;
    private bool _itemSelected;

    [SerializeField] private float _charTimeToGetBuild;
    [SerializeField] private TextMeshProUGUI _text;

    private BuildingVocabulary.PairType _currentType;


    private void Start()
    {
        generateLevel(_currentLevel);
    }

    public override void parseCommand(string command)
    {
        _text.text = "Comando: " + command + " , Comando Lenght: " + command.Length;

        findItemOnBuilds(command);
        findItemOnChars(command);

        if (_firstSelection != null && _secondSelection != null)
        {
            checkPair();
        }

        if (_pairsMatched >= _levels[_currentLevel].NumberOfPairs)
        {
            _currentLevel++;
            resetLevel();
        }

    }

    //simulación sin voz
    public void parseCommandSimulation(string command)
    {


        findItemOnBuilds(command);
        findItemOnChars(command);

        if (_firstSelection != null && _secondSelection != null)
        {

            checkPair();
        }

        if (_pairsMatched >= _levels[_currentLevel].NumberOfPairs)
        {
            _currentLevel++;
            resetLevel();
        }
    }


    public void resetLevel()
    {
        BuildPairItem[] _allItems = FindObjectsOfType<BuildPairItem>();
        int _itemsCount = _allItems.Length;

        for (int i = 0; i < _itemsCount; i++)
        {
            Destroy(_allItems[i].gameObject);
        }

        Debug.Log("ResetLevel");
        generateLevel(_currentLevel);
    }

    public void generateLevel(int level)
    {
        //Asignar que parejas se crearán
        int count = 0;
        List<Pairs> _scenePairs = new List<Pairs>();
        _scenePairs.Clear();


        List<int> _usedIndexes = new List<int>();
        _usedIndexes.Clear();


        while (count < _levels[level].NumberOfPairs)
        {
            int _indexRandom = Random.Range(0, _levels[level].Pairs.Count);
            if (!_usedIndexes.Contains(_indexRandom))
            {
                _scenePairs.Add(_levels[level].Pairs[_indexRandom]);
                _usedIndexes.Add(_indexRandom);
                count++;
            }
        }

    
        count = 0;//saber cuántos se han creado
        _usedIndexes = new List<int>();
        _usedIndexes.Clear();

        //create builds
        while (count < _scenePairs.Count)
        {
            int _randomBuildTransform = Random.Range(0, _buildTransforms.Count);
            if (!_usedIndexes.Contains(_randomBuildTransform))
            {
                GameObject _newBuild = Instantiate(_scenePairs[count].BuildPrefab);
                _newBuild.transform.position = _buildTransforms[_randomBuildTransform].position;
                _usedIndexes.Add(_randomBuildTransform);
                count++;
            }
        }

        count = 0;//saber cuántos se han creado
        _usedIndexes = new List<int>();
        _usedIndexes.Clear();

        //create chars
        while (count < _scenePairs.Count)
        {
            int _randomCharTransform = Random.Range(0, _charTransforms.Count);
            if (!_usedIndexes.Contains(_randomCharTransform))
            {
                GameObject _newBuild = Instantiate(_scenePairs[count].CharPrefab);
                _newBuild.transform.position = _charTransforms[_randomCharTransform].position;
                _usedIndexes.Add(_randomCharTransform);
                count++;
            }
        }
    }



    private void checkPair()
    {


        if (_firstSelection.GetComponent<BuildPairItem>().Type == _secondSelection.GetComponent<BuildPairItem>().Type)
        {
            GameObject _theChar;
            GameObject _theBuild;

            //mover char hasta build
            if (_firstSelection.GetComponent<BuildPairItem>().PairItemType == BuildPairItem.PairType.BUILD)
            {
                _theBuild = _firstSelection;
                _theChar = _secondSelection;
            }
            else
            {
                _theBuild = _secondSelection;
                _theChar = _firstSelection;
            }

            _theChar.GetComponent<CharItem>().moveToBuild(_theBuild.transform, _charTimeToGetBuild);

            _firstSelection.GetComponent<SpriteRenderer>().color = _secondSelection.GetComponent<SpriteRenderer>().color = Color.red;
            _firstSelection.GetComponent<BuildPairItem>().PairedUp = _secondSelection.GetComponent<BuildPairItem>().PairedUp = true;

            Debug.Log("Correcto");
            _pairsMatched++;

        }
        else
        {
            _firstSelection.GetComponent<SpriteRenderer>().color = _secondSelection.GetComponent<SpriteRenderer>().color = Color.white;

            Debug.Log("Incorecto");

        }

        _firstSelection = _secondSelection = null;
        _itemSelected = false;

    }

    private void setGreyGameObject(GameObject go)
    {
        go.GetComponent<SpriteRenderer>().color = Color.grey;
    }

    private void findItemOnChars(string commandName)
    {
        CharItem[] _charItem = FindObjectsOfType<CharItem>();

        foreach (var item in _charItem)
        {

            if (item.RecognitionName.Equals(commandName, System.StringComparison.OrdinalIgnoreCase) && !item.PairedUp)
            {
                if (_itemSelected)
                {
                    if (item.gameObject != _firstSelection)
                    {
                        _secondSelection = item.gameObject;
                        setGreyGameObject(item.gameObject);
                    }
                }
                else
                {
                    _firstSelection = item.gameObject;
                    _itemSelected = true;
                    setGreyGameObject(item.gameObject);

                }
            }
        }
    }

    private void findItemOnBuilds(string commandName)
    {

        BuildItem[] _buildItem = FindObjectsOfType<BuildItem>();
        foreach (var item in _buildItem)
        {
            if (item.RecognitionName.Equals(commandName, System.StringComparison.OrdinalIgnoreCase) && !item.PairedUp)
            {

                if (_itemSelected)
                {
                    if (item.gameObject != _firstSelection)
                    {
                        _secondSelection = item.gameObject;
                        setGreyGameObject(item.gameObject);
                    }
                }
                else
                {
                    _firstSelection = item.gameObject;
                    _itemSelected = true;
                    setGreyGameObject(item.gameObject);
                }
            }
        }
    }
}
