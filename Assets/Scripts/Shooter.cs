using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;//弾オブジェクト
    public float shotSpeed;//600くらい

    public BulletHolder bulletHolder;
    int reloadBulletAmount;//リロード弾数

    AudioSource audioSource;
    public AudioClip[] sounds;//[0]ショット音,[1]リロード音,[2]弾切れ

    PlayerController playerController;

    public GameObject sighter;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが行動不可なら撃てなくする
        if (playerController.GetTouchEnemy() == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Return))
        {
            Reload();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (sighter.activeSelf == true)
            {
                sighter.SetActive(false);
            }
            else
            {
                sighter.SetActive(true);
            }
        }
    }

    void Shot()
    {
        //弾がなければ撃てない
        if (bulletHolder.GetBulletAmount() <= 0)
        {
            audioSource.PlayOneShot(sounds[2]);
            return;
        }

        //弾の生成
        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.identity
            );

        //生成したbulletの親をBulletHolderに設定
        bullet.transform.parent = bulletHolder.transform;

        //弾の発射
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(transform.forward * shotSpeed);
        audioSource.PlayOneShot(sounds[0]);

        bulletHolder.ConsumeBullet();//弾の消費

        Destroy(bullet, 5f);//指定時間後に弾消去
    }

    void Reload()
    {
        reloadBulletAmount = bulletHolder.GetDefalutBulletAmount() - bulletHolder.GetBulletAmount();
        bulletHolder.AddBullet(reloadBulletAmount);
        audioSource.PlayOneShot(sounds[1]);
    }
}
