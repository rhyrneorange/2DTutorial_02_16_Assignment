using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] UILabel m_titleCountLabel; //TODO:과제
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

    public int TitleCount = 0; //TODO:과제
    /// <summary>
    /// 아이템 개수 UI 갱신
    /// </summary>
    public void SetTitleCount() //TODO:과제
    {
        /*int count = 0;
        for(int i=0; i<m_itemSlotList.Count; i++)
        {
            if (!m_itemSlotList[i].IsEmpty) count++;
        }*/
        m_titleCountLabel.text = string.Format("{0}/{1}", TitleCount, m_itemSlotList.Count);
    }
    /// <summary>
    /// 확장하기 버튼 눌렀을 때
    /// </summary>
    public void OnPressExpand() //TODO:과제
    {
        for(int i=0; i<6; i++)
        {
            CreateSlot();
        }
        m_itemSlotGrid.Reposition(); //Grid 재정렬
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
    /// 아이템 생성 버튼 클릭 시
    /// </summary>
    public void CreateItem() //아이템 생성
    {
        for (int i = 0; i < m_itemSlotList.Count; i++)
        {
            if (m_itemSlotList[i].IsEmpty)
            {
                var item = Instantiate(m_itemPrefab).GetComponent<Item>();
                var type = (ItemType)Random.Range((int)ItemType.Ball, (int)ItemType.Max); //무작위 value값
                var data = GetItemData(type);
                ItemInfo itemInfo = new ItemInfo() { itemData = data, count = Random.Range(1, 100) };

                item.SetItem(itemInfo, GetItemIcon(type));
                item.InitItem(this); //TODO:과제
                m_itemSlotList[i].SetSlot(item);
                break;
            }
        }
        TitleCount++;
        SetTitleCount();
    }
    /// <summary>
    /// 슬록 클릭 시
    /// </summary>
    /// <param name="slot"></param>
    public void OnSelectSlot(ItemSlot slot) //선택된 슬롯
    {
        for (int i = 0; i < m_itemSlotList.Count; i++) //리스트를 돌면서
        {
            if (m_itemSlotList[i].m_isSelected) //이전에 선택된 슬롯의 선택 상태를 해제
            {
                m_itemSlotList[i].m_isSelected = false;
                break; //해제 후 더 반복할 필요가 없으므로 빠져나온다
            }
        }
        slot.m_isSelected = true; //선택한 슬롯에 커서 생성
        if (!m_cursorSpr.enabled)
            m_cursorSpr.enabled = true;
        m_cursorSpr.transform.position = slot.transform.position;
    }
    /// <summary>
    /// 아이템 아이콘 설정
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
        m_itemSlotGrid.Reposition(); //Grid 재정렬
    }
    void CreateSlot()
    {
        var obj = Instantiate(m_itemSlotPrefab);
        obj.transform.SetParent(m_itemSlotGrid.transform); //Grid의 자식오브젝트로 생성
        obj.transform.localPosition = Vector3.zero; //부모오브젝트의 영향을 받으므로 transform을 초기화
        obj.transform.localScale = Vector3.one;
        obj.transform.localRotation = Quaternion.identity;

        var slot = obj.GetComponent<ItemSlot>();
        slot.InitSlot(this);
        m_itemSlotList.Add(slot);
    }
    /// <summary>
    /// 사용 버튼 틀릭 시
    /// </summary>
    public void OnPressItemUse()
    {
        var find = m_itemSlotList.Find(slot => slot.m_isSelected); //Predicate이 delegate => 람다
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
