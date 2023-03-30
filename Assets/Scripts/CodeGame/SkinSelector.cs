using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    private PlayerData playerData; //todo delete
    private DirGameDataManager dirGameData; 
    public int currentSkin;
    public SkinItem[] skinItems;

    void Start()
    {
        dirGameData = DirGameDataManager.Ins;
        playerData = dirGameData.playerData;
        
        currentSkin = playerData.currentSkin;
        
        for (int i = 0; i < skinItems.Length; i++)
        {
            if (playerData.listSkins[i])
            {
                skinItems[i].Unlock();
                skinItems[i].UnChoose();
            }
        }
        
        skinItems[currentSkin].Choose();
    }
    
    public void SkinChoosen(int index)
    {
        if (currentSkin == index)
        {
            return;
        }
        else if (playerData.listSkins[index] == false)
        {
            if (!playerData.CheckCanUnlockChange())
            {
                return;
            }
            unSkin(index);
        }
        
        skinItems[currentSkin].UnChoose();
        skinItems[index].Choose();
        currentSkin = index;
        playerData.currentSkin = currentSkin;
        //todo add Playerdata
    }

    public void unSkin(int index)
    {
        if (!playerData.listSkins[index])
        {
            playerData.SubHelp(Constant.priceUnlockSkin);
        }
        
        skinItems[index].Unlock();
        playerData.listSkins[index] = true;
    }

}
