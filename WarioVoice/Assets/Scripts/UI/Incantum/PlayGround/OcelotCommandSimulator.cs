using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OcelotCommandSimulator : MonoBehaviour
{
    [SerializeField] private Dropdown _listCommands;
    [SerializeField] private Dropdown _listObj;
    private CommandParser _commandParser; 

    private void Start()
    {
        _commandParser = FindObjectOfType<CommandParser>();
    }

    public void applyCommand()
    {

        switch (_listCommands.value)
        {
            case 0:
                _commandParser.selectExe(PlayerGrimoire.STAY_PUT + "#*#" + Tags.Ocelot);
                break;

            case 1:
                _commandParser.selectExe(PlayerGrimoire.WALK + "#*#" + Tags.Ocelot);
                break;

            case 2:
                _commandParser.selectExe(PlayerGrimoire.RUN + "#*#" + Tags.Ocelot);
                break;

            case 3:
                _commandParser.selectExe(PlayerGrimoire.JUMP + "#*#" + Tags.Ocelot);
                break;

            case 4:
                _commandParser.selectExe(PlayerGrimoire.WALK_FORWARD + "#*#" + Tags.Ocelot);
                break;

            case 5:
                _commandParser.selectExe(PlayerGrimoire.WALK_BACKWARDS + "#*#" + Tags.Ocelot);
                break;

            case 6:
                _commandParser.selectExe(PlayerGrimoire.GRAB + "#*#" + Tags.Ocelot);
                break;

        }
    }
}
