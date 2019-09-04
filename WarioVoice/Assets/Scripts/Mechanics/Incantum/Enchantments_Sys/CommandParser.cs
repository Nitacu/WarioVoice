using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommandParser : MonoBehaviour
{
    private EnchantmentsExe _enchantmentsExe;
    private OcelotMovements _ocelotMovements;
    private TutorialManager _tutorialManager;
    private EnchantableObjTags.Tags _tagEnchantableObj;
    private string[] _partsEnchantment;
    [SerializeField] private bool _tutorialMode = false;
    public enum enchantmentResponse
    {
        FAIL,
        SUCCESS
    }

    private void Start()
    {
        _enchantmentsExe = FindObjectOfType<EnchantmentsExe>();
        _ocelotMovements = FindObjectOfType<OcelotMovements>();

        if (FindObjectOfType<TutorialManager>())
        {
            _tutorialMode = true;
            _tutorialManager = FindObjectOfType<TutorialManager>();
        }

    }

    public void parseCommand(string command)
    {
        selectExe(command);
    }

    public void selectExe(string enchantment)
    {
        _partsEnchantment = enchantment.Split("#*#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        if (_partsEnchantment.Length < 3)
        {

            if (_partsEnchantment.Length == 1)
            {
                selectEnchantment(_partsEnchantment[0]);

            }
            else if (_partsEnchantment[1] == Tags.Ocelot)
            {

                selectCommand(_partsEnchantment[0]);
                
            }
            else
            {
                selectEnchantment(_partsEnchantment[0], _partsEnchantment[1]);
            }

        }
    }


    private void selectCommand(string command)
    {
        switch (command)
        {
            case PlayerGrimoire.WALK:
                if (!_tutorialMode)
                    _ocelotMovements.move();
                else
                    _tutorialManager.tutorialCommadExe(PlayerGrimoire.commands.WALK);
                break;

            case PlayerGrimoire.RUN:
                if (!_tutorialMode)
                    _ocelotMovements.run();
                else
                    _tutorialManager.tutorialCommadExe(PlayerGrimoire.commands.RUN);
                break;

            case PlayerGrimoire.JUMP:
                if (!_tutorialMode)
                    _ocelotMovements.jump();
                else
                    _tutorialManager.tutorialCommadExe(PlayerGrimoire.commands.JUMP);
                break;

            case PlayerGrimoire.STAY_PUT:
                if (!_tutorialMode)
                    _ocelotMovements.idle();
                else
                    _tutorialManager.tutorialCommadExe(PlayerGrimoire.commands.STAY_PUT);
                break;

            case PlayerGrimoire.WALK_FORWARD:
                if (!_tutorialMode)
                    _ocelotMovements.walkForward();
                else
                    _tutorialManager.tutorialCommadExe(PlayerGrimoire.commands.WALK_FORWARD);
                break;

            case PlayerGrimoire.WALK_BACKWARDS:
                if (!_tutorialMode)
                    _ocelotMovements.walkBackwards();
                else
                    _tutorialManager.tutorialCommadExe(PlayerGrimoire.commands.WALK_BACKWARDS);
                break;

            case PlayerGrimoire.GRAB:
                if (!_tutorialMode)
                    _ocelotMovements.grab();
                else
                    _tutorialManager.tutorialCommadExe(PlayerGrimoire.commands.GRAB);
                break;
        }
    }

    //solo los encamientos globales
    private void selectEnchantment(string enchantment)
    {
        switch (enchantment)
        {
            case PlayerGrimoire.SHOW_ME_MORE:
                if (!_tutorialMode)
                    _enchantmentsExe.showTags();
                else
                    _tutorialManager.tutorialEnchantmentExe(PlayerGrimoire.enchantment.SHOW_ME_MORE, _tagEnchantableObj);
                break;

        }
    }

    private void selectEnchantment(string enchantment, string obj)
    {
        //escoge el nombre del objeto
        switch (obj)
        {
            case Tags.Barrel:
                _tagEnchantableObj = EnchantableObjTags.Tags.BARREL;
                break;

            case Tags.Box:
                _tagEnchantableObj = EnchantableObjTags.Tags.BOX;
                break;

            case Tags.Candle:
                _tagEnchantableObj = EnchantableObjTags.Tags.CANDLE;
                break;

            case Tags.Ocelot:
                _tagEnchantableObj = EnchantableObjTags.Tags.OCELOT;
                break;

            case Tags.Container:
                _tagEnchantableObj = EnchantableObjTags.Tags.CONTAINER;
                break;

            case Tags.ElectricStation:
                _tagEnchantableObj = EnchantableObjTags.Tags.ELECTRIC_STATION;
                break;

            case Tags.Fountain:
                _tagEnchantableObj = EnchantableObjTags.Tags.FOUNTAIN;
                break;

            case Tags.Rabbit:
                _tagEnchantableObj = EnchantableObjTags.Tags.RABBIT;
                break;

            case Tags.Rock:
                _tagEnchantableObj = EnchantableObjTags.Tags.ROCK;
                break;

            case Tags.Rope:
                _tagEnchantableObj = EnchantableObjTags.Tags.ROPE;
                break;

            case Tags.Tree:
                _tagEnchantableObj = EnchantableObjTags.Tags.TREE;
                break;

            case Tags.Boat:
                _tagEnchantableObj = EnchantableObjTags.Tags.BOAT;
                break;

            case Tags.Statue:
                _tagEnchantableObj = EnchantableObjTags.Tags.STATUE;
                break;

            case Tags.Torch:
                _tagEnchantableObj = EnchantableObjTags.Tags.TORCH;
                break;

            case Tags.Chest:
                _tagEnchantableObj = EnchantableObjTags.Tags.CHEST;
                break;
        }

        // escoge el hechizo
        switch (enchantment)
        {
            case PlayerGrimoire.BIGGER_IS_BETTER:
                if (!_tutorialMode)
                    _enchantmentsExe.makeBigger(_tagEnchantableObj);
                else
                    _tutorialManager.tutorialEnchantmentExe(PlayerGrimoire.enchantment.BIGGER_IS_BETTER, _tagEnchantableObj);
                break;

            case PlayerGrimoire.SMALLER_IS_CUTE:
                if (!_tutorialMode)
                    _enchantmentsExe.makeSmaller(_tagEnchantableObj);
                else
                    _tutorialManager.tutorialEnchantmentExe(PlayerGrimoire.enchantment.SMALLER_IS_CUTE, _tagEnchantableObj);
                break;

            case PlayerGrimoire.GRAVITY_BREAKS:
                if (!_tutorialMode)
                    _enchantmentsExe.gravityInvert(_tagEnchantableObj);
                else
                    _tutorialManager.tutorialEnchantmentExe(PlayerGrimoire.enchantment.GRAVITY_BREAKS, _tagEnchantableObj);
                break;

            case PlayerGrimoire.IGNITE_SPARK:
                if (!_tutorialMode)
                    _enchantmentsExe.spark(_tagEnchantableObj);
                else
                    _tutorialManager.tutorialEnchantmentExe(PlayerGrimoire.enchantment.IGNITE_SPARK, _tagEnchantableObj);
                break;

        }
    }
}
