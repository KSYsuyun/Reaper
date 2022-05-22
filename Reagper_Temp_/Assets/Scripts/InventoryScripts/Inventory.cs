using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // 플레이어가 가진 Item의 정보를 보여주고,
    // 정보를 다른 스크립트에서 받아쓸 수 있도록 관리하는 곳.

    public List<Item> item;
    public List<GameObject> item_Object;
    public GameObject preItem; //인벤토리에 담겨져있던 이전 아이템의 종류

    private int slot_size=1;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddItem( GameObject _itemObject, Item _item)
    {

        if (item.Count < slot_size)
        {
            //만약 아무 것도 안들어있다면..
            item.Add(_item);
            item_Object.Add(_itemObject);

            Debug.Log(_itemObject.name + " 넣음");

            return false; //인벤토리에 처음부터 아무것도 없었기에.. 이전 아이템이 없는 경우..
        }
        else
        {
            //가득 찼을 때, 안의 아이템을 버리고 새로운 아이템을 가지고 온다.
            preItem = item_Object[0];

            item.Remove(item[0]);//기존의 아이템 없앰
            item_Object.Remove(item_Object[0]);

            item.Add(_item);
            item_Object.Add(_itemObject);

            Debug.Log(preItem + " 빼고 " + _itemObject.name + " 넣음");

            return true; //인벤토리에 이전 아이템이 있어서.. 버려야 할 경우..
        }
            
    }
}
