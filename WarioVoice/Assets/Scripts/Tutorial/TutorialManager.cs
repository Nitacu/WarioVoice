using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Estos seran los encantamientos que se debloquearan")]
    public PlayerGrimoire.enchantment[] _unlockedEnchantment;

    [Header("Estos seran los comandos que se debloquearan")]
    public PlayerGrimoire.commands[] _unlockedCommands;

    [Header("Objeto usado para explicar el tutorial")]
    public EnchantableObjTags.Tags _tag;

    [Header("Lista de encatamientos en orden")]
    public List<PlayerGrimoire.enchantment> _enchantments = new List<PlayerGrimoire.enchantment>();

    [Header("Lista de comandos en orden")]
    public List<PlayerGrimoire.commands> _commands = new List<PlayerGrimoire.commands>();

    public List<bool> _instructionIsEnchantment = new List<bool>();

    //scripts donde se de los EXE
    private OcelotMovements _ocelotMovements;
    private EnchantmentsExe _enchantmentsExe;
    private DialogManager _dialogManager;

    private void Start()
    {
        _ocelotMovements = FindObjectOfType<OcelotMovements>();
        _enchantmentsExe = FindObjectOfType<EnchantmentsExe>();
        _dialogManager = FindObjectOfType<DialogManager>();

        commandsUnlock();
        enchantmentUnlock();
    }

    private void enchantmentUnlock()
    {
        foreach (PlayerGrimoire.enchantment enchantment in _unlockedEnchantment)
        {
            switch (enchantment)
            {
                case PlayerGrimoire.enchantment.BIGGER_IS_BETTER:
                    PlayerGrimoire.GetInstance()._biggerIsBetter = true;
                    break;

                case PlayerGrimoire.enchantment.SMALLER_IS_CUTE:
                    PlayerGrimoire.GetInstance()._smallIscute = true;
                    break;

                case PlayerGrimoire.enchantment.GRAVITY_BREAKS:
                    PlayerGrimoire.GetInstance()._gravityBreak = true;
                    break;

                case PlayerGrimoire.enchantment.IGNITE_SPARK:
                    PlayerGrimoire.GetInstance()._igniteSpark = true;
                    break;
            }
        }
    }

    private void commandsUnlock()
    {
        foreach (PlayerGrimoire.commands commands in _unlockedCommands)
        {
            switch (commands)
            {
                case PlayerGrimoire.commands.WALK:
                    PlayerGrimoire.GetInstance()._wall = true;
                    break;

                case PlayerGrimoire.commands.RUN:
                    PlayerGrimoire.GetInstance()._run = true;
                    break;

                case PlayerGrimoire.commands.STAY_PUT:
                    PlayerGrimoire.GetInstance()._stayPut = true;
                    break;

                case PlayerGrimoire.commands.JUMP:
                    PlayerGrimoire.GetInstance()._jump = true;
                    break;

                case PlayerGrimoire.commands.WALK_FORWARD:
                    PlayerGrimoire.GetInstance()._walkForward = true;
                    break;

                case PlayerGrimoire.commands.WALK_BACKWARDS:
                    PlayerGrimoire.GetInstance()._walkBackwards = true;
                    break;

                case PlayerGrimoire.commands.GRAB:
                    PlayerGrimoire.GetInstance()._grabObj = true;
                    break;

                default:
                    Debug.Log("no se encuentra ese comando para desbloquear");
                    break;
            }
        }
    }

    private void finishTutorial()
    {
        GetComponent<ChangeScene>().chanceScene();
    }

    public void tutorialCommadExe(PlayerGrimoire.commands commands, EnchantableObjTags.Tags tag = EnchantableObjTags.Tags.NONE)
    {
        if (_instructionIsEnchantment.Count > 0)
        {
            if (!_instructionIsEnchantment[0])
            {
                if (commands == _commands[0])
                {
                    
                    //sacar lo que esta de primeras
                    _commands.RemoveAt(0);
                    _instructionIsEnchantment.RemoveAt(0);
                    // coloca la siguiete linea de dialogo
                    _dialogManager.DisplayNextSentence();

                    switch (commands)
                    {
                        case PlayerGrimoire.commands.JUMP:
                            _ocelotMovements.jump();
                            break;

                        case PlayerGrimoire.commands.RUN:
                            _ocelotMovements.run();
                            break;

                        case PlayerGrimoire.commands.WALK:
                            _ocelotMovements.move();
                            break;
                        case PlayerGrimoire.commands.STAY_PUT:
                            _ocelotMovements.idle();
                            break;
                        case PlayerGrimoire.commands.WALK_FORWARD:
                            _ocelotMovements.walkForward();
                            break;
                        case PlayerGrimoire.commands.WALK_BACKWARDS:
                            _ocelotMovements.walkBackwards();
                            break;
                        case PlayerGrimoire.commands.GRAB:
                            _ocelotMovements.grab();
                            break;
                    }

                    if (_instructionIsEnchantment.Count == 0)
                    {
                        Invoke("finishTutorial", 3);
                    }
                }
            }
        }
    }

    public void tutorialEnchantmentExe(PlayerGrimoire.enchantment enchantment, EnchantableObjTags.Tags tag)
    {
        
        if (_instructionIsEnchantment.Count > 0)
        {
            if (_instructionIsEnchantment[0])
            {
                if (enchantment == _enchantments[0] && _tag == tag)
                {
                    //sacar lo que esta de primeras
                    _enchantments.RemoveAt(0);
                    _instructionIsEnchantment.RemoveAt(0);
                    // coloca la siguiete linea de dialogo
                    _dialogManager.DisplayNextSentence();
                   
                    switch (enchantment)
                    {
                        case PlayerGrimoire.enchantment.BIGGER_IS_BETTER:
                            _enchantmentsExe.makeBigger(tag);
                            break;

                        case PlayerGrimoire.enchantment.GRAVITY_BREAKS:
                            _enchantmentsExe.gravityInvert(tag);
                            break;

                        case PlayerGrimoire.enchantment.IGNITE_SPARK:
                            _enchantmentsExe.spark(tag);
                            break;

                        case PlayerGrimoire.enchantment.SMALLER_IS_CUTE:
                            _enchantmentsExe.makeSmaller(tag);
                            break;

                        case PlayerGrimoire.enchantment.SHOW_ME_MORE:
                            _enchantmentsExe.showTags();
                            break;
                    }

                    if (_instructionIsEnchantment.Count == 0)
                    {
                        Invoke("finishTutorial", 3);
                    }
                }
            }
        }
        else
        {
            // termina el tutorial
            Invoke("finishTutorial", 3);
        }

    }

}
