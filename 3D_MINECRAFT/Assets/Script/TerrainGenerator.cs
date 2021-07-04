using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    #region 欄位
    [Header("地形大小")]
    public int x;
    public int y;
    //陣列
    //語法: 類型後加上中括號
    //用途: 存放相同類型的多筆資料
    //陣列內的資料都有編號、並且從0開始
    /// <summary>
    /// 用於存放地形物件、按順序為:0草地、1泥土、2石頭
    /// </summary>
    [Header("地形物件:草地、泥土、石頭")]
    public GameObject[] objTerrains;
    private Transform traTerrainGroup;
    #endregion
    #region 事件
    private void Start()
    {
        traTerrainGroup = GameObject.Find("地形群組").transform;
    }
    #endregion
    #region 方法
    private void Generat()
    {

    }
    #endregion
}
