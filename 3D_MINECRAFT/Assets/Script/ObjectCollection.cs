using UnityEngine;

/// <summary>
/// 採集物件:儲存採集物件資料以及功能
/// </summary>
public class ObjectCollection : MonoBehaviour
{
    [Header("採集物件資料")]
    public DataCollection data;
    [Header("採集物爆炸特效")]
    public GameObject objExplosion;
    /// <summary>
    /// 血量
    /// 儲存腳本化物件資料提供物件個別使用
    /// </summary>
    private float hp;
    private void Start()
    {
        hp = data.hp;
    }
    /// <summary>
    /// 受到攻擊傷害
    /// </summary>
    /// <param name="damage"></param>
    public void Hit(float damage)
    {
        hp -= damage;
        if (hp <= 0) Dead();
    }
    /// <summary>
    /// 採集物件毀損死亡並且掉落採集物的碎片
    /// </summary>
    private void Dead()
    {
        Destroy(gameObject);
        Instantiate(data.objDrop, transform.position, Quaternion.Euler(0, 45, 0));
        Instantiate(objExplosion, transform.position, Quaternion.identity);
    }
}
