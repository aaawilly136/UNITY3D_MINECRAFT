using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// 切換道具管理器
/// 切換道具:裝備與道具欄
/// 點擊後將道具資訊存放到【選取中的道具】
/// 並判定與另一個欄位切換
/// 切換道具到合成素材區後匹配有沒有對應到合成物件
/// </summary>
public class InventorySwitch : MonoBehaviour
{
    #region 欄位
    [Header("選取中的道具")]
    public Transform traChooseProp;
    [Header("要判斷的畫布:圖像射線碰撞")]
    public GraphicRaycaster Graphic;
    [Header("事件系統:EventSystem")]
    public EventSystem eventSystem;
    [Header("素材1~4")]
    public Item[] itemMerge;
    [Header("合成表")]
    public MergeTable mergeTable;

    /// <summary>
    /// 當前合成區的組合
    /// </summary>
    private GameObject[] goMergeCurrent = new GameObject[4];

    /// <summary>
    /// 事件碰撞座標資訊
    /// </summary>
    private PointerEventData dataPointer;
    /// <summary>
    /// 是否選到道具
    /// </summary>
    private bool chooseItem;
    #endregion

    #region 事件
    private InventoryItem inventoryItemChooseProp;
    private Item itemChooseProp;
    private void Start()
    {
        inventoryItemChooseProp = traChooseProp.GetComponent<InventoryItem>();
        itemChooseProp = traChooseProp.GetComponent<Item>();
    }
    private void Update()
    {
        CheckMousePositionItem();
    }
    #endregion

    #region 方法
    /// <summary>
    /// 檢查滑鼠座標上的道具
    /// </summary>
    private void CheckMousePositionItem()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            #region 檢查滑鼠有沒有碰到介面
            // 取得滑鼠座標
            Vector3 posMouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);


            //新增是件碰撞座標資訊並指定事件系統
            dataPointer = new PointerEventData(eventSystem);
            //指定為滑鼠座標
            dataPointer.position = posMouse;
            //碰撞清單
            List<RaycastResult> result = new List<RaycastResult>();

            //圖形.射線碰撞(碰撞物件資訊,碰撞清單)
            Graphic.Raycast(dataPointer, result);
            #endregion

            #region 點擊後並且有介面就顯示選取中的道具
            //如果碰撞清單數量>零
            if (result.Count > 0)
            {
                Item item = result[0].gameObject.GetComponent<Item>();
                
                #region 判定是否為合成區域的素材,以名稱來判定是否包含素材兩個字
                if(item.gameObject.name.Contains("素材"))
                {
                    UpdateMergeItem(inventoryItemChooseProp, item.GetComponent<InventoryItem>(), itemChooseProp, item);
                    return;
                }
                #endregion

                // 判斷 如果 道具有item並且 不是空值道具欄在做切換道具處理
                if (!chooseItem && item && item.propType != PropType.None)
                {
                    chooseItem = true;                          //已經選到道具
                    traChooseProp.gameObject.SetActive(true);

                    //更新道具(點到的道具.選取中的道具.點到的道具item.選取中的道具 item)
                    //將第一個選到的道具 更新到 選取中的道具上
                    UpdateItem(item.GetComponent<InventoryItem>(), inventoryItemChooseProp, item, itemChooseProp);
                }
                else if (item && chooseItem)
                {
                    chooseItem = false;                         //已經選到道具
                    traChooseProp.gameObject.SetActive(false);
                    UpdateItem(inventoryItemChooseProp, item.GetComponent<InventoryItem>(), itemChooseProp, item);
                }
            }
            #endregion
        }
        #region 選到道具後:選取中的道具跟著滑鼠移動
        if (chooseItem)
        {
            dataPointer.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            traChooseProp.position = dataPointer.position;
        }
        #endregion

    }
    /// <summary>
    /// 更新道具資訊.圖片.數量.道具物件.類型
    /// </summary>
    private void UpdateItem(InventoryItem chooseInventory,InventoryItem updateInventory,Item chooseItem,Item updateItem)
    {
        updateInventory.imgProp.sprite = chooseInventory.imgProp.sprite;    //更新 道具 圖片
        updateInventory.textProp.text = chooseInventory.textProp.text;      //更新 道具數量
        updateInventory.imgProp.enabled = true;                             //啟動 更新 道具 圖片
        chooseInventory.imgProp.enabled = false;                            //關閉選到的道具圖片
        chooseInventory.imgProp.sprite = null;                              //刪除選到的道具圖片
        chooseInventory.textProp.text = "";                                 //刪除選到的道具數量

        // 更新道具資訊 Item 將選種道具的資訊更新 並 刪除原本的
        updateItem.count = chooseItem.count;
        updateItem.goItem = chooseItem.goItem;
        updateItem.propType = chooseItem.propType;
        chooseItem.count = 0;
        chooseItem.goItem = null;
        chooseItem.propType = PropType.None;

        //通知裝備管理器
        EquipmentManager.instance.ShowEquipment();
    }

    private void UpdateMergeItem(InventoryItem chooseInventory, InventoryItem updateInventory, Item chooseItem, Item updateItem)
    {
        if(chooseItem.count>0)
        {
            updateInventory.imgProp.sprite = chooseInventory.imgProp.sprite;    //更新 道具 圖片
            updateInventory.textProp.text = "1";                                //素材區放入一個道具
            updateInventory.imgProp.enabled = true;                             //啟動 更新 道具 圖片

            int chooseCount = chooseItem.count - 1;

            chooseInventory.textProp.text = chooseCount.ToString();
            updateItem.count = 1;
            updateItem.goItem = chooseItem.goItem;
            updateItem.propType = chooseItem.propType;
            chooseItem.count = chooseCount;
        }
        if(chooseItem.count == 0)
        {
            chooseInventory.imgProp.sprite = null;
            chooseInventory.imgProp.enabled = false;
            chooseInventory.textProp.text = "";
            chooseItem.count = 0;
            chooseItem.goItem = null;
            chooseItem.propType = PropType.None;
            traChooseProp.gameObject.SetActive(false);
            this.chooseItem = false;                    //手上沒有道具-this指此類別可以存取類別定義的欄位方法
        }

        CheckMergeData();
        EquipmentManager.instance.ShowEquipment();
    }
    /// <summary>
    /// 檢查目前合成素材在合成表內是否有相同的資料
    /// </summary>
    private void CheckMergeData()
    {
        // 將合成區素材存放到陣列內
        for(int i = 0;i<itemMerge.Length;i++)
        {
            goMergeCurrent[i] = itemMerge[i].goItem;
        }
    }
    #endregion
}
