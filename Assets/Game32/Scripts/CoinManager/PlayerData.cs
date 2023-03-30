using System;
using UnityEngine;

public class Constant
{
    public static string DataKey_PlayerData = "player_data";
    public static int countSong = 3;
    public static int priceUnlockSkin = 100;
}

public class PlayerData : BaseData
{
    public int intHelp;
    public int intLevel;
    public int currentSkin;
    public bool[] listSkins;

    public Action<int> onChangeDiamond;

    public override void Init()
    {
        prefString = Constant.DataKey_PlayerData;
        if (PlayerPrefs.GetString(prefString).Equals(""))
        {
            ResetData();
        }

        base.Init();
    }
    
    public void Unlock(int id)
        {
            if (!listSkins[id])
            {
                listSkins[id] = true;
            }
    
            Save();
        }


    public override void ResetData()
    {
        intHelp = 0;
        intLevel = 0;
        currentSkin = 0;
        listSkins = new bool[Constant.countSong];

        for (int i = 0; i < 1; i++)
        {
            listSkins[i] = true;
        }

        Save();
    }

    public void LevelUP()
    {
        intLevel++;
        Save();
    }
    
    public bool CheckLock(int id)
    {
        return this.listSkins[id];
    }
    
    public void ChooseSong(int i)
        {
            currentSkin = i;
            Save();
        }

    

    public void AddDiamond(int a)
    {
        intHelp += a;

        onChangeDiamond?.Invoke(intHelp);
        
        Save();
    }

    public bool CheckCanUnlockChange()
    {
        return intHelp >= Constant.priceUnlockSkin;
    }

    public void SubHelp(int a)
    {
        intHelp -= a;

        if (intHelp < 0)
        {
            intHelp = 0;
        }

        onChangeDiamond?.Invoke(intHelp);
        
        Save();
    }

    
}