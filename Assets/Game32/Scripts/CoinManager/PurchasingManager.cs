using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasingManager : MonoBehaviour
{
    public void OnPressDown(int i)
    {
        switch (i)
        {
            case 1:
                IAPManager.OnPurchaseSuccess = () =>
                    GameDataManager.Ins.playerData.AddDiamond(1);
                IAPManager.Ins.BuyProductID(Key.PACK1);
                break;
            case 2:
                IAPManager.OnPurchaseSuccess = () =>
                    GameDataManager.Ins.playerData.AddDiamond(2);
                IAPManager.Ins.BuyProductID(Key.PACK2);
                break;
            case 6:
                IAPManager.OnPurchaseSuccess = () =>
                    GameDataManager.Ins.playerData.AddDiamond(100);
                IAPManager.Ins.BuyProductID(Key.PACK6);
                break;
            case 5:
                IAPManager.OnPurchaseSuccess = () =>
                    GameDataManager.Ins.playerData.AddDiamond(50);
                IAPManager.Ins.BuyProductID(Key.PACK5);
                break;
            case 3:
                IAPManager.OnPurchaseSuccess = () =>
                    GameDataManager.Ins.playerData.AddDiamond(3);
                IAPManager.Ins.BuyProductID(Key.PACK3);
                break;
            case 4:
                IAPManager.OnPurchaseSuccess = () =>
                    GameDataManager.Ins.playerData.AddDiamond(10);
                IAPManager.Ins.BuyProductID(Key.PACK4);
                break;
        }
    }

    public void Sub(int i)
    {
        GameDataManager.Ins.playerData.SubHelp(i);
    }
}