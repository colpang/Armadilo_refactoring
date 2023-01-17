using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 보스 음파발사 탄환(5번 탄환) 클래스
/// </summary>
public class SoundWave : MonoBehaviour
{
    //Object Pooling을 위한 Bullet LIst
    List<GameObject> BulletList;
    //Bullet prefab
    [SerializeField]
    GameObject Bullet_SoundWave;
    Vector2 originPos;
    Vector2 shootVec;
    Vector2 mousePos;
    [SerializeField]
    float speed;

    private void Awake()
    {
        originPos = transform.position;
    }
    private void Start()
    {
        BulletList = new List<GameObject>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            BulletFire();
        }
    }

    /// <summary>
    /// 음파 발사 함수
    /// </summary>
    void BulletFire()
    {
        Debug.Log(mousePos);
        GameObject bullet = GenerateBullet();
        shootVec = mousePos - originPos;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.velocity = shootVec.normalized * speed;
    }

    /// <summary>
    /// 음파 생성 함수
    /// 최초 탄환 발사 시에만 음파를 Instantiate하고 이후에는 Pool에서 꺼내서 재사용.
    /// </summary>
    GameObject GenerateBullet()
    {
        //최초 1회 Bullet Generate, Speaker 오브젝트의 자식 오브젝트로 설정.
        if (BulletList.Count> 0)
        {
            BulletList[0].SetActive(true);
            BulletList[0].transform.localPosition = Vector2.zero;
            return BulletList[0];

        }
        else
        {
            GameObject bullet = GameObject.Instantiate<GameObject>(Bullet_SoundWave);
            bullet.transform.localPosition = Vector2.zero;
            bullet.transform.SetParent(gameObject.transform);
            BulletList.Add(bullet);
            return bullet;

        }
    }
}
