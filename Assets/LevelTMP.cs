using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTMP : MonoBehaviour
{

    public TextMeshProUGUI textMeshProUgui;
    // Start is called before the first frame update
    void Start()
    {
        textMeshProUgui.SetText($"LEVEL {GameDataManager.Ins.playerData.intLevel + 1}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
