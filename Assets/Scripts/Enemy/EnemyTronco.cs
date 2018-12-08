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
        statstronco = GameObject.FindObjectOfType<Tronco_Stats>();
        cooldown = GameObject.FindGameObjectWithTag("CoolDown").GetComponent<CoolDown>();
    }
	
	// Update is called once per frame
	void Update () {
        distance = Vector2.Distance(transform.position, ObjectPlayer.transform.position);


        if (distance <= attackRadius && !stopAttack && !statstronco.morreu) // Se a distancia do player for menor que o range de ataque, ele atacará, e se ele morrer ele nao pode atacar
        {
            StartCoroutine(Attack_CR());
        }
    
    if (statstronco.morreu == false)
        {
            //Target inicial, é sempre a posicao que a slime comeca
            Vector3 target = initialPosition;
            //DestroyCollider.transform.position = transform.position;

            // Se a distancia do player for menor que a variavel visionradius, o target mudara para o player, e ele sera o alvo
            float dist = Vector3.Distance(ObjectPlayer.transform.position, transform.position);
            if (dist<visionRadius) target = ObjectPlayer.transform.position;

            // Assim que o target for o player, o tronco se movera ate ele

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

            // Se o player estiver no alcance, o tronco para e ataca
            if (target != initialPosition && distance<attackRadius)
            {
                anim.SetFloat("MovX", dir.x);
                anim.SetFloat("MovY", dir.y); 
                anim.Play("Enemy_Walk", -1, 0);  // Congela la animación de andar
            }
            //Caso nao seja o if, vai para o else para ele se mover
            else
            {
                //rb2d.MovePosition(transform.position + dir* speed * Time.deltaTime);

                //anim.speed = 1;
                //anim.SetFloat("MovX", dir.x);
                //anim.SetFloat("MovY", dir.y);
                //anim.SetBool("Walking", true);
            }
            if (target == initialPosition && distance< 0.02f)
            {
                transform.position = initialPosition;
                //anim.SetBool("Walking", false);
            }
            Debug.DrawLine(transform.position, target, Color.green);
        }
    }

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

    IEnumerator Attack_CR() //IEnumerator para acionar o ataque do tronco
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
