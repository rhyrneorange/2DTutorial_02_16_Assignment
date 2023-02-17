using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    /*string m_id = "아이디를 입력하세요";
    string m_pass = string.Empty;
    bool m_isOpen;
    bool m_isShow;
    int m_select;
    float m_height = 20;
    string[] m_weaponList = new string[]
    {
        "단검", "양손검", "바스타드", "양날도끼", "할버드", "화룡의 대검", "엑스칼리버", "헤비머신건", "로켓런처"
    };
    void OnGUI()
    {
        if(GUI.Button(new Rect((Screen.width - 200) / 2, (Screen.height - 100) / 2, 200, 100), "Start"))
        {
            Debug.Log("Push Button");
        }
        GUILayout.BeginArea(new Rect(10, Screen.height - 400, 200, 400),GUI.skin.box);
        GUILayout.Space(20);
        GUILayout.Button("Start");
        m_isOpen = GUILayout.Toggle(m_isOpen, "무적모드");
        if (m_isOpen)
            GUILayout.TextArea("무적모드가 활성화되었습니다");
        GUILayout.Label("ID:");
        m_id = GUILayout.TextField(m_id);
        GUILayout.Label("PASS:");
        m_pass = GUILayout.PasswordField(m_pass, '*');
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width - 10 - 200, Screen.height - m_height, 200, 400), GUI.skin.box);
        m_isShow = GUILayout.Toggle(m_isShow, "무기선택", GUI.skin.button);
        if (m_isShow)
        {
            m_height = 400;
            m_select = GUILayout.SelectionGrid(m_select, m_weaponList, 1);
        }
        else
        {
            m_height = 20;
        }    
        GUILayout.EndArea();
    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
