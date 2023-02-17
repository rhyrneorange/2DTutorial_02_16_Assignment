using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    [SerializeField] UI2DSprite m_icon;
    [SerializeField] UILabel m_countLabel;
    ItemInfo m_itemInfo;
    Inventory m_inventory;

    public void InitItem(Inventory inventory) //TODO:과제
    {
        m_inventory = inventory;
    }

    public void SetItem(ItemInfo itemInfo, Sprite icon)
    {
        m_itemInfo = itemInfo;
        m_countLabel.text = m_itemInfo.count.ToString();
        m_icon.sprite2D = icon;
    }

    public void OnUse()
    {
        m_itemInfo.count--;
        m_countLabel.text = m_itemInfo.count.ToString();
        if (m_itemInfo.count <= 0)
        {
            m_inventory.TitleCount--; //TODO:과제
            m_inventory.SetTitleCount(); //TODO:과제
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }
}