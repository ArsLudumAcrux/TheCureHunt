using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptBoss : MonoBehaviour {

    public bool IdleBool;
    public bool AttackBool;
    public bool SpellBool;
    public bool IdleBool2;



    public float Damage;
    public float Life;


    public bool attacking;

    //public CircleCollider2D attackCollider;
    public Slider sliderlife;

    public Vector3 offset;

    PlayerScript player;
    HealthBar HB;
    Magic magic;



    public GameObject PrefabSlime;
    public GameObject[] PosicoesSlime;
    public int countslime;


    Animator anim;

	// Use this for initialization
	void Start () {
        IdleBool = false;
        IdleBool2 = false;
        AttackBool = true;
        SpellBool = false;

        countslime = 2;
       

        offset = new Vector3(0, 3f);
        anim = gameObject.GetComponent<Animator>();
       // attacking = false;
       // attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
       // attackCollider.enabled = false;
        sliderlife.maxValue = Life;
        sliderlife.value = Life;

        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerScript>();
        HB = GameObject.FindGameObjectWithTag("Content").gameObject.GetComponent<HealthBar>();
        magic = GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<Magic>();

    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("Attack");

       //if (attacking == true)
       //{
       //
       //    float PlaybackTime = stateInfo.normalizedTime;
       //    if (PlaybackTime > 0.2f && PlaybackTime < 0.7f)
       //    {
       //
       //        attackCollider.enabled = true;
       //    }
       //    else
       //    {
       //        attackCollider.enabled = false;
       //
       //    }
       //}
        if (Life >= 0)
        {
            sliderlife.value = Life;
        }

        if(Life <= 0)
        {
            anim.SetTrigger("die");
        }
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HB.HP_Current -= Mathf.RoundToInt(Damage * player.ShieldPotionMult);
        }
    }

    public void IddleBoss2()
    {
        anim.SetTrigger("idle");
        IdleBool = false;
        IdleBool2 = false;
        AttackBool = true;
        SpellBool = false;
        StartCoroutine(ComecarCoroutine());
    }
    public void IddleBoss()
    {
        anim.SetTrigger("idle");
        IdleBool = false;
        IdleBool2 = false;
        AttackBool = false;
        SpellBool = true;
        StartCoroutine(ComecarCoroutine());
    }
    public void AttackingBoss()
    {
        anim.SetTrigger("attack");
        IdleBool = true;
        IdleBool2 = false;
        AttackBool = false;
        SpellBool = false;
       // attacking = !attacking;
        StartCoroutine(ComecarCoroutine());
    }
    public void SpellBoss()
    {
        anim.SetTrigger("spell");
        for (int i = 0; i < countslime; i++)
        {
            int intposicao = i;
            Instantiate(PrefabSlime, PosicoesSlime[intposicao].transform.position, Quaternion.identity);
        }
        IdleBool = false;
        IdleBool2 = true;
        AttackBool = false;
        SpellBool = false;
        StartCoroutine(ComecarCoroutine());
    }


    public void FindPlayer(Vector3 pos)
    {
        
        pos += offset;
        transform.position = pos;
        AttackingBoss();
    }
   
    public void DeadBoss()
    {
        magic.DisableGemaBloq();
        Destroy(gameObject);
    }

   public IEnumerator ComecarCoroutine()
    {
        yield return new WaitForSeconds(4.5f);
        if(AttackBool == true)
        {
            player.BossPosition();
        }
        else if(IdleBool == true)
        {
            IddleBoss();
        }
        else if (IdleBool2 == true)
        {
            IddleBoss2();
        }
        else if(SpellBool == true)
        {
            SpellBoss();
        }
                   
    }

}
