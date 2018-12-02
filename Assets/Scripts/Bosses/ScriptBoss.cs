using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptBoss : MonoBehaviour {

    public int count;
    public Transform TP;
    public Transform TPAltar;
    [Header("Status do boss")]
    public float Damage;
    public float Life;

    public GameObject disablecollider;
    public GameObject Escada;
    public GameObject simbolo;


    public bool attacking;

    //public CircleCollider2D attackCollider;
    public Slider sliderlife;

    public Vector3 offset;

    PlayerScript player;
    HealthBar HB;
    Magic magic;

    public Transform OrbeEmitor;
    public GameObject OrbePrefab;
    public float OrbeSpeed;
    public Transform OrbeRotator;

    public GameObject PrefabSlime;
    public GameObject[] PosicoesSlime;
    public int countslime;

    bool reverso;
    bool morreu;
    Animator anim;

	// Use this for initialization
	void Start () {
        count = 0;

        countslime = 2;

        Escada.gameObject.SetActive(false);
        simbolo.gameObject.SetActive(false);

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
       if(count > 8)
        {
            count = 0;
        }
        if (Life >= 0)
        {
            sliderlife.value = Life;
        }

        if(Life <= 0)
        {
            morreu = true;
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
    public void iddle1()
    {
        anim.SetTrigger("idle1");
    }
    public void IdleBoss()
    {
        anim.SetTrigger("idle");
        count = count + 1;
        StartCoroutine(ComecarCoroutine(4.5f));
    }
    public void AttackingBoss()
    {
        anim.SetTrigger("attack");
        count = count + 1;
        // attacking = !attacking;
        StartCoroutine(ComecarCoroutine(2f));
    }
    public void SpellBoss()
    {
        anim.SetTrigger("spell");
        for (int i = 0; i < countslime; i++)
        {
            int intposicao = i;
            Instantiate(PrefabSlime, PosicoesSlime[intposicao].transform.position, Quaternion.identity);
        }
        count = count + 1;
        StartCoroutine(ComecarCoroutine(1.5f));
    }

    public void Teletransporte()
    {
        transform.position = TP.transform.position;
        count = count + 1;
        StartCoroutine(ComecarCoroutine(2f));
    }
    public void TeletransporteReverso(Vector3 pos)
    {
        anim.SetTrigger("TeleporteReverso");
        reverso = true;
        StartCoroutine(TPReverso(pos));

        count = count + 1;
        StartCoroutine(ComecarCoroutine(2f));
    }
    public void TeletransporteReversoAltar()
    {
        anim.SetTrigger("TeleporteReversoAltar");
        reverso = false;
        StartCoroutine(TPReversoAltar());

        count = count + 1;
        StartCoroutine(ComecarCoroutine(2f));
    }
    public void Vinhas()
    {
        count = count + 1;
        StartCoroutine(ComecarCoroutine(2f));
    }
    public void ORBE()
    {
        count = count + 1;

        GameObject tempOrbe = Instantiate(OrbePrefab, OrbeEmitor.position, OrbeRotator.rotation);
        Rigidbody2D tempRB2D = tempOrbe.GetComponent<Rigidbody2D>();
        tempRB2D.AddForce(OrbeEmitor.forward * OrbeSpeed);
        tempOrbe.GetComponent<Orbe>().Damage = Damage;
        StartCoroutine(ComecarCoroutine(2f));
    }
   //public void FindPlayer(Vector3 pos)
   //{
   //    
   //    pos += offset;
   //    transform.position = pos;
   //    AttackingBoss();
   //}
   
    public void DeadBoss()
    {
        magic.DisableGemaBloq();
        disablecollider.gameObject.SetActive(false);
        Escada.gameObject.SetActive(true);
        simbolo.gameObject.SetActive(true);

        Destroy(gameObject);
    }


    public IEnumerator TPReversoAltar()
    {
        yield return new WaitForSeconds(0.5f);
    
        if (reverso == false)
        {
            transform.position = TPAltar.transform.position;
        }

    }

    public IEnumerator TPReverso(Vector3 position)
    {
        yield return new WaitForSeconds(0.5f);
        if (reverso == true)
        {
            position += offset;
            transform.position = position;
        }else if(reverso == false)
        {
            transform.position = TPAltar.transform.position;
        }
    
    }
   public IEnumerator ComecarCoroutine(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        if (morreu == false)
        {
            if (count == 0)
            {
                ORBE();
            }
            else if (count == 1)
            {
                SpellBoss();
            }
            else if (count == 2)
            {
                anim.SetTrigger("Teleporte");
            }
            else if (count == 3)
            {
                player.BossPosition();
            }
            else if (count == 4)
            {
                AttackingBoss();
            }
            else if (count == 5)
            {
                IdleBoss();
            }
            else if (count == 6)
            {
                anim.SetTrigger("Teleporte");
            }
            else if (count == 7)
            {
                TeletransporteReversoAltar();
            }
            else if (count == 8)
            {
                Vinhas();
            }
        }
    }

}
