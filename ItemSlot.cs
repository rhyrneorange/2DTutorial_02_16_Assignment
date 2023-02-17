using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    Item m_item;
    Inventory m_inventory;
    public bool m_isSelected;
    public bool IsEmpty { get { return m_item == null; } }

    public void InitSlot(Inventory inventory)
    {
        m_inventory = inventory;
    }
    /// <summary>
    /// 슬롯에 아이템 지정
    /// </summary>
    /// <param name="item"></param>
    public void SetSlot(Item item)
    {
        m_item = item;
        item.transform.SetParent(transform); //item을 ItemSlot의 자식오브젝트로 생성
        item.transform.localPosition = Vector3.zero; //부모오브젝트의 영향을 받으므로 transform을 초기화
        item.transform.localRotation = Quaternion.identity;
        item.transform.localScale = Vector3.one;
    }
    /// <summary>
    /// 슬롯 클릭 시
    /// </summary>
    public void OnSelect()
    {
        m_inventory.OnSelectSlot(this);
        m_isSelected = true;
    }
    public void UseItem()
    {
        if (IsEmpty) return;
        m_item.OnUse();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
}