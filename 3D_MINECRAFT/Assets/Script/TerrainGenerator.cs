using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    #region 欄位
    /// <summary>
    /// 長
    /// </summary>
    [Header("地形大小:長")]
    public int x;
    /// <summary>
    /// 寬
    /// </summary>
    [Header("地形大小:寬")]
    public int z;
    [Header("地形高")]
    public float height;
    [Header("地形細節 波度的起伏"),Tooltip("數字越大起伏越低、數字1為沒有起伏"),Range(1,100)]
    public float detial;
    [Header("地形物件的高度")]
    public float heightTerrain = 0.8f;
    //陣列
    //語法: 類型後加上中括號
    //用途: 存放相同類型的多筆資料
    //陣列內的資料都有編號、並且從0開始
    /// <summary>
    /// 用於存放地形物件、按順序為:0草地、1泥土、2石頭
    /// </summary>
    [Header("地形物件:0草地、1泥土、2石頭")]
    public GameObject[] objTerrains;
    [Header("玩家")]
    public Transform traPlayer;
    [Header("泥土範圍")]
    public Vector2 v2Dirty = new Vector2(1, 6);


    private Transform traTerrainGroup;
    private int randomTerrain;
    #endregion
    #region 事件
    private void Start()
    {
        traTerrainGroup = GameObject.Find("地形群組").transform;
        randomTerrain = Random.Range(1, 10000);  //取得隨機地形數值
        Generat();
        GeneratePlayer();
    }
    #endregion
    #region 方法
    /// <summary>
    /// 生成地形
    /// </summary>
    private void Generat()
    {
        // 取得陣列資料語法:陣列名稱[編號]
        for(int posX = 0; posX < x; posX++)
        {
            for(int posZ = 0; posZ < z; posZ++)
            {
                // 浮點數轉整數資料 - (int)浮點數資料
                int posY = (int)(Mathf.PerlinNoise(posX / detial + randomTerrain , posZ / detial + randomTerrain) * height);
                Vector3 pos = new Vector3(posX, posY * heightTerrain, posZ);
                //生成(物件、座標、角度、副物件)
                Instantiate(objTerrains[0], pos, Quaternion.identity, traTerrainGroup);  // 生成(物件、座標、角度、副物件)
                #region 第一層底下的地形物件處理: 泥土與石頭
                for (int y =0; y<posY;y++)  //第一層底下、從高度0開始
                {
                    int rDirty = (int)Random.Range(v2Dirty.x, v2Dirty.y); //隨機泥土

                    if(y>= posY - rDirty)  //第一層下方先顯示泥土再顯示石頭
                    {
                        Vector3 posDirty = new Vector3(posX, y * heightTerrain, posZ);
                        Instantiate(objTerrains[1], posDirty, Quaternion.identity, traTerrainGroup);
                    }
                    else
                    {
                        Vector3 posDirty = new Vector3(posX, y * heightTerrain, posZ);
                        Instantiate(objTerrains[2], posDirty, Quaternion.identity, traTerrainGroup);
                    }
                }
                #endregion
            }
        }
            

    }
    private void GeneratePlayer()
    {
        Vector3 pos = new Vector3(x / 2, height + 3, z / 2);
        Instantiate(traPlayer, pos, Quaternion.identity);
    }
    #endregion
}
