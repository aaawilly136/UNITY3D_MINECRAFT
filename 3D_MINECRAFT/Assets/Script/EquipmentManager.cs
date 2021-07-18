using UnityEngine;

/// <summary>
/// 裝備管理器: 可以使用滾輪或案件切換裝備
/// 以及使用裝備系統
/// </summary>
public class EquipmentManager : MonoBehaviour
{
    /// <summary>
    /// 當前的裝備的編號:0~4 (共五個欄位)
    /// </summary>
    private int indexEquipment;
    private void Update()
    {
        SwitchEquiment();
    }
    /// <summary>
    /// 切換裝備
    /// 透過滾輪切換
    /// </summary>
    private void SwitchEquiment()
    {
        float wheel =(Input.GetAxis("Mouse ScrollWheel")); //抓到滑鼠滾輪的值
        if (wheel<-0.1f)                                   //如果 往後捲
        {
            indexEquipment++;                              //編號遞增
            if (indexEquipment == 5) indexEquipment = 0;   //如果編號超出範圍就回到零
            print("裝備編號:" + indexEquipment);
        }
    }
}
