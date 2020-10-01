using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //プレイヤーに近づいたら走る！
    //弾に当たった時とプレイヤーに当たった時の反応を用意する！

    Animator animator;
    AudioSource source;
    public AudioClip[] sounds;//[0]生成時,[1]走る時,[2]プレイヤー接触時,[3]やられた時

    GameObject player;//距離を計算するターゲット(EnemyGeneratorにて設定)
    GameManager gameManager;//(Enemygeneratorにて設定)

    EnemyGenerator enemyGenerator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        enemyGenerator = GetComponentInParent<EnemyGenerator>();
        source.PlayOneShot(sounds[0]);
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが行動不可 or clear条件達成したならば停止する
        if (player.GetComponent<PlayerController>().GetTouchEnemy() == true || enemyGenerator.GetAttackEnemyCount() >= gameManager.GetClearScore())
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            return;
        }

        //プレイヤーに近づいたら走りだす
        Vector3 offset = player.transform.position - transform.position;
        float distance = offset.sqrMagnitude;
        //Debug.Log(distance);//数値確認用
        if (distance < 2000f)
        {
            animator.SetTrigger("run");
            source.PlayOneShot(sounds[1]);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //弾が当たった時
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(GetComponent<CapsuleCollider>());
            enemyGenerator.AddAttackEnemyCount();
            animator.SetTrigger("Falling");
            source.PlayOneShot(sounds[3]);
            Destroy(this.gameObject, 1f);
        }

        //プレイヤーと接触時
        if (collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("attack");
            source.PlayOneShot(sounds[2]);
            Invoke("Stan", 1f);
        }
    }

    void Stan()
    {
        player.GetComponent<PlayerController>().SetIsStan();
        enemyGenerator.gameObject.SetActive(false);
    }

    //EnemyGeneratorでInstansiateした時にSceneのplayerをセットする用
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
    //EnemyGeneratorでInstansiateした時にSceneのGameManagerをセットする用
    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
