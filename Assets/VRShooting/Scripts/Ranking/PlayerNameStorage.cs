using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerNameStorage : MonoBehaviour
{
    private static PlayerName NowPlayerName;
    private void Awake()
    {
        NowPlayerName = new PlayerName();
    }

    public static void AddChar(string _char)
    {
        
        var addedChar = new PlayerName(_char);
        NowPlayerName = NowPlayerName.Add(addedChar);
        Debug.Log("add:"+ NowPlayerName.name);

    }

    public static void DelChar()
    {
        NowPlayerName = NowPlayerName.Delete();
    }

    public static string GetPlayerName()
    {
        Debug.Log("get:" + NowPlayerName.name);
        return NowPlayerName.name;
    }

}




public class PlayerName
{
    public string name { get;}
    public PlayerName(string _name = "")
    {
        this.name = _name;
    }
    public PlayerName Add(PlayerName other)
    {
        if (other.name.Length != 1)
        {
            Debug.Log("1文字にしてください");
            return this;
        }
        if (this.name.Length > 7)
        {
            Debug.Log("名前の上限は7文字です");
            return this;
        }
        
        return new PlayerName(this.name+other.name);
    }
    public PlayerName Delete()
    {
        if (this.name.Length == 0)
        {
            return this;
        }
        
        return new PlayerName(this.name.Remove(name.Length - 1));
    }
}