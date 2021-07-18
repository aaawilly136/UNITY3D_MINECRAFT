using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 道具欄以及裝備道具欄的每一格項目
/// </summary>
public class InventoryItem : MonoBehaviour
{
    /// <summary>
    /// 是否有道具
    /// </summary>
    [HideInInspector] //將public 公開的欄位隱藏
    public bool hasPro;
    /// <summary>
    /// 道具圖示
    /// </summary>
    private Image imgProp;
    
    /// <summary>
    /// 道具數量
    /// </summary>
    private Text textProp;
    private void Start()
    {
        imgProp = transform.Find("道具圖示").GetComponent<Image>();
        textProp = transform.Find("道具數量").GetComponent<Text>();
    }
}
