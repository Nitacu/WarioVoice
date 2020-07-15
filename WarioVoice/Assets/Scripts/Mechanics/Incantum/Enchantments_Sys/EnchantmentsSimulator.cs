using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EnchantmentsSimulator : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'EnchantmentsSimulator._listEnchantments' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Dropdown _listEnchantments;
#pragma warning restore CS0649 // El campo 'EnchantmentsSimulator._listEnchantments' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'EnchantmentsSimulator._listEnchantmentObjs' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private Dropdown _listEnchantmentObjs;
#pragma warning restore CS0649 // El campo 'EnchantmentsSimulator._listEnchantmentObjs' nunca se asigna y siempre tendrá el valor predeterminado null

    private CommandParser _commandParser;

    private void Start()
    {
        _commandParser = FindObjectOfType<CommandParser>();
    }

    public void applySpell()
    {

        switch (_listEnchantments.value)
        {
            case 0:
                _commandParser.selectExe(PlayerGrimoire.SHOW_ME_MORE);
                break;

            case 1:
                _commandParser.selectExe(PlayerGrimoire.BIGGER_IS_BETTER+"#*#"+
                    _listEnchantmentObjs.options[_listEnchantmentObjs.value].text);
                break;

            case 2:
                _commandParser.selectExe(PlayerGrimoire.SMALLER_IS_CUTE + "#*#" +
                    _listEnchantmentObjs.options[_listEnchantmentObjs.value].text);
                break;

            case 3:
                _commandParser.selectExe(PlayerGrimoire.GRAVITY_BREAKS + "#*#" +
                    _listEnchantmentObjs.options[_listEnchantmentObjs.value].text);
                break;

            case 4:
                _commandParser.selectExe(PlayerGrimoire.IGNITE_SPARK + "#*#" +
                    _listEnchantmentObjs.options[_listEnchantmentObjs.value].text);
                break;

        }
    }

}
