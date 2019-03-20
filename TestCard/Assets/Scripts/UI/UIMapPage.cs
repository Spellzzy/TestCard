using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UIMapPage :  Window{

    private GList mapShowList;

    private List<StageInfo> stageInfoList;

    private StageInfo[,] stageMapTable;

    protected override void OnInit()
    {
        base.OnInit();
        contentPane = UIPackage.CreateObject("main", "map_page").asCom;

        // 地图关卡展示列表 共三页(3ITEM) (1/2页 为普通关 第3页为Boss)
        mapShowList = contentPane.GetChild("map_list").asList;
        mapShowList.RemoveChildren(0, -1, true);

        GetMapInfo();

        SetMapInfo();

    }

    private void GetMapInfo()
    {
        stageInfoList = ReadXML.GetInfoList<StageInfo>(Application.dataPath + "/Resources/XML/" + "map_info.xml");
    }

    private void SetMapInfo()
    {
        stageMapTable = new StageInfo[11, 3];
        for (int i = 0; i < stageInfoList.Count; i++)
        {
            stageMapTable[stageInfoList[i].XPos, stageInfoList[i].YPos] = stageInfoList[i];
        }

        //
        SetMapListItem(0);
        SetMapListItem(5);
        SetBoss();
    }

    public void SetMapListItem(int ik)
    {
        GComponent panel_1 = UIPackage.CreateObject("main", "map_panel").asCom;

        GList column_list = panel_1.GetChild("column_list").asList;
        column_list.RemoveChildrenToPool();

        for (int i = 0 + ik; i < 5+ ik; i++)
        {
            // 获取每列 关卡
            GComponent column_com = column_list.GetFromPool("").asCom;

            for (int j = 0; j < 3; j++)
            {
                GComponent stage_com = column_com.GetChild("stage_" + (j + 1).ToString()).asCom;
                //是否存在该 位置关卡
                if (stageMapTable[i, j] == null)
                {
                    Debug.LogError(i + " : " + j + " null");
                    // 空
                    stage_com.visible = false;
                    continue;
                }
                else
                {
                    // 当前关卡 数据
                    StageInfo data = stageMapTable[i, j];
                    // 类型
                    FairyGUI.Controller type_con = stage_com.GetController("type");
                    type_con.SetSelectedIndex((int)data.StageType);
                    // xy
                    GTextField name_text = stage_com.GetChild("name_text").asTextField;
                    name_text.text = data.XPos.ToString() + "  ---- " + data.YPos.ToString();

                    stage_com.data = data;
                    stage_com.onClick.Set(OnClickStageBtn);

                    // 画线
                    for (int k = 0; k < data.NextYPos.Count; k++)
                    {
                        if (data.NextYPos[k] != -1)
                        {
                            if (data.NextYPos[k] > data.YPos)
                            {
                                //在下一行 右下
                                GComponent ES = stage_com.GetChild("ES").asCom;
                                ES.visible = true;
                            }
                            else if (data.NextYPos[k] < data.YPos)
                            {
                                //在上一行 右上
                                GComponent ES = stage_com.GetChild("EN").asCom;
                                ES.visible = true;
                            }
                            else
                            {
                                // 在同一行 右
                                GComponent ES = stage_com.GetChild("EAST").asCom;
                                ES.visible = true;
                            }
                        }
                    }
                    // 赋值
                    stage_com.data = data;
                }
            }
            column_list.AddChild(column_com);
        }
        mapShowList.AddChild(panel_1);
    }


    public void SetBoss()
    {
        if (stageMapTable[10, 0] != null)
        {
            GComponent map_boss = UIPackage.CreateObject("main", "map_boss").asCom;

            mapShowList.AddChild(map_boss); 
        }
    }

    public void OnClickStageBtn(EventContext context)
    {
        StageInfo data = (StageInfo)((GButton)context.sender).data;
        if (null == data)
        {
            return;
        }

        Controller.Instance.IntoBattle(data.MonsterID);
    }

    
}
