using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UIMapPage :  Window{

    private GList mapShowList;
    protected override void OnInit()
    {
        base.OnInit();

        contentPane = UIPackage.CreateObject("main", "").asCom;
    }
}
