using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01 : MonoBehaviour
{

    // Variaveis para questionar o raio de visão, o raio de ataque e a velocidade (cat)
    public float visionRadius;
    public float attackRadius;
    public float speed;

    // Variavel para guardar o jogador
    GameObject player;

    // Variavel para guardar a posicão inicial
    Vector3 initialPosition;

    // Animador e RigidBody com a rotacão em Z congelada
    Animator anim;
    Rigidbody2D rb2d;

    public GameObject ObjectPlayer;

    void Start()
    {


        //Recuperamos o jogador graças ao Tag (cat)
        player = GameObject.FindGameObjectWithTag("Player");


        //Guardamos nossa posição inicial (cat)
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Por padrão, nosso objetivo (É A CONQUISTA!) sempre será nossa posição atual. (cat)
        Vector3 target = initialPosition;
        //DestroyCollider.transform.position = transform.position;

        
        //Mas se a distância até o jogador é menor que o raio de visão, o objetivo será ele. (cat)
        float dist = Vector3.Distance(ObjectPlayer.transform.position, transform.position);
        if (dist < visionRadius) target = ObjectPlayer.transform.position;

       
        //Finalmente movemos o inimigo em direção a sua Target (cat)
        float fixedSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);

      
        //E podemos debuguear com uma linha (cat)
        Debug.DrawLine(transform.position, target, Color.green);

        // Por padrão, nosso target sempre será nossa posição inicial (cat)
        //Vector3 target = initialPosition;

        
        // Comprovamos um Raycast do inimigo até o jogador (cat)
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            player.transform.position - transform.position,
            visionRadius,
            1 << LayerMask.NameToLayer("Default")

        // Colocar o próprio Enemy em uma Layer diferente a Default para evitar o Raycast (cat)

        // também colocar o objeto Attack e o Prefab Slash uma Layer Attack (cat)
        
        // Se não, os detectará como entorno e recuará depois de fazer o ataque. (cat)
        );


        //Aqui podemos debugear o raycast (cat)
        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        
        // Se o Raycast encontra ao jogador, o colocamos de target (cat)
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                target = player.transform.position;
            }
        }

        
        // Calculamos a distância e direção atual até o target (cat)
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;

        
        // Se é o inimigo, e está no alcance do ataque, nós paramos e atacamos (cat)
        if (target != initialPosition && distance < attackRadius)
        {
            
            // Aqui o atacaríamos, mas por hora, simplesmente mudamos a animação (cat)
            anim.SetFloat("MovX", dir.x);
            anim.SetFloat("MovY", dir.y);
            anim.Play("Enemy_Walk", -1, 0);  // Congela la animación de andar
        }
        
        //Caso contrário, nos movemos até ele. (cat)
        else
        {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);


            // Ao nos mover, estabelecemos a animação de movimento.(cat)
            anim.speed = 1;
            anim.SetFloat("MovX", dir.x);
            anim.SetFloat("MovY", dir.y);
            anim.SetBool("Walking", true);
        }

        
        // Uma última comprovação para evitar Bugs, forçando a posição inicial (cat)
        if (target == initialPosition && distance < 0.02f)
        {
            transform.position = initialPosition;
            
            // E mudamos a animação de novo a Idle (cat)
            anim.SetBool("Walking", false);
        }

        
        // E um debug opcional com uma linha até o target (cat)
        Debug.DrawLine(transform.position, target, Color.green);
    }

    
    // Podemos desenhar o raio de visão e ataque sobre a cena desenhando uma esfera. (cat)
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

    }

}
