using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private List<GameObject> _enchantmentsObjs = new List<GameObject>();

    void Start()
    {
        //llenar array con objetos encantables
        EnchantableObj[] arrayAux = FindObjectsOfType<EnchantableObj>();

        for (int i = 0; i < arrayAux.Length; i++)
        {
            _enchantmentsObjs.Add(arrayAux[i].gameObject);
        }
    }

    public void removeEnchantableObj(GameObject gameObject)
    {
        _enchantmentsObjs.Remove(gameObject);
    }

    //TODO: Crear funcion pra buscar objeto encantable por tag especial
    public List<GameObject> findEnchantableObj(EnchantableObjTags.Tags tag)
    {
        List<GameObject> enchantableObjList = new List<GameObject>();

        foreach (GameObject enchantableObj in _enchantmentsObjs)
        {
            if (enchantableObj.GetComponent<EnchantableObj>().EnchantableObjTag == tag)
            {
                enchantableObjList.Add(enchantableObj);
            }
        }

        return enchantableObjList;
    }

    // devuelve todos los objetos encantables
    public List<GameObject> findEnchantableObj()
    {
        List<GameObject> enchantableObjList = new List<GameObject>();

        foreach (GameObject enchantableObj in _enchantmentsObjs)
        {
            enchantableObjList.Add(enchantableObj);
        }

        return enchantableObjList;
    }

}
