using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

public class ReadXML {

    public static Dictionary<string, string> xmlDic;

    /// <summary>
    /// 加载xml 表 获取resources 下的 属性Nodelist
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static XmlDocument LoadXML(string path)
    {
        if (File.Exists(path))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(XmlReader.Create(path));

            return xml;
        }
        else
        {
            Debug.LogError("error xml path");
            return null;
        }
    }

    /// <summary>
    ///  获取XML 中的属性列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<T> GetInfoList<T>(string path) where T : XMLInfo, new() {
        XmlDocument xml = LoadXML(path);
        if (xml == null)
        {
            Debug.LogError("error xml path");
            return null;
        }
        else
        {
            List<T> infoList = new List<T>();

            XmlNodeList xmlNodeList = xml.SelectSingleNode("resources").ChildNodes;
            foreach (XmlElement item in xmlNodeList)
            {
                T temp = new T();

                temp.GetValue(item);

                infoList.Add(temp);
            }
            return infoList;
        }
    }

    /// <summary>
    ///  获取XML 中的属性字典 ID为key T为value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Dictionary<string, T> GetCarrerInfo<T>(string path) where T: XMLInfo, new()
    {
        XmlDocument xml = LoadXML(path);
        if (xml == null)
        {
            Debug.LogError("error xml path");
            return null;
        }
        else
        {
            //TestXML aaa = DESerializer<TestXML>(xml.InnerXml);

            Dictionary<string, T> infoDic = new Dictionary<string, T>();

            XmlNodeList xmlNodeList = xml.SelectSingleNode("resources").ChildNodes;
            foreach (XmlElement item in xmlNodeList)
            {
                T temp = new T();
                temp.GetValue(item);

                infoDic.Add(temp.ID, temp);
            }
            return infoDic;
        }
    }



    public static T DESerializer<T>(string strXML) where T : class
    {
        try
        {
            using (StringReader sr = new StringReader(strXML))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(sr) as T;
            }
        }
        catch (System.Exception ex)
        {
            return null;
        }

    }

}
