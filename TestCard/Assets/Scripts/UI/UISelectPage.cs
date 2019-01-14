using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UISelectPage : Window {

    private GButton confirm_btn;

    protected override void OnInit()
    {
        base.OnInit();

        contentPane = UIPackage.CreateObject("main", "select_page").asCom;

        confirm_btn = contentPane.GetChild("confirm_btn").asButton;
    }
}
