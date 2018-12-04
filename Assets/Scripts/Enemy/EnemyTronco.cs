using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTronco : MonoBehaviour {

    public Transform BulletEmitor;
    public GameObject BulletPrefab;
    public float BulletSpeed;
    public Transform BulletRotator;

    public float visionRadius;
    public float attackRadius;
    public float speed;
    public float damage;
    public Tronco_Stats statstronco;

    public GameObject ObjectPlayer;

    Vector3 initialPosition;

    Animator anim;
    Rigidbody2D rb2d;

    public int EnemyType = 1;

    public AudioSource Sound;
    private AudioClip DanoSound;

    Vector2 mov;

    CoolDown cooldown;

    public bool DropaMoeda;
    public int MaxDropChance;
    private int DropChance;
    private int DropType;
    public GameObject Moeda1;
    public GameObject Moeda2;
    public GameObject Moeda3;

    [SerializeField]
    float distance;

    public bool stopAttack;

    // Use this for initialization
    void Start () {
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        ObjectPlayer = GameObject.FindGameObjectWithTag("Player");
        statstronco = FindObjectOfType<Tronco_Stats>();
        cooldown = GameObject.FindGameObjectWithTag("CoolDown").GetComponent<CoolDown>();
    }
	
	// Update is called once per frame
	void Update () {
        distance = Vector2.Distance(transform.position, ObjectPlayer.transform.position);


        if (distance <= attackRadius && !stopAttack)
        {
            StartCoroutine(Attack_CR());
        }
    
    if (statstronco.morreu == false)
        {
            /////////////////////////////////////////////  "CATARINA SÓ A PRIMEIRA LINHA" /////////////////////////////////////////////////////////////
            // Por defecto nuestro objetivo siempre será nuestra posición actual
            Vector3 target = initialPosition;
    //DestroyCollider.transform.position = transform.position;

    /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
    // Pero si la distancia hasta el jugador es menor que el radio de visión el objetivo será él
    float dist = Vector3.Distance(ObjectPlayer.transform.position, transform.position);
            if (dist<visionRadius) target = ObjectPlayer.transform.position;

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
            if (target != initialPosition && distance<attackRadius)
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
                //rb2d.MovePosition(transform.position + dir* speed * Time.deltaTime);
/////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
// Al movernos establecemos la animación de movimiento
                //anim.speed = 1;
                //anim.SetFloat("MovX", dir.x);
                //anim.SetFloat("MovY", dir.y);
                //anim.SetBool("Walking", true);
            }
            /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
            // Una última comprobación para evitar bugs forzando la posición inicial
            if (target == initialPosition && distance< 0.02f)
            {
                transform.position = initialPosition;
                /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
                // Y cambiamos la animación de nuevo a Idle
                //anim.SetBool("Walking", false);
            }
            /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
            // Y un debug optativo con una línea hasta el target
            Debug.DrawLine(transform.position, target, Color.green);
        }
    }
    /////////////////////////////////////////////  "CATARINA" /////////////////////////////////////////////////////////////
    // Podemos dibujar el radio de visión sobre la escena dibujando una esfera
    void OnDrawGizmos()
{

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
        //anim.SetTrigger("Hit");
        speed = 0;
        stopAttack = true;
        GameObject tempBullet = Instantiate(BulletPrefab, BulletEmitor.position, BulletRotator.rotation);

        Rigidbody2D tempRB2D = tempBullet.GetComponent<Rigidbody2D>();
        tempRB2D.AddForce(BulletEmitor.forward * BulletSpeed);

        tempBullet.GetComponent<Projetil>().Damage = damage;



        yield return new WaitForSeconds(3.2f);
        speed = 1;
        stopAttack = false;
    }
}
