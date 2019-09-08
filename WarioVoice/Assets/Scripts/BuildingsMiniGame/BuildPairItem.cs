using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPairItem : MonoBehaviour
{
    private bool _showingText;

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
            if (hit.collider != null )
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (!PairedUp)
                    {
                        FindObjectOfType<BuildingsManager>().showItemName(hit.collider.gameObject.GetComponent<BuildPairItem>().RecognitionName);
                        Debug.Log("hits");
                        Debug.Log(hit.collider.gameObject.name);
                        _showingText = true;
                    }
                }

                  
            }
        }
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
