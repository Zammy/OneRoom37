using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerType
{
    Gamepad,
    Keyboard
}

public enum CharacterType
{
    Detective,
    Mike,
    Ninja,
    Kristoff
}

public class PlayerInfo
{
    public static PlayerInfo[] PlayerInfos;
    // =
    // {
    //       new PlayerInfo
    //       {
    //             ChracterType = CharacterType.Detective,
    //             PlayerIndex = 0
    //       },
    //         new PlayerInfo
    //       {
    //             ChracterType = CharacterType.Kristoff,
    //             PlayerIndex = 1
    //       }
    // };

    public CharacterType ChracterType { get; set; }

    public int PlayerIndex  { get; set; }

    public int ControllerIndex  { get; set; }

    public ControllerType ControllerType { get; set; }
}
