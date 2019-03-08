using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SlayCard;

public class Model : MonoBehaviour {

    public Player player;

    //  玩家自定义名字
    public string Name { get; private set; }

    /// <summary>
    /// 记录玩家自定义名称
    /// </summary>
    /// <param name="_name"></param>
    public void SetName(string _name)
    {
        Name = _name;

        PlayerPrefs.SetString("PlayerName", Name);
    }


    /// <summary>
    /// 创建玩家角色
    /// </summary>
    /// <param name="职业index"></param>
    public void InitPlayer(int _carrer)
    {
        player = new Player(_carrer);
    }

    public Player GetPlayer()
    {
        return player;
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
