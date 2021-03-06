﻿using UnityEngine;
using System.Xml;
using System.Collections;
using System.IO;
using UnityEditor;

public enum XMLTYPE
{
    NEW,
    CARD,
    CARRER,
    STAGE,
    MAP,
    MONSTER
}

public class XMLMaker : EditorWindow
{
    private static EditorWindow window;

    // 新加列
    private string newAttribute = "";

    static string newXMLName = "new.xml";

    static string resPath = "";

    string xmlPath = resPath + "/" + newXMLName;

    [MenuItem("Game/XMLMaker")]
    static void Open()
    {
        resPath = Application.dataPath + "/Resources/XML";

        window = EditorWindow.GetWindow(typeof(XMLMaker));

        window.titleContent.text = "制作XML模版";

        window.autoRepaintOnSceneChange = true;

        window.minSize = new Vector2(600, 500);
        window.maxSize = window.minSize;
        var position = window.position;
        position.center = new Rect(0f, 0f, Screen.currentResolution.width, Screen.currentResolution.height).center;

        window.position = position;
        window.Show();
    }


    private ArrayList textList = new ArrayList();

    private XMLTYPE currentType = XMLTYPE.NEW;

    private void OnGUI()
    {
        // 行间隔
        GUILayout.Space(10);

        currentType = (XMLTYPE)EditorGUILayout.EnumPopup("操作XML类型 ->", currentType);

        GUILayout.Space(20);

        switch (currentType)
        {
            case XMLTYPE.NEW:
                newXMLName = "new.xml";
                newXMLName = EditorGUILayout.TextField("表名 -->: ", newXMLName);
                break;                
            case XMLTYPE.CARD:
                newXMLName = "card_info.xml";
                break;
            case XMLTYPE.CARRER:
                newXMLName = "carrer_info.xml";
                break;
            case XMLTYPE.STAGE:
                newXMLName = "stage_info.xml";
                break;
            case XMLTYPE.MAP:
                newXMLName = "map_info.xml";
                break;
            case XMLTYPE.MONSTER:
                newXMLName = "monster_info.xml";
                break;
            default:
                break;
        }

        xmlPath = resPath + "/" + newXMLName;

        if (!File.Exists(xmlPath))
        {
            GUILayout.Label("不存在该表 需要新建");
            GUILayout.Space(20);
            if (GUILayout.Button("Create"))
            {
                CreateXML();
            }
        }
        else
        {
            GUILayout.Label("已存在该表");
            GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            newAttribute = EditorGUILayout.TextField("添加新列 --> 名称 : ", newAttribute);
            if (GUILayout.Button("Add"))
            {
                AddXml();
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(30);
            if (GUILayout.Button("Delete"))
            {
                DeleteXML();
            }
        }
    }

    void CreateXML()
    {
        if (!File.Exists(xmlPath))
        {
            //创建最上一层节点
            XmlDocument xml = new XmlDocument();

            // 根节点
            XmlElement root = xml.CreateElement("resources");


            XmlElement element1 = xml.CreateElement("info");

            element1.SetAttribute("ID", "10001");

            XmlElement element2 = xml.CreateElement("info");
            element2.SetAttribute("ID", "10002");

            XmlElement element3 = xml.CreateElement("info");
            element3.SetAttribute("ID", "10003");

            root.AppendChild(element1);
            root.AppendChild(element2);
            root.AppendChild(element3);

            xml.AppendChild(root);

            xml.Save(xmlPath);

            Debug.LogError("Create over" + xmlPath);

        }
        else
        {
            Debug.LogError("Existsed!!!");
        }
    }

    void DeleteXML()
    {
        
        if (File.Exists(xmlPath))
        {
            File.Delete(xmlPath);
        }
        else
        {
            Debug.LogError("Null!!!!");
        }
    }

    void LoadXML()
    {
        string xmlPath = Application.dataPath + "/card_info.xml";

        XmlDocument xml = new XmlDocument();

        XmlReaderSettings set = new XmlReaderSettings();
        // 忽略xml注释文档影响
        set.IgnoreComments = true;

        xml.Load(XmlReader.Create(xmlPath, set));

        XmlNodeList xmlNodeList = xml.SelectSingleNode("objects").ChildNodes;

        foreach (XmlElement xl1 in xmlNodeList)
        {
            if (xl1.GetAttribute("id") == "333")
            {
                foreach (XmlElement xl2 in xl1.ChildNodes)
                {
                    textList.Add(xl2.GetAttribute("name") + ":" + xl2.InnerText);

                    if (xl2.GetAttribute("age") == "11")
                    {
                        Debug.LogError(xl2.GetAttribute("age") + ":" + xl2.InnerText);
                    }
                }
            }
        }

        foreach (var item in textList)
        {
            Debug.LogError(item);
        }
    }

    void AddXml()
    {
        if (newAttribute == "" || null == newAttribute)
        {
            Debug.LogError("新列名不可为空!!");
            return;
        }

        if (File.Exists(xmlPath))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);

            XmlNodeList xmlNodeList = xml.SelectSingleNode("resources").ChildNodes;
            foreach (XmlElement xl1 in xmlNodeList)
            {
                string cur = xl1.GetAttribute(newAttribute);
                if (cur == "")
                {
                    xl1.SetAttribute(newAttribute, "1");
                }
                else
                {
                    Debug.LogError("已存在相同列名");
                    return;
                }
            }

            xml.Save(xmlPath);

            window.ShowNotification(new GUIContent("添加新列: " + newAttribute + "成功"));
        }
        else
        {
            Debug.LogError("空路径");
        }
    }
}
