using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UISelectPage : Window {

    private FairyGUI.Controller select_con;

    private List<CarrerInfo> carrerInfoList;

    private GComponent infoPage;

    protected override void OnInit()
    {
        base.OnInit();

        carrerInfoList = ReadXML.GetInfoList<CarrerInfo>(Application.dataPath + "/Resources/XML/" + "carrer_info.xml");
    }

    protected override void OnShown()
    {
        base.OnShown();
        contentPane = UIPackage.CreateObject("main", "select_page").asCom;

        select_con = contentPane.GetController("select");

        SetSelectBtn();
    }

    protected override void OnHide()
    {
        base.OnHide();
        GRoot.inst.RemoveChild(infoPage);
        infoPage = null;
    }

    private void SetSelectBtn()
    {
        for (int i = 0; i < carrerInfoList.Count; i++)
        {
            GButton btn = contentPane.GetChild("btn_"+ (i + 1).ToString()).asButton;

            btn.data = carrerInfoList[i];
            btn.onClick.Set(OnClickSelectBtn);
        }
    }

    private void OnClickSelectBtn(EventContext context)
    {
        CarrerInfo info = (CarrerInfo)((GButton)context.sender).data;
        Debug.LogError("----->  " + info.ID);

        SetInfoPage(info);
    }

    private void OnClickBack()
    {
        select_con.SetSelectedIndex(0);
        infoPage.visible = false;
    }

    private void OnClickConfirm()
    {
        Hide();
        // todo 打开地图界面 开始游戏 随机出一条游戏 线路 
    }

    private void SetInfoPage(CarrerInfo info)
    {
        if (infoPage == null)
        {
            infoPage = UIPackage.CreateObject("main", "select_panel").asCom;
            infoPage.visible = false;

            GButton btn_back = infoPage.GetChild("btn_back").asButton;
            btn_back.onClick.Set(OnClickBack);

            GButton btn_confirm = infoPage.GetChild("btn_confirm").asButton;
            btn_confirm.onClick.Set(OnClickConfirm);

            GRoot.inst.AddChild(infoPage);
        }

        GLoader loade_icon = infoPage.GetChild("loade_icon").asLoader;

        GTextField text_name = infoPage.GetChild("text_name").asTextField;
        GTextField text_hp = infoPage.GetChild("text_hp").asTextField;
        GTextField text_desc = infoPage.GetChild("text_desc").asTextField;

        text_name.text = info.Name;

        text_hp.text = string.Format("{0}/{1}", info.HP.ToString(), info.HP.ToString());

        text_desc.text = info.Desc;

        loade_icon.url = UIPackage.GetItemURL("main", info.Icon);

        infoPage.visible = true;
    }

    private void GetCurCarrerInfo()
    {

    }
}
