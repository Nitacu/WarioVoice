using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DopDrownEnchantments : MonoBehaviour
{
    private Dropdown _dropdown;
    [SerializeField]private List<string> _enchantmentsList = new List<string>();

    private void Start()
    {
        _dropdown = GetComponent<Dropdown>();
        _dropdown.AddOptions(_enchantmentsList);
    }
}
