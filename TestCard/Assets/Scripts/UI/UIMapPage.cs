using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UIMapPage :  Window{

    private GList mapShowList;

    private List<StageInfo> stageInfoList;

    protected override void OnInit()
    {
        base.OnInit();

        contentPane = UIPackage.CreateObject("main", "").asCom;
    }

    private void GetMapInfo()
    {
        stageInfoList = ReadXML.GetInfoList<StageInfo>(Application.dataPath + "/Resources/XML/" + "stage_info.xml");
    }

    private void SetMapInfo()
    {
        for (int i = 0; i < stageInfoList.Count; i++)
        {

        }
    }
}
