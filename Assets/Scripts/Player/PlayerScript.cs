using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    [Header("Poções")]
    public List<PotionScriptableObj> potions;
    public float ShieldPotionMult;
    public float LuckPotionMult;
    public float TimePotionMult;
    public float RunTimePotion;
    public float fillValueTest;
    public PotionScriptableObj CurrentPotion;

    public bool UsingPotion;
    public bool Debuff;
    public Image expfillamount;

    [Space(25)]
    public float speed;

	Animator anim;
	Rigidbody2D rb2d;
	Vector2 Mov;  // Agora é visível nos métodos
    Sword sword;
    Magic magic;
    CoolDown cooldown;
    ScriptBoss boss;
    UIPotionPrefs uipotions;

    [Space]
    [Header("Cameras")]
    public GameObject maincamera;
    public GameObject bosscamera;


    [Space]
    [Header("Morreu Configs")]
    public string UltimoMapa;
    public bool mapa1;
    public bool mapa2;
    public bool mapa3;
    public bool mapa4;
    public bool mapa5;
    Warp warp;
    public GameObject Respawn1;
    public GameObject Respawn2;
    public GameObject Respawn3;
    public GameObject Respawn4;
    public GameObject Respawn5;
    public GameObject player;
    public SpriteRenderer playersprite;
    public int Vidas;

 


    [Space]

    public CircleCollider2D attackCollider;

    public SpriteRenderer shadow;

    public GameObject InitialMap;

    float test;

	void Awake () {
	    Assert.IsNotNull(InitialMap);
	}

	void Start () {
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
        sword = GetComponent<Sword>();
        magic = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Magic>();
        cooldown = GameObject.FindGameObjectWithTag("CoolDown").GetComponent<CoolDown>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<ScriptBoss>();
        warp = GameObject.FindGameObjectWithTag("Warp").GetComponent<Warp>();
        

        ShieldPotionMult = 1;
        TimePotionMult = 1;
        LuckPotionMult = 1;

        Debuff = false;

        maincamera.gameObject.SetActive(true);
        bosscamera.gameObject.SetActive(false);

        mapa1 = false;
        mapa2 = false;
        mapa3 = false;
        mapa4 = false;
        mapa5 = false;



        /*	Fazemos com que a variável "attackCollider" receba o Componente CircleCollider pertencente ao GameObject
			 do "primeiro filho" do GameObject a qual esse script está inserido (que no caso é o Player).
			 (Nós declaramos o primeiro filho pela linha de código "GetChild (0)". Em inglês essa linha se traduziria
			 para algo como "Pegue o filho 0", sendo que "filho 0" nesse caso refere-se ao primeiro filho do Objeto Player)*/
        attackCollider = transform.GetChild (0).GetComponent<CircleCollider2D>();

        //shadow = transform.GetChild(3).GetComponent<SpriteRenderer>();
        //shadow.color =new Color(10, 10, 10, 103);

        /* Agora, após termos declarado a variável "attackCollider" acima, nós queremos que esse collider esteja inativo
			no início do jogo para evitar que o personagem cause dano em tudo á sua frente sem necessitar apertar a tecla
			de ataque, certo? Então, nós fazemos isso deixando como FALSA a informação de que a variável já esteja ativa.
			Inclusive estamos fazendo isso dentro do "Void Start" justamente para o jogo já começar com isso definido como desativado.*/
        attackCollider.enabled = false;
		/* Mas ter colocado a linha acima gera um problema... que é o seguinte:
			O collider já começa o jogo como falso, certo? Ok, mas então... COMO nós vamos fazer para ativar o collider
			no momento que o jogador realizar o ataque?
			Nós desativamos o collider no início do jogo, mas em nenhum momento nós pedimos para o script reativá-lo, ou seja,
			o jogador nunca vai atacar.
			É por esse motivo que nós vamos definir o momento certo para que esse Collider seja ativado. Nós o colocaremos embaixo
			da linha de código que atualiza a posição X e Y do collider conforme a direção que o personagem estiver olhando.
		*/



	//	Camera.main.GetComponent<MainCamera>().SetBound(InitialMap);
	}

	void Update () {

        if (Input.GetKey(KeyCode.L))
        {
            transform.position = new Vector3(-16.12f, 142.1f, -0.042225f);
        }

        if (RunTimePotion <= Time.time && UsingPotion == true)
        {
            PotionShieldEnd();
            PotionLuckEnd();
            TimePotionEnd();
        }

        if (UsingPotion == true)
        {
            fillValueTest = Mathf.Clamp(RunTimePotion - Time.time, 0, CurrentPotion.Duration);
            expfillamount.fillAmount = fillValueTest / CurrentPotion.Duration;
        }


        if(Vidas < 0)
        {
            warp.FadeIn();
            StartCoroutine(CenaGameOver());
        }

    

        

        // Detectando movimento em vector 2D
        Mov = new Vector2(
			Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical")
		);

		// Estabelecendo as animacoes
		if (Mov != Vector2.zero) {
			anim.SetFloat("movX", Mov.x);
			anim.SetFloat("movY", Mov.y);
			anim.SetBool("Walking", true);
		} else {
			anim.SetBool("Walking", false);
        }


		//Buscamos o estado atual conferindo a informação da animação de ataque no animator.
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		bool attacking = stateInfo.IsName("Arthur_Attack");


        //Detectamos o Ataque do personagem, o ataque têm prioridade e por isto o código é colocado por último.
        if ((Input.GetKeyDown("space") && !attacking) || ((Input.GetKeyDown(KeyCode.Mouse0) && !attacking)))
        { /*		-------> Essa linha apenas permite que o jogador
		realize a animação de ataque caso a tecla espaço for pressionada, e além disso, só permite a execução da
		animação caso o jogador não esteja com esta animação de ataque sendo executada no exato momento que ele
		pressionar a tecla espaço. (Por esse motivo aquela parte "&& !attacking"							  */
            
            if (sword.Sword_CurrentDamage != 0)
            {
                anim.SetTrigger("Attacking");
                AudioManager.instancia.PlaySFX(AudioManager.instancia.attackClip);
            }
            else
            {
                Hud_Menu HudMenu = GameObject.FindGameObjectWithTag("Area").GetComponent<Hud_Menu>();
                HudMenu.ArmaEquipada();
            }
        }





        // Com isso abaixo nós vamos ficar atualizando a posição X e Y da colisão do ataque do personagem para
        // que ele se ajuste á direção que o jogador está virado no momento atual da jogatina.
        // O colisor do ataque será a região onde o jogador vai causar dano nos objetos e inimigos que estiverem ali.
        if (Mov != Vector2.zero) attackCollider.offset = new Vector2(Mov.x/3, Mov.y/3);
        

        /* É aqui que nós vamos ativar o collider quando a animação do ataque estiver sendo executada.
		 	Isso vai fazer uma checagem a cada frame, visto que estamos escrevendo o código dentro da "Void Update".
			Em resumo, essa checagem faz com que quando a animação de ataque já tiver começado, o collider do ataque
			só fique LIGADO enquanto o tempo de execução da animação for maior que 20% do tempo total dela, e enquanto
			o tempo da animação for menor que 70% do tempo total, fazendo com que ANTES de 20% e APÓS 70% do tempo que
			a execução da animação levar, o collider fique DESLIGADO.
			
			Isso serve pra justificar o fato do ataque do personagem causar dano nos inimigos, dando mais realismo para
			o jogo, visto que se nós permitirmos que o collider fique ativo durante todo o tempo de execução da animação,
			o ataque também vai funcionar mesmo nos primeiros e nos últimos frames da animação, mas os primeiros frames
			são onde o jogador ainda estará "sacando" a espada pra realizar o ataque e nos últimos frames ele já está com
			a espada parada após realizar o corte com a lâmina, então não faz sentido que esse tempo também seja levado em
			conta na hora de aplicar o dano nos inimigos e nos objetos.

			Isso também impede que o jogador fique dedilhando a tecla espaço fazendo com que a direção que ele esteja
			olhando se torne uma área onde o dano seja sempre constante */
        if (attacking){
            
			float PlaybackTime = stateInfo.normalizedTime;
            if (PlaybackTime > 0.2 && PlaybackTime < 0.7)
            {

                attackCollider.enabled = true;
            }
            else
            {
                attackCollider.enabled = false;
                //    print("nao atacou");
            }
            

        }
        if (Input.GetKeyDown(KeyCode.R) && magic.MagiaAtual != "" && cooldown.PodeUsar == true|| Input.GetKeyDown(KeyCode.Mouse1) && magic.MagiaAtual != "" && cooldown.PodeUsar == true)
        {
            if (magic.MagiaAtual == "Fogo")
            {
                
                cooldown.CoolDownMagia("Fogo");
                

            }
            else if (magic.MagiaAtual == "Floresta" && cooldown.EscudosRestante <= 0)
            {
                cooldown.CoolDownMagia("Floresta");

            }

            cooldown.CoolDownMagia(magic.MagiaAtual);
        }

    }
    public void BossPosition()
    {
        FindObjectOfType<ScriptBoss>().TeletransporteReverso(transform.position);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "TriggerBosque")
            FindObjectOfType<MainCamera>().MapaAtual = "Bosque";
        if (collision.tag == "TriggerCaverna")
            FindObjectOfType<MainCamera>().MapaAtual = "Caverna";
        if (collision.tag == "TriggerBosque2")
            FindObjectOfType<MainCamera>().MapaAtual = "Bosque2";
        if (collision.tag == "TriggerFogo")
            FindObjectOfType<MainCamera>().MapaAtual = "Fogo";



        if (collision.tag == "TriggerBosque")
            FindObjectOfType<MiniMapCamera>().MapaAtual = "Bosque";
        if (collision.tag == "TriggerCaverna")
            FindObjectOfType<MiniMapCamera>().MapaAtual = "Caverna";
        if (collision.tag == "TriggerBosque2")
            FindObjectOfType<MiniMapCamera>().MapaAtual = "Bosque2";
        if (collision.tag == "TriggerChefe")
            FindObjectOfType<MiniMapCamera>().MapaAtual = "Chefe";
        if (collision.tag == "TriggerFogo")
            FindObjectOfType<MiniMapCamera>().MapaAtual = "Fogo";


        if (collision.tag == "TriggerChefe")
        {
            maincamera.gameObject.SetActive(false);
            bosscamera.gameObject.SetActive(true);
        }
        if (collision.tag == "TriggerBosque")
        {
            maincamera.gameObject.SetActive(true);
            bosscamera.gameObject.SetActive(false);
        }
        if (collision.tag == "TriggerCaverna")
        {
            maincamera.gameObject.SetActive(true);
            bosscamera.gameObject.SetActive(false);
        }
        if (collision.tag == "TriggerBosque2")
        {
            maincamera.gameObject.SetActive(true);
            bosscamera.gameObject.SetActive(false);
        }
        if (collision.tag == "TriggerFogo")
        {
            maincamera.gameObject.SetActive(true);
            bosscamera.gameObject.SetActive(false);
        }
        // else
        //{
        //maincamera.gameObject.SetActive(true);
        //bosscamera.gameObject.SetActive(false);
        // }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TriggerBosque")
        {
            FindObjectOfType<AreaScript>().ChamarCoroutine("Reserva Florestal");
            GameManager gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gamemanager.monstrosMortos = 0;
            if(mapa1 == false)
            {
                mapa1 = true;
                UltimoMapa = "mapa1";
                maincamera.gameObject.SetActive(true);
                bosscamera.gameObject.SetActive(false);
            }
        }
        if (collision.tag == "TriggerCaverna")
        {
            FindObjectOfType<AreaScript>().ChamarCoroutine("Caverna do Diabo");
            GameManager gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gamemanager.monstrosMortos2 = 0;
            if (mapa2 == false)
            {
                mapa2 = true;
                UltimoMapa = "mapa2";
                maincamera.gameObject.SetActive(true);
                bosscamera.gameObject.SetActive(false);
            }
        }
        if (collision.tag == "TriggerBosque2")
        {
            FindObjectOfType<AreaScript>().ChamarCoroutine("Bosque de Álamos");
            GameManager gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gamemanager.monstrosMortos3 = 0;
            if (mapa3 == false)
            {
                mapa3 = true;
                UltimoMapa = "mapa3";
                maincamera.gameObject.SetActive(true);
                bosscamera.gameObject.SetActive(false);
            }
        }
        if (collision.tag == "TriggerChefe")
        {
            FindObjectOfType<AreaScript>().ChamarCoroutine("Sala do Chefe");
            if(mapa4 == false)
            {
                mapa4 = true;
                UltimoMapa = "mapa4";
                maincamera.gameObject.SetActive(false);
                bosscamera.gameObject.SetActive(true);
            }
            StartCoroutine(boss.ComecarCoroutine(2f));
        }
        if (collision.tag == "TriggerFogo")
        {
            FindObjectOfType<AreaScript>().ChamarCoroutine("Vale da Morte");
            GameManager gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gamemanager.monstrosMortos = 0;
            if (mapa5 == false)
            {
                mapa5 = true;
                UltimoMapa = "mapa5";
                maincamera.gameObject.SetActive(true);
                bosscamera.gameObject.SetActive(false);
            }
        }
    }

    public void AddPotion(PotionScriptableObj[] newPotions)
    {
        for (int i = 0; i < newPotions.Length; i++)
        {
            potions.Add(newPotions[i]);
        }
    }

    public void UsePotion (PotionScriptableObj potionUsed)
    {
        for (int i = 0; i < potions.Count; i++)
        {
            if (potionUsed == potions[i])
            {

                if (potionUsed.potionFunction == 0 && !(GameObject.FindGameObjectWithTag("Content").GetComponent<HealthBar>().HP_Current == GameObject.FindGameObjectWithTag("Content").GetComponent<HealthBar>().stats.HP_Max)) {

                    GameObject.FindGameObjectWithTag("Content").GetComponent<HealthBar>().HP_Current += potionUsed.healthGain;

                    potions.RemoveAt(i);

                } else if(potionUsed.potionFunction != 0)
                {

                    CurrentPotion = potionUsed;

                    if (potionUsed.potionFunction == 1 && !UsingPotion)
                    {
                        UsePotionShield();
                    }
                    else if (potionUsed.potionFunction == 2 && Debuff == true)
                    {
                        UseRecoveryPotion();
                    }
                    else if (potionUsed.potionFunction == 3 && !UsingPotion)
                    {
                        UseTimePotion();
                    }
                    else if (potionUsed.potionFunction == 4 && !UsingPotion)
                    {
                        UseLuckPotion();
                    }

                    potions.RemoveAt(i);


                }
                else
                {
                    Hud_Menu HudMenu = GameObject.FindGameObjectWithTag("Area").GetComponent<Hud_Menu>();
                    HudMenu.NaoPodeUsar();
                }

                break;
            }        
        }
    }
    public void UsePotionShield()
    {
        RunTimePotion = CurrentPotion.Duration + Time.time;
        expfillamount.color = new Color(0, 118, 255);
        ShieldPotionMult = 0.5f;
        UsingPotion = true;
        //Invoke("PotionShieldEnd", 60f);
    }
    public void PotionShieldEnd()
    {
        ShieldPotionMult = 1f;
        expfillamount.color = new Color(255, 255, 255);
        UsingPotion = false;
        print("a");
    }
    public void UseLuckPotion()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            LuckPotionMult = 1.5f;
        }
        else
        {
            LuckPotionMult = 0.5f;
            Debuff = true;
        }

        RunTimePotion = CurrentPotion.Duration + Time.time;
        UsingPotion = true;
    }
    public void PotionLuckEnd()
    {
        LuckPotionMult = 1;
        Debuff = false;
        UsingPotion = false;
    }
    public void UseTimePotion()
    {
        TimePotionMult = 0.7f;
        expfillamount.color = new Color(255, 235, 0);
        RunTimePotion = CurrentPotion.Duration + Time.time;
        UsingPotion = true;
    }
    public void TimePotionEnd()
    {
        TimePotionMult = 1;
        expfillamount.color = new Color(255, 255, 255);
        UsingPotion = false;
    }
    public void UseRecoveryPotion()
    {
        RecoveryPotionEnd();
    }
    public void RecoveryPotionEnd()
    {
        PotionLuckEnd();
        UsingPotion = false;
    }



    void FixedUpdate () {

		rb2d.MovePosition(rb2d.position + Mov * (speed * LuckPotionMult) * Time.deltaTime);
	}


    public void Morreu()
    {
        playersprite.enabled = false;
        warp.StartCoroutine(warp.FadeMorreu());
        Vidas--;
    }
    IEnumerator CenaGameOver()
    {
        yield return new WaitForSeconds(3f);
        warp.FadeOut();
        SceneManager.LoadScene(2);
    }
}