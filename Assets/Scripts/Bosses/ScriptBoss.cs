using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptBoss : MonoBehaviour {

    public int count;
    public int countorbe;
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
    public bool morreu;
    Animator anim;
    CoolDown cooldown;

    public GameObject[] VinhasDireita;
    public GameObject[] VinhasEsquerda;

	// Use this for initialization
	void Start () {
        cooldown = GameObject.FindGameObjectWithTag("CoolDown").GetComponent<CoolDown>();


        for (int i = 0; i < VinhasDireita.Length; i++) // Desativar os gameobject das vinhas
        {
            VinhasDireita[i].SetActive(false);
        }
        for (int i = 0; i < VinhasEsquerda.Length; i++) // Desativar os gameobject das vinhas
        {
            VinhasEsquerda[i].SetActive(false);
        }



        count = 0;

        countslime = 2;

        Escada.gameObject.SetActive(false); // Desativar Escada
        simbolo.gameObject.SetActive(false); // Desativar Simbolo do altar

        offset = new Vector3(0, 3f); // Colocar o chefe um pouco para cima quando ele teleportar
        anim = gameObject.GetComponent<Animator>();
       // attacking = false;
       // attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
       // attackCollider.enabled = false;
        sliderlife.maxValue = Life; // Fazer o slider virar a vida do chefe
        sliderlife.value = Life; // Fazer o slider virar a vida do chefe

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
       if(count > 8) // Assim que a contagem chegar a 8, zera 
        {
            count = 0;
        }
        if (Life >= 0)
        {
            sliderlife.value = Life;
        }

        if(Life <= 0) // Se a vida dele chegar a 0, ele morre
        {
            morreu = true;
            anim.SetTrigger("die");
        }
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && cooldown.EscudosRestante > 0)  // Se o chefe atingir o player e o mesmo estiver com a magia de escudo ativa, ele nao recebera dano
        {
            cooldown.Escudo();
        }
        else if (collision.CompareTag("Player")) // Se nao tiver ativa, ele recebera dano
        {
            HB.HP_Current -= Mathf.RoundToInt(Damage * player.ShieldPotionMult);
        }
    }
    public void iddle1() // Animacao de parado
    {
        anim.SetTrigger("idle1");
    }
    public void IdleBoss() // Animacao de parado
    {
        anim.SetTrigger("idle");
        count = count + 1;
        StartCoroutine(ComecarCoroutine(4.5f));
    }
    public void AttackingBoss() // Animacao de ataque
    {
        anim.SetTrigger("attack");
        count = count + 1;
        // attacking = !attacking;
        StartCoroutine(ComecarCoroutine(2f));
    }
    public void SpellBoss() // Animacao de "Usar magia"
    {
        anim.SetTrigger("spell");
        for (int i = 0; i < countslime; i++) // Invocar 2 slimes no mapa do chefe
        {
            int intposicao = i;
            Instantiate(PrefabSlime, PosicoesSlime[intposicao].transform.position, Quaternion.identity);
        }
        count = count + 1;
        StartCoroutine(ComecarCoroutine(1.5f));
    }

    public void Teletransporte() // Fazer o chefe teleportar para fora do mapa
    {
        transform.position = TP.transform.position;
        count = count + 1;
        StartCoroutine(ComecarCoroutine(2f));
    }
    public void TeletransporteReverso(Vector3 pos) // Fazer o chefe teleportar para a posicao do player, só que um pouco para cima
    {
        anim.SetTrigger("TeleporteReverso");
        reverso = true;
        StartCoroutine(TPReverso(pos));

        count = count + 1;
        StartCoroutine(ComecarCoroutine(2f));
    }
    public void TeletransporteReversoAltar() // Fazer o chefe teleportar para o altar novamente para usar ataque la
    {
        anim.SetTrigger("TeleporteReversoAltar");
        reverso = false;
        StartCoroutine(TPReversoAltar());

        count = count + 1;
        StartCoroutine(ComecarCoroutine(2f));
    }
    public void Vinhas() // Animacao das vinhas, que é o ataque especial do chefe
    {
        anim.SetTrigger("spell");
        count = count + 1;
        Invoke("VinhasEsquerdaAtaque1x", 1f);
    }
    public void VinhasDireitaAtaque1x() // Animacao das vinhas da direita
    {
        anim.SetTrigger("spell");
        for (int i = 0; i < VinhasDireita.Length; i++)
        {
            VinhasDireita[i].SetActive(true);
        }
        Invoke("VinhasDireitaDisable", 3.55f);
        Invoke("VinhasEsquerdaAtaque2x", 3.55f);
    }
    public void VinhasDireitaAtaque2x() // Animacao das vinhas da direita
    {
        anim.SetTrigger("spell");
        for (int i = 0; i < VinhasDireita.Length; i++)
        {
            VinhasDireita[i].SetActive(true);
        }
        Invoke("VinhasDireitaDisable", 3.55f);
        StartCoroutine(ComecarCoroutine(4.2f));
    }
    public void VinhasDireitaDisable() // Desabilitar as vinhas da direita
    {
        for (int i = 0; i < VinhasDireita.Length; i++)
        {
            VinhasDireita[i].SetActive(false);
        }
    }
    public void VinhasEsquerdaAtaque1x() // Animacao das vinhas da esquerda
    {
        anim.SetTrigger("spell");
        for (int i = 0; i < VinhasEsquerda.Length; i++)
        {
            VinhasEsquerda[i].SetActive(true);
        }
        Invoke("VinhasEsquerdaDisable", 3.55f);
        Invoke("VinhasDireitaAtaque1x", 3.55f);
    }
    public void VinhasEsquerdaAtaque2x() // Animacao das vinhas da esquerda
    {
        anim.SetTrigger("spell");
        for (int i = 0; i < VinhasEsquerda.Length; i++)
        {
            VinhasEsquerda[i].SetActive(true);
        }
        Invoke("VinhasEsquerdaDisable", 3.55f);
        Invoke("VinhasDireitaAtaque2x", 3.55f);
    }
    public void VinhasEsquerdaDisable() // Desativar as vinhas da esquerda
    {
        for (int i = 0; i < VinhasEsquerda.Length; i++)
        {
            VinhasEsquerda[i].SetActive(false);
        }
    }
    public void ORBE() // Ataque Orbe
    {
        count = count + 1;
        countorbe = countorbe + 1; // A cada ataque de orbe, acrescenta um ataque ao chefe, no maximo 3

        if (countorbe == 1)
        {
            anim.SetTrigger("spell");
            GameObject tempOrbe = Instantiate(OrbePrefab, OrbeEmitor.position, OrbeRotator.rotation);
            Rigidbody2D tempRB2D = tempOrbe.GetComponent<Rigidbody2D>();
            tempRB2D.AddForce(OrbeEmitor.forward * OrbeSpeed);
            tempOrbe.GetComponent<Orbe>().Damage = Damage;
            StartCoroutine(ComecarCoroutine(2f));
        }
        else if (countorbe == 2)
        {
            anim.SetTrigger("spell");
            GameObject tempOrbe = Instantiate(OrbePrefab, OrbeEmitor.position, OrbeRotator.rotation);
            Rigidbody2D tempRB2D = tempOrbe.GetComponent<Rigidbody2D>();
            tempRB2D.AddForce(OrbeEmitor.forward * OrbeSpeed);
            tempOrbe.GetComponent<Orbe>().Damage = Damage;
            Invoke("ORBEx1", 1f);
        }
        else if (countorbe >= 3)
        {
            anim.SetTrigger("spell");
            GameObject tempOrbe = Instantiate(OrbePrefab, OrbeEmitor.position, OrbeRotator.rotation);
            Rigidbody2D tempRB2D = tempOrbe.GetComponent<Rigidbody2D>();
            tempRB2D.AddForce(OrbeEmitor.forward * OrbeSpeed);
            tempOrbe.GetComponent<Orbe>().Damage = Damage;
            Invoke("ORBEx2", 1f);

        }
    }
    public void ORBEx1()
    {
        GameObject tempOrbe = Instantiate(OrbePrefab, OrbeEmitor.position, OrbeRotator.rotation);
        Rigidbody2D tempRB2D = tempOrbe.GetComponent<Rigidbody2D>();
        tempRB2D.AddForce(OrbeEmitor.forward * OrbeSpeed);
        tempOrbe.GetComponent<Orbe>().Damage = Damage;
        StartCoroutine(ComecarCoroutine(2f));
    }
    public void ORBEx2()
    {
        GameObject tempOrbe = Instantiate(OrbePrefab, OrbeEmitor.position, OrbeRotator.rotation);
        Rigidbody2D tempRB2D = tempOrbe.GetComponent<Rigidbody2D>();
        tempRB2D.AddForce(OrbeEmitor.forward * OrbeSpeed);
        tempOrbe.GetComponent<Orbe>().Damage = Damage;
        Invoke("ORBEx3", 1f);
    }
    public void ORBEx3()
    {
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

    public void DeadBoss() //Animacao de morte do chefe
    {
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
