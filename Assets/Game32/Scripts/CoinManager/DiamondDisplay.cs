using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondDisplay : MonoBehaviour
{
   public TextMeshProUGUI diamondTmp;

   void OnValidated()
   {
      diamondTmp = GetComponent<TextMeshProUGUI>();
   }

   void Start()
   {
      GameDataManager.Ins.playerData.onChangeDiamond += i => OnChangeHelp(i);
      diamondTmp.text = $"{GameDataManager.Ins.playerData.intHelp} Help";
   }
   
   void OnDestroy()
   {
      GameDataManager.Ins.playerData.onChangeDiamond -= i => OnChangeHelp(i);
   }

   private void OnChangeHelp(int i)
   {
      diamondTmp.text = $"{i} Help";
   }
}
