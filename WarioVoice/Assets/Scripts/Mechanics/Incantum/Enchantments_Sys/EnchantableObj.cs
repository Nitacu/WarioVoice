using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnchantableObj : MonoBehaviour
{
    [Header("como se ve quemandose")]
    [SerializeField] private Sprite _sprBurn;
    [SerializeField] private Light _light;
    private Sprite _sprNormal;

    [Header("cosas para hacer saltar al gatos")]
    [SerializeField] private Transform _topPoint;
    [SerializeField] private GameObject _closeness;

    [SerializeField] private EnchantableObjTags.Tags _enchantableObjTag;

    [SerializeField] private GameObject _textBaloon;

    private LevelManager _levelManager;
    private bool _taken = false;
    private GameObject _mouthCat;
    private GameObject _wood;

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();

        if (GetComponent<SpriteRenderer>()) {
            _sprNormal = GetComponent<SpriteRenderer>().sprite;
        }
        saveCurretGravityScale();

    }

    private void Update()
    {
        if (Taken)
        {
            transform.position = _mouthCat.transform.position;
        }
    }

    public void followCat()
    {
        Taken = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void saveCurretGravityScale()
    {
        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<EnchantableObjProperties>().GravityScale = GetComponent<Rigidbody2D>().gravityScale;
        }
    }

    public void turnOn()
    {
        GetComponent<SpriteRenderer>().sprite = _sprBurn;
        Instantiate(_light,transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CAT") &&  GetComponent<EnchantableObjProperties>().AllowGrab)
        {
            collision.gameObject.GetComponent<OcelotMovements>().stopMove();
            collision.gameObject.GetComponent<OcelotMovements>().GrabObj = gameObject;

            _mouthCat = collision.gameObject.GetComponent<OcelotProperties>().Mouth;
        }
        else if(collision.gameObject.CompareTag("CAT") && _closeness == null)
        {
            collision.gameObject.GetComponent<OcelotMovements>().stopMove();
        }
    }

    public void onBurn()
    {
        if (GetComponent<EnchantableObjProperties>().Onfire && _wood != null)
        {
            _wood.AddComponent<SelfDestroy>().Time = 2;
            Instantiate(FindObjectOfType<EnchantmentsExe>().ObjFire, _wood.gameObject.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Wood) && GetComponent<EnchantableObjProperties>().OnBurn )
        {
            _wood = collision.gameObject;

            onBurn();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Wood))
        {
            _wood = null;
        }
    }

    public void OnDestroy()
    {
        _levelManager.removeEnchantableObj(gameObject);
    }

    // GET Y SET
    public Transform TopPoint { get => _topPoint; set => _topPoint = value; }
    public EnchantableObjTags.Tags EnchantableObjTag { get => _enchantableObjTag; set => _enchantableObjTag = value; }
    public bool Taken { get => _taken; set => _taken = value; }
    public GameObject TextBaloon { get => _textBaloon; set => _textBaloon = value; }
}
