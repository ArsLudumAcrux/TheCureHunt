using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour {

    /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
    // Variaveis para questionar el raio de visión, o raio de ataque e a velocidade
    public float visionRadius;
    public float attackRadius;
    public float speed;
    public float damage;
    public Slime_Stats stats;
    

    // Variavel para guardar o jogador
    public GameObject ObjectPlayer;

    // Variavel para guardar a posicão inicial
    Vector3 initialPosition;

    // Animador e RigidBody com a rotacão em Z congelada
    Animator anim;
    Rigidbody2D rb2d;

    CoolDown cooldown;
   

    

	// Variáveis que configuram o raio de visão do inimigo, sua velocidade e seu dano.
	public int EnemyType = 1;


	public AudioSource Sound;
	private AudioClip DanoSound;


    Vector2 mov;


	// Variável para guardar o nome do estado de destruição do objeto.
	public string destroyState;
	// Variável com os segundos a esperar antes de desativar o colisor do objeto.
	public float timeForDisable;



	// Configuradores das moedas e chance de drop.
	public bool DropaMoeda;

	public int MaxDropChance;
	private int DropChance;
	private int DropType;
	public GameObject Moeda1;
	public GameObject Moeda2;
	public GameObject Moeda3;
	public GameObject Moeda4;
	public GameObject Moeda5;




    [SerializeField]
    float distance;

    
    public bool stopAttack;


    public HealthBar HB;





    //////////////////////////////////////////////////////////////////////////////////////////////////







    void Start () {


        // Guarda a posicao inicial da slime
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        //velocidaderb = GetComponent<Rigidbody2D>().velocity.normalized.x;

        // Encontrar o jogador pela tag
        ObjectPlayer = GameObject.FindGameObjectWithTag("Player");
        //DestroyCollider = GetComponent<CircleCollider2D>();


        //DanoSound = ObjectPlayer.GetComponent<Statistics>().DmgSound;
        HB = GameObject.FindGameObjectWithTag("Content").GetComponent<HealthBar>();
        cooldown = GameObject.FindGameObjectWithTag("CoolDown").GetComponent<CoolDown>();


    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, ObjectPlayer.transform.position);


        if(distance <= attackRadius && !stopAttack) // Se a distancia do player for menor que o range de ataque, ele atacará
        {
            StartCoroutine(Attack_CR());
        }


        if (stats.morreu == false)
        {
            //Target inicial, é sempre a posicao que a slime comeca
            Vector3 target = initialPosition;
            //DestroyCollider.transform.position = transform.position;

            // Se a distancia do player for menor que a variavel visionradius, o target mudara para o player, e ele sera o alvo
            float dist = Vector3.Distance(ObjectPlayer.transform.position, transform.position);
            if (dist < visionRadius) target = ObjectPlayer.transform.position;

            // Assim que o target for o player, a slime se movera ate ele

            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);

            // Aqui podemos ver o target com uma linha 
            Debug.DrawLine(transform.position, target, Color.green);

            //Vector3 target = initialPosition;


            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                ObjectPlayer.transform.position - transform.position,
                visionRadius,
                1 << LayerMask.NameToLayer("Default")

            // Colocar o inimigo em uma camada diferente do padrão para evitar o raycast
            );


            // Aqui vemos a linha vermelha na Scene na unity, que é o target, que é o player
            Vector3 forward = transform.TransformDirection(ObjectPlayer.transform.position - transform.position);
            Debug.DrawRay(transform.position, forward, Color.red);


            // Se o raycast encontrar o jogador, colocamos ele na variavel Target
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {
                    target = ObjectPlayer.transform.position;
                }
            }

            // Calculo para ver a distancia atual do player
            float distance = Vector3.Distance(target, transform.position);
            Vector3 dir = (target - transform.position).normalized;


            // Se o player estiver no alcance, a slime para e ataca
            if (target != initialPosition && distance < attackRadius)
            {

                // Aqui seria para atacar, mas por hora movimentamos a slime
                anim.SetFloat("MovX", dir.x);
                anim.SetFloat("MovY", dir.y); 
                //anim.Play("Enemy_Walk", -1, 0);  // Congela a animacao de andar
            }

            //Caso nao seja o if, vai para o else para ele se mover
            else
            {
                rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);

                // Ao se mover, começa a animacao de andar
                anim.speed = 1;
                anim.SetFloat("MovX", dir.x);
                anim.SetFloat("MovY", dir.y);
                anim.SetBool("Walking", true);
            }

            if (target == initialPosition && distance < 0.02f)
            {
                transform.position = initialPosition;

                // Deixar a animação andar falsa, e voltar ao idle
                anim.SetBool("Walking", false);
            }
            Debug.DrawLine(transform.position, target, Color.green);
        }
    }

    void OnDrawGizmos() {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

    }

   
    ////Caso o Inimigo slime colidir com o Player tera -10 da varialvel "HP_Current" do Script "HealthBar"
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log(HealthBar.HP_Current);
    //        HealthBar.HP_Current -= damage;
    //    }
    //}

    IEnumerator Attack_CR()
    {
        anim.SetTrigger("Attacking");
        speed = 0;
        PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (cooldown.EscudosRestante > 0)
        {
            cooldown.Escudo();
            stopAttack = true;
        }
        else
        {
            HB.HP_Current -= Mathf.RoundToInt(damage * player.ShieldPotionMult);
            stopAttack = true;
        }
        stopAttack = true;

        yield return new WaitForSeconds(2f);
        speed = 1;
        stopAttack = false;
    }
  

    /*IEnumerator OnTriggerEnter2D (Collider2D col) 	{

        var hit = col.gameObject;
        var health = hit.GetComponent<Statistics>();

        if (col.gameObject.CompareTag ("Player")) {

            //Sound.PlayOneShot(FogoSound, 1.0f);
            //anim.Play(destroyState);
            //Sound.PlayOneShot(DanoSound, 5.0f);

            if (health != null)
            {
             //   health.TakeDamage(10);
            }



            //speed = 0;
			//GetComponent<SpriteRenderer>().sortingOrder = 2000;
			//GetComponent<CircleCollider2D>().enabled = false;
			//yield return new WaitForSeconds(timeForDisable);

			//DestroyCollider.enabled = false;
			//Destroy (gameObject);

		}*/

		/*if (col.gameObject.CompareTag ("Enemy_Damage")) {

			yield return new WaitForSeconds (0.05f);
			speed = 0;
			damage = 0;
			Sound.PlayOneShot(FogoSound, 1.0f);
			anim.Play(destroyState);


			if (DropaMoeda == true) {


			DropChance = Random.Range (0, MaxDropChance);
			DropType = Random.Range (1, 100);
			if (DropChance == 0)
			{

				if (DropType >= 90) {
					Instantiate (Moeda5, transform.position, transform.rotation);
				} else {
					if (DropType >= 80) {
						Instantiate (Moeda4, transform.position, transform.rotation);
					} else {
						if (DropType >= 30) {
							Instantiate (Moeda3, transform.position, transform.rotation);
						} else {
							if (DropType >= 20) {
								Instantiate (Moeda2, transform.position, transform.rotation);
							} else {
								if (DropType >= 1) {
									Instantiate (Moeda1, transform.position, transform.rotation);
								
									}

								}

							}

						}

					}

				}

			}

			yield return new WaitForSeconds(timeForDisable);

			//DestroyCollider.enabled = false;
			Destroy (gameObject);*/
		}


	//}
//}