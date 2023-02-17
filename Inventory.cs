using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] UILabel m_titleCountLabel; //TODO:����
    //[SerializeField] UIScrollView m_scrollView;

    [SerializeField] GameObject m_itemSlotPrefab;
    [SerializeField] GameObject m_itemPrefab;
    [SerializeField] UIGrid m_itemSlotGrid;
    [SerializeField] UISprite m_cursorSpr;
    List<ItemSlot> m_itemSlotList = new List<ItemSlot>();
    int m_maxSlot = 24;
    [SerializeField] Sprite[] m_iconSprites;
    [SerializeField] List<ItemData> m_itemDataList = new List<ItemData>();
    Dictionary<ItemType, ItemData> m_itemTable = new Dictionary<ItemType, ItemData>();

    public int TitleCount = 0; //TODO:����
    /// <summary>
    /// ������ ���� UI ����
    /// </summary>
    public void SetTitleCount() //TODO:����
    {
        /*int count = 0;
        for(int i=0; i<m_itemSlotList.Count; i++)
        {
            if (!m_itemSlotList[i].IsEmpty) count++;
        }*/
        m_titleCountLabel.text = string.Format("{0}/{1}", TitleCount, m_itemSlotList.Count);
    }
    /// <summary>
    /// Ȯ���ϱ� ��ư ������ ��
    /// </summary>
    public void OnPressExpand() //TODO:����
    {
        for(int i=0; i<6; i++)
        {
            CreateSlot();
        }
        m_itemSlotGrid.Reposition(); //Grid ������
        //m_scrollView.ResetPosition();
        SetTitleCount();
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
    }
    public void HideUI()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// ������ ���� ��ư Ŭ�� ��
    /// </summary>
    public void CreateItem() //������ ����
    {
        for (int i = 0; i < m_itemSlotList.Count; i++)
        {
            if (m_itemSlotList[i].IsEmpty)
            {
                var item = Instantiate(m_itemPrefab).GetComponent<Item>();
                var type = (ItemType)Random.Range((int)ItemType.Ball, (int)ItemType.Max); //������ value��
                var data = GetItemData(type);
                ItemInfo itemInfo = new ItemInfo() { itemData = data, count = Random.Range(1, 100) };

                item.SetItem(itemInfo, GetItemIcon(type));
                item.InitItem(this); //TODO:����
                m_itemSlotList[i].SetSlot(item);
                break;
            }
        }
        TitleCount++;
        SetTitleCount();
    }
    /// <summary>
    /// ���� Ŭ�� ��
    /// </summary>
    /// <param name="slot"></param>
    public void OnSelectSlot(ItemSlot slot) //���õ� ����
    {
        for (int i = 0; i < m_itemSlotList.Count; i++) //����Ʈ�� ���鼭
        {
            if (m_itemSlotList[i].m_isSelected) //������ ���õ� ������ ���� ���¸� ����
            {
                m_itemSlotList[i].m_isSelected = false;
                break; //���� �� �� �ݺ��� �ʿ䰡 �����Ƿ� �������´�
            }
        }
        slot.m_isSelected = true; //������ ���Կ� Ŀ�� ����
        if (!m_cursorSpr.enabled)
            m_cursorSpr.enabled = true;
        m_cursorSpr.transform.position = slot.transform.position;
    }
    /// <summary>
    /// ������ ������ ����
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    Sprite GetItemIcon(ItemType type)
    {
        return m_iconSprites[(int)type];
    }
    void SetInventroey()
    {
        for (int i = 0; i < m_maxSlot; i++)
        {
            CreateSlot();
        }
        m_itemSlotGrid.Reposition(); //Grid ������
    }
    void CreateSlot()
    {
        var obj = Instantiate(m_itemSlotPrefab);
        obj.transform.SetParent(m_itemSlotGrid.transform); //Grid�� �ڽĿ�����Ʈ�� ����
        obj.transform.localPosition = Vector3.zero; //�θ������Ʈ�� ������ �����Ƿ� transform�� �ʱ�ȭ
        obj.transform.localScale = Vector3.one;
        obj.transform.localRotation = Quaternion.identity;

        var slot = obj.GetComponent<ItemSlot>();
        slot.InitSlot(this);
        m_itemSlotList.Add(slot);
    }
    /// <summary>
    /// ��� ��ư Ʋ�� ��
    /// </summary>
    public void OnPressItemUse()
    {
        var find = m_itemSlotList.Find(slot => slot.m_isSelected); //Predicate�� delegate => ����
        if (find != null)
        {
            find.UseItem();
        }
    }
    void InitItemTable()
    {
        for (int i = 0; i < m_itemDataList.Count; i++)
        {
            m_itemTable.Add(m_itemDataList[i].type, m_itemDataList[i]);
        }
    }
    ItemData GetItemData(ItemType type)
    {
        return m_itemTable[type];
    }

    void Start()
    {
        SetInventroey();
        InitItemTable();
        m_cursorSpr.enabled = false;

        SetTitleCount();
        HideUI();
    }
}
