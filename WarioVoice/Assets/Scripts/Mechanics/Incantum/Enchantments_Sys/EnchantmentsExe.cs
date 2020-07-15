using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnchantmentsExe : MonoBehaviour
{
    [SerializeField] private GameObject _objTextTags;
#pragma warning disable CS0649 // El campo 'EnchantmentsExe._magicRabbit' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _magicRabbit;
#pragma warning restore CS0649 // El campo 'EnchantmentsExe._magicRabbit' nunca se asigna y siempre tendrá el valor predeterminado null
    [Header("sistemas de particulas")]
#pragma warning disable CS0649 // El campo 'EnchantmentsExe._objSpark' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _objSpark;
#pragma warning restore CS0649 // El campo 'EnchantmentsExe._objSpark' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EnchantmentsExe._objMagicCloud' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _objMagicCloud;
#pragma warning restore CS0649 // El campo 'EnchantmentsExe._objMagicCloud' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EnchantmentsExe._objSnowflake' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _objSnowflake;
#pragma warning restore CS0649 // El campo 'EnchantmentsExe._objSnowflake' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _objFire;

    private List<GameObject> _listEnchantableObj;
    private LevelManager _levelManager;

    [Header("efectos de sonido")]
#pragma warning disable CS0649 // El campo 'EnchantmentsExe._audioSource' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioSource _audioSource;
#pragma warning restore CS0649 // El campo 'EnchantmentsExe._audioSource' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EnchantmentsExe._gravityBreak' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioClip _gravityBreak;
#pragma warning restore CS0649 // El campo 'EnchantmentsExe._gravityBreak' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EnchantmentsExe._ignite' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioClip _ignite;
#pragma warning restore CS0649 // El campo 'EnchantmentsExe._ignite' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EnchantmentsExe._magic' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioClip _magic;
#pragma warning restore CS0649 // El campo 'EnchantmentsExe._magic' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EnchantmentsExe._magicTags' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private AudioClip _magicTags;
#pragma warning restore CS0649 // El campo 'EnchantmentsExe._magicTags' nunca se asigna y siempre tendrá el valor predeterminado null

    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }


    //Enchantments

    //Vision Magica (Muestra tags de objetos encantables)
    public CommandParser.enchantmentResponse showTags()
    {
        if (PlayerGrimoire.GetInstance()._showMeMore)
        {
            _listEnchantableObj = _levelManager.findEnchantableObj();

            if (_listEnchantableObj.Count > 0)
            {
                foreach (GameObject aux in _listEnchantableObj)
                {
                    if (aux.GetComponent<EnchantableObjProperties>())  // para no crear mas de un globo de texto al tiempo
                    {
                        if (aux.GetComponent<EnchantableObjProperties>().AllowShowTag)
                        {
                            aux.GetComponent<EnchantableObj>().TextBaloon.SetActive(true);
                            aux.GetComponent<EnchantableObjProperties>().AllowShowTag = false;
                        }
                    }
                }

                _audioSource.clip = _magicTags;
                _audioSource.Play();

                return CommandParser.enchantmentResponse.SUCCESS;
            }
            else
            {
                return CommandParser.enchantmentResponse.FAIL;
            }
        }
        else
        {
            return CommandParser.enchantmentResponse.FAIL;
        }
    }


    //Earthquake

    //Rain

    //Make Bigger (x2)
    public CommandParser.enchantmentResponse makeBigger(EnchantableObjTags.Tags tag)
    {
        if (PlayerGrimoire.GetInstance()._biggerIsBetter)
        {
            _listEnchantableObj = _levelManager.findEnchantableObj(tag);

            if (_listEnchantableObj.Count > 0)
            {
                foreach (GameObject aux in _listEnchantableObj)
                {
                    // comprueba si a ese objeto ya se le habia modificado el tamaño o si esta disponible para hacerlo grande
                    if (aux.GetComponent<EnchantableObjProperties>().AllowMakeBigger || !aux.GetComponent<EnchantableObjProperties>().AllowMakeSmaller)
                    {
                        aux.transform.localScale = new Vector3(aux.transform.localScale.x * 2, aux.transform.localScale.y * 2, aux.transform.localScale.z);
                        Instantiate(_objMagicCloud, aux.transform);
                        // si el objeto era pequeño deja que lo vuelva a hacer pequeño o que lo siga agrandando
                        if (!aux.GetComponent<EnchantableObjProperties>().AllowMakeSmaller)
                        {
                            aux.GetComponent<EnchantableObjProperties>().AllowMakeSmaller = true;
                        }
                        // si el objeto estaba normal no deja que lo siga agrandando
                        else
                        {
                            aux.GetComponent<EnchantableObjProperties>().AllowMakeBigger = false;
                        }
                    }
                }

                _audioSource.clip = _magic;
                _audioSource.Play();
                return CommandParser.enchantmentResponse.SUCCESS;
            }
            else
            {
                return CommandParser.enchantmentResponse.FAIL;
            }
        }
        else
        {
            return CommandParser.enchantmentResponse.FAIL;
        }

    }


    //Make Smaller (x1/2)
    public CommandParser.enchantmentResponse makeSmaller(EnchantableObjTags.Tags tag)
    {
        if (PlayerGrimoire.GetInstance()._smallIscute)
        {
            _listEnchantableObj = _levelManager.findEnchantableObj(tag);

            if (_listEnchantableObj.Count > 0)
            {
                foreach (GameObject aux in _listEnchantableObj)
                {
                    // comprueba si a ese objeto ya se le habia modificado el tamaño o si esta disponible para hacerlo pequeño
                    if (aux.GetComponent<EnchantableObjProperties>().AllowMakeSmaller || !aux.GetComponent<EnchantableObjProperties>().AllowMakeBigger)
                    {
                        aux.transform.localScale = new Vector3(aux.transform.localScale.x / 2, aux.transform.localScale.y / 2, aux.transform.localScale.z);
                        Instantiate(_objMagicCloud, aux.transform);

                        // si el objeto era grande deja que lo vuelva a hacer grande o que lo siga pequeño
                        if (!aux.GetComponent<EnchantableObjProperties>().AllowMakeBigger)
                        {
                            aux.GetComponent<EnchantableObjProperties>().AllowMakeBigger = true;
                        }
                        // si el objeto estaba normal no deja que lo siga agrandando
                        else
                        {
                            aux.GetComponent<EnchantableObjProperties>().AllowMakeSmaller = false;
                        }

                    }
                }

                _audioSource.clip = _magic;
                _audioSource.Play();

                return CommandParser.enchantmentResponse.SUCCESS;
            }
            else
            {
                return CommandParser.enchantmentResponse.FAIL;
            }
        }
        else
        {
            return CommandParser.enchantmentResponse.FAIL;
        }
    }

    //Spark
    public CommandParser.enchantmentResponse spark(EnchantableObjTags.Tags tag)
    {
        if (PlayerGrimoire.GetInstance()._igniteSpark)
        {
            _listEnchantableObj = _levelManager.findEnchantableObj(tag);

            if (_listEnchantableObj.Count > 0)
            {
                foreach (GameObject aux in _listEnchantableObj)
                {
                    Instantiate(_objSpark, aux.transform);

                    if (aux.GetComponent<EnchantableObjProperties>().AllowIgnite)
                    {
                        aux.GetComponent<EnchantableObj>().turnOn();
                        aux.GetComponent<EnchantableObjProperties>().Onfire = true;
                    }
                    else if (aux.GetComponent<EnchantableObjProperties>().DestroyWhiteFire)
                    {
                        aux.AddComponent<SelfDestroy>().Time = 1;
                        Instantiate(ObjFire, aux.transform);
                    }else if (aux.GetComponent<EnchantableObjProperties>().OnBurn)
                    {
                        Instantiate(ObjFire, aux.transform).GetComponent<SelfDestroy>().enabled = false;
                        aux.GetComponent<EnchantableObjProperties>().Onfire = true;
                    }
                }

                _audioSource.clip = _ignite;
                _audioSource.Play();

                return CommandParser.enchantmentResponse.SUCCESS;
            }
            else
            {
                return CommandParser.enchantmentResponse.FAIL;
            }
        }
        else
        {
            return CommandParser.enchantmentResponse.FAIL;
        }
    }

    //Freeze
    public void freeze(string tag)
    {
        GameObject[] enchantableObjs = GameObject.FindGameObjectsWithTag(tag);

        if (enchantableObjs.Length > 0)
        {
            foreach (GameObject aux in enchantableObjs)
            {
                if (aux.GetComponent<EnchantableObjProperties>().AllowFreeze)
                {
                    aux.GetComponent<SpriteRenderer>().color = Color.cyan;
                    Instantiate(_objSnowflake, aux.transform); // la nube magica
                    aux.GetComponent<EnchantableObjProperties>().AllowFreeze = false;
                }
            }
        }
    }


    //DisEnchant (controlZ)

    //Renew (Vuelve al estado original)
    public void renew(string tag)
    {
        GameObject[] enchantableObjs = GameObject.FindGameObjectsWithTag(tag);

        if (enchantableObjs.Length > 0)
        {
            foreach (GameObject aux in enchantableObjs)
            {


            }
        }
    }

    //Gravity On (Activada)
    public void gravityOn(string tag)
    {
        GameObject[] enchantableObjs = GameObject.FindGameObjectsWithTag(tag);

        if (enchantableObjs.Length > 0)
        {
            foreach (GameObject aux in enchantableObjs)
            {
                if (aux.GetComponent<EnchantableObjProperties>().AllowChangeGravite)
                {
                    aux.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    Instantiate(_objMagicCloud, aux.transform);  // la nube magica
                }
            }
        }
    }

    //Gravity Off (Apagada)
    public void gravityOff(string tag)
    {
        GameObject[] enchantableObjs = GameObject.FindGameObjectsWithTag(tag);

        if (enchantableObjs.Length > 0)
        {
            foreach (GameObject aux in enchantableObjs)
            {
                aux.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                Instantiate(_objMagicCloud, aux.transform); // la nube magica
            }
        }
    }

    //Gravity Invert (Invertir)
    public CommandParser.enchantmentResponse gravityInvert(EnchantableObjTags.Tags tag)
    {

        if (PlayerGrimoire.GetInstance()._gravityBreak)
        {
            _listEnchantableObj = _levelManager.findEnchantableObj(tag);

            if (_listEnchantableObj.Count > 0)
            {
                foreach (GameObject aux in _listEnchantableObj)
                {
                    Debug.Log(aux.name);
                    if (aux.GetComponent<EnchantableObjProperties>().AllowChangeGravite)
                    {
                        
                        aux.GetComponent<Rigidbody2D>().gravityScale *= -1;
                        aux.GetComponent<EnchantableObj>().saveCurretGravityScale();
                        Instantiate(_objMagicCloud, aux.transform); // la nube magica
                    }
                }

                _audioSource.clip = _gravityBreak;
                _audioSource.Play();

                return CommandParser.enchantmentResponse.SUCCESS;
            }
            else
            {
                return CommandParser.enchantmentResponse.FAIL;
            }
        }
        else
        {
            return CommandParser.enchantmentResponse.FAIL;
        }
    }

    //Change Material Lana
    public void changeMaterialLana(string tag)
    {
        GameObject[] enchantableObjs = GameObject.FindGameObjectsWithTag(tag);

        if (enchantableObjs.Length > 0)
        {
            foreach (GameObject aux in enchantableObjs)
            {
                if (aux.GetComponent<EnchantableObjProperties>().AllowChangeMaterial)
                {
                    // si esta congelado no permite ver el cambio visual 
                    if (aux.GetComponent<EnchantableObjProperties>().AllowFreeze)
                    {
                        aux.GetComponent<SpriteRenderer>().color = Color.magenta;
                    }

                    // aplica las propiedades del hechizo
                    Instantiate(_objMagicCloud, aux.transform); // la nube magica
                    aux.GetComponent<EnchantableObjProperties>().AllowChangeMaterial = false;
                }
            }
        }
    }


    //Crear Conejito
    public void createdRabbit()
    {
        Instantiate(_magicRabbit);
    }

    //Abrir Cerraduras (ste es para la version final)


    public GameObject ObjFire { get => _objFire; set => _objFire = value; }
}
