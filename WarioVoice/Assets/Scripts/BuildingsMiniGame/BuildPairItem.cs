using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildPairItem : MonoBehaviour
{
    private const string REDUCE_CLIP = "Reduce";

    private bool _showingText;
#pragma warning disable CS0649 // El campo 'BuildPairItem._dialogTextName' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _dialogTextName;
#pragma warning restore CS0649 // El campo 'BuildPairItem._dialogTextName' nunca se asigna y siempre tendrá el valor predeterminado null
    private GameObject _instantiatedDialogText;
#pragma warning disable CS0649 // El campo 'BuildPairItem._dialogTextOffset' nunca se asigna y siempre tendrá el valor predeterminado 
    [SerializeField] private Vector3 _dialogTextOffset;
#pragma warning restore CS0649 // El campo 'BuildPairItem._dialogTextOffset' nunca se asigna y siempre tendrá el valor predeterminado 


    private float _showingTextTime = 4f;
    private float _currentTimeShowingText = 0;
    private float _timeToDestroyText = 0.3f;


    public virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;

            if (Input.touches.Length > 0)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            }
            else
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject && !_showingText)
                {
                    if (!PairedUp)
                    {
                        _instantiatedDialogText = Instantiate(_dialogTextName, GameObject.Find("Canvas").transform);
                        _instantiatedDialogText.transform.position = gameObject.transform.position + _dialogTextOffset;
                        _instantiatedDialogText.GetComponentInChildren<TextMeshProUGUI>().text = RecognitionName;
                        //FindObjectOfType<BuildingsManager>().showItemName(hit.collider.gameObject.GetComponent<BuildPairItem>().RecognitionName);
                        _showingText = true;
                        _currentTimeShowingText = 0;
                    }
                }
                else
                {
                    if (_instantiatedDialogText != null)
                    {
                        _currentTimeShowingText = 0;
                        _showingText = false;
                        StartCoroutine(destroyText());

                    }
                }
            }
        }

        if (_showingText && _currentTimeShowingText < _showingTextTime)
        {
            _currentTimeShowingText += Time.deltaTime;
        }
        else
        {
            if (_currentTimeShowingText >= _showingTextTime)
            {
                _currentTimeShowingText = 0;
                _showingText = false;
                StartCoroutine(destroyText());
            }
        }
    }

    private IEnumerator destroyText()
    {
        _instantiatedDialogText.GetComponent<Animator>().Play(Animator.StringToHash(REDUCE_CLIP));
        yield return new WaitForSeconds(_timeToDestroyText);
        Destroy(_instantiatedDialogText);
    }


    public enum PairType
    {
        BUILD, CHAR
    }

    protected PairType _pairItemType;
    public PairType PairItemType
    {
        get { return _pairItemType; }
    }

    private bool _pairedUp;
    public bool PairedUp
    {
        get { return _pairedUp; }
        set { _pairedUp = value; }
    }

    protected string _recognitionName;
    public string RecognitionName
    {
        get { return _recognitionName; }
    }

    [SerializeField] protected BuildingVocabulary.PairType _type;

    public BuildingVocabulary.PairType Type
    {
        get { return _type; }
    }
}
