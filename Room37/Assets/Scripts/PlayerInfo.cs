using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public CharacterType ChracterType
    {
        get;
        set;
    }

    public int PlayerIndex
    {
        get;
        set;
    }
}
