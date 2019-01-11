using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour {

    // 打开初始进入界面
    public void OpenLoginPage()
    {
        Controller.Instance.state = GAME_STATE.LOGIN;
    }

    // 打开选择界面
    public void OpenSelectPage()
    {
        Controller.Instance.state = GAME_STATE.SELECT;
    }
}
