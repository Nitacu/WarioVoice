using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class BuildingVocabulary
{
    #region VOCABULARIOCHARS
    public const string POLICEMAN = "POLICEMAN";
    public const string DOCTOR = "DOCTOR";
    public const string STUDENT = "STUDENT";
    public const string CLOWN = "CLOWN";
    #endregion

    #region VOCABULARIOBUILDS
    public const string POLICESTATION = "POLICE STATION";
    public const string HOSPITAL = "HOSPITAL";
    public const string SCHOOL = "SCHOOL";
    public const string CIRCUS = "CIRCUS";
    #endregion

    public enum PairType
    {
        POLICE, STUDENT, DOCTOR, CLOWN
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
    [SerializeField] private int _currentLevel = 0;

    private GameObject _firstSelection;
    private GameObject _secondSelection;
    private bool _itemSelected;

    private BuildingVocabulary.PairType _currentType;


    private void Start()
    {
        generateLevel(_currentLevel);
    }

    public override void parseCommand(string command)
    {
        findItemOnBuilds(command);
        findItemOnChars(command);

        if (_firstSelection != null && _secondSelection != null)
        {
            checkPair();
        }

        base.parseCommand(command);
    }

    public void generateLevel(int level)
    {
        //Asignar que parejas se crearán
        List<Pairs> _scenePairs = new List<Pairs>();

        for (int i = 0; i < _levels[level].NumberOfPairs; i++)
        {
            int _indexRandom = Random.Range(0, _levels[level].Pairs.Count);
            _scenePairs.Add(_levels[level].Pairs[_indexRandom]);
            _levels[level].Pairs.RemoveAt(_indexRandom);

        }

        int count = 0;//saber cuántos se han creado
        List<int> _usedIndexes = new List<int>();
        
        //create builds
        while (count < _scenePairs.Count)
        {
            int _randomBuildTransform = Random.Range(0, _buildTransforms.Count);
            if (!_usedIndexes.Contains(_randomBuildTransform))
            {
                GameObject _newBuild = Instantiate(_scenePairs[count].BuildPrefab);
                _newBuild.transform.position = _buildTransforms[_randomBuildTransform].position;
                count++;
            }
        }

        count = 0;//saber cuántos se han creado
        _usedIndexes = new List<int>();
        _usedIndexes.Clear();

        //create builds
        while (count < _scenePairs.Count)
        {
            int _randomCharTransform = Random.Range(0, _charTransforms.Count);
            if (!_usedIndexes.Contains(_randomCharTransform))
            {
                GameObject _newBuild = Instantiate(_scenePairs[count].CharPrefab);
                _newBuild.transform.position = _charTransforms[_randomCharTransform].position;
                count++;
            }
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
    }

    private void checkPair()
    {
        Debug.Log("1st selection type: " + _firstSelection.name);
        Debug.Log("2nd selection type: " + _secondSelection.name);

        if (_firstSelection.GetComponent<BuildPairItem>().Type == _secondSelection.GetComponent<BuildPairItem>().Type)
        {
            _firstSelection.GetComponent<SpriteRenderer>().color = _secondSelection.GetComponent<SpriteRenderer>().color = Color.red;
            _firstSelection.GetComponent<BuildPairItem>().PairedUp = _secondSelection.GetComponent<BuildPairItem>().PairedUp = true;

            Debug.Log("Correcto");
            _firstSelection = _secondSelection = null;

        }
        else
        {
            Debug.Log("Incorecto");

        }
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
            if (item.RecognitionName == commandName && !item.PairedUp)
            {
                Debug.Log("Finded: " + item.RecognitionName + " = " + commandName);


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
            if (item.RecognitionName == commandName && !item.PairedUp)
            {
                Debug.Log("Finded: " + item.RecognitionName + " = " + commandName);

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

//GUIPROPERTY
public class NamedLevesArrayAttribute : PropertyAttribute
{
    public readonly string[] names;
    public NamedLevesArrayAttribute(string[] names) { this.names = names; }
}

[CustomPropertyDrawer(typeof(NamedLevesArrayAttribute))]
public class NamedLevesArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        try
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.ObjectField(position, property, new GUIContent(((NamedLevesArrayAttribute)attribute).names[pos]));
        }
        catch
        {
            EditorGUI.ObjectField(position, property, label);

        }
        base.OnGUI(position, property, label);
    }
}