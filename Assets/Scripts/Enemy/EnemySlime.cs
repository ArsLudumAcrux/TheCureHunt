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

        /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
        // Guardamos nuestra posición inicial
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        //velocidaderb = GetComponent<Rigidbody2D>().velocity.normalized.x;

        // Recuperamos al jugador gracias al Tag
        ObjectPlayer = GameObject.FindGameObjectWithTag("Player");
        //DestroyCollider = GetComponent<CircleCollider2D>();


        //DanoSound = ObjectPlayer.GetComponent<Statistics>().DmgSound;
        HB = GameObject.FindGameObjectWithTag("Content").GetComponent<HealthBar>();
		

	}

    void Update()
    {
        distance = Vector2.Distance(transform.position, ObjectPlayer.transform.position);


        if(distance <= attackRadius && !stopAttack)
        {    
            StartCoroutine(Attack_CR()); 
        }


        if (stats.morreu == false)
        {
            /////////////////////////////////////////////  "CATARINA SÓ A PRIMEIRA LINHA" /////////////////////////////////////////////////////////////
            // Por defecto nuestro objetivo siempre será nuestra posición actual
            Vector3 target = initialPosition;
            //DestroyCollider.transform.position = transform.position;

            /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
            // Pero si la distancia hasta el jugador es menor que el radio de visión el objetivo será él
            float dist = Vector3.Distance(ObjectPlayer.transform.position, transform.position);
            if (dist < visionRadius) target = ObjectPlayer.transform.position;

            /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
            // Finalmente movemos al enemigo en dirección a su target

            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);

            // Y podemos debugearlo con una línea
            Debug.DrawLine(transform.position, target, Color.green);
            /////////////////////////////////////////////  "CATARINA SÓ A PRIMEIRA LINHA" /////////////////////////////////////////////////////////////
            //// Por defecto nuestro target siempre será nuestra posición inicial
            //Vector3 target = initialPosition;

            /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
            // Comprobamos un Raycast del enemigo hasta el jugador
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                ObjectPlayer.transform.position - transform.position,
                visionRadius,
                1 << LayerMask.NameToLayer("Default")

            /////////////////////////////////////////////  "CATARINA AS 3 LINHAS" /////////////////////////////////////////////////////////////
            // Poner el propio Enemy en una layer distinta a Default para evitar el raycast
            // También poner al objeto Attack y al Prefab Slash una Layer Attack 
            // Sino los detectará como entorno y se mueve trás al hacer ataques
            );

            /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
            // Aquí podemos debugear el Raycast
            Vector3 forward = transform.TransformDirection(ObjectPlayer.transform.position - transform.position);
            Debug.DrawRay(transform.position, forward, Color.red);

            /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
            // Si el Raycast encuentra al jugador lo ponemos de target
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {
                    target = ObjectPlayer.transform.position;
                }
            }
            /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
            // Calculamos la distancia y dirección actual hasta el target
            float distance = Vector3.Distance(target, transform.position);
            Vector3 dir = (target - transform.position).normalized;

            /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
            // Si es el enemigo y está en rango de ataque nos paramos y le atacamos
            if (target != initialPosition && distance < attackRadius)
            {
                /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
                // Aquí le atacaríamos, pero por ahora simplemente cambiamos la animación
                anim.SetFloat("MovX", dir.x);
                anim.SetFloat("MovY", dir.y); /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
                anim.Play("Enemy_Walk", -1, 0);  // Congela la animación de andar
            }
            /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
            // En caso contrario nos movemos hacia él
            else
            {
                rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);
                /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
                // Al movernos establecemos la animación de movimiento
                anim.speed = 1;
                anim.SetFloat("MovX", dir.x);
                anim.SetFloat("MovY", dir.y);
                anim.SetBool("Walking", true);
            }
            /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
            // Una última comprobación para evitar bugs forzando la posición inicial
            if (target == initialPosition && distance < 0.02f)
            {
                transform.position = initialPosition;
                /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
                // Y cambiamos la animación de nuevo a Idle
                anim.SetBool("Walking", false);
            }
            /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
            // Y un debug optativo con una línea hasta el target
            Debug.DrawLine(transform.position, target, Color.green);
        }
    }
    /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
    // Podemos dibujar el radio de visión sobre la escena dibujando una esfera
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
        anim.SetTrigger("Hit");
        speed = 0;
        PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        HB.HP_Current -= Mathf.RoundToInt(damage * player.ShieldPotionMult);
        print(Mathf.RoundToInt(damage * player.ShieldPotionMult));
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