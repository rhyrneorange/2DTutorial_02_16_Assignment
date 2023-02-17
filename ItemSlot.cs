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
    /// ���Կ� ������ ����
    /// </summary>
    /// <param name="item"></param>
    public void SetSlot(Item item)
    {
        m_item = item;
        item.transform.SetParent(transform); //item�� ItemSlot�� �ڽĿ�����Ʈ�� ����
        item.transform.localPosition = Vector3.zero; //�θ������Ʈ�� ������ �����Ƿ� transform�� �ʱ�ȭ
        item.transform.localRotation = Quaternion.identity;
        item.transform.localScale = Vector3.one;
    }
    /// <summary>
    /// ���� Ŭ�� ��
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