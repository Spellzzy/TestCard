using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UILoginPage : Window {

    protected GButton startBtn;

    protected override void OnInit()
    {
        base.OnInit();

        UIPackage.AddPackage("UI/main");

        contentPane = UIPackage.CreateObject("main", "main_page").asCom;

        // 开始按钮 进入新游戏
        startBtn = contentPane.GetChild("start_btn").asButton;
        startBtn.onClick.Set(() => {
            Hide();
            Controller.Instance.view.OpenSelectPage();
        });
    }
}
