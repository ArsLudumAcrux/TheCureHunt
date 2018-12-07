using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    //public float HP_Max = 100f;
    public float HP_Current;
    public Image HP_Bar;
    public Statistics stats;
    Animator anim;
    public bool podemorrer;
    

    // Use this for initialization
    void Start () {
        stats = FindObjectOfType<Statistics>();
        HP_Bar = GetComponent<Image>();
        anim = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Animator>();

        podemorrer = true;
        HP_Current = stats.HP_Max;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (HP_Current > stats.HP_Max)
        {
            HP_Current = stats.HP_Max;
        }

        HP_Bar.fillAmount = HP_Current / stats.HP_Max;

        if (HP_Current <= 0 && podemorrer == true)
        {
            podemorrer = false;
            //anim.SetTrigger("Death");
            //anim.SetBool("Dead", true);
            //anim.SetBool("Dead", false);
       
        }
        else
        {
            anim.SetBool("Dead", false);
            podemorrer = true;
        }
       
    }
    public void HPFull()
    {
        HP_Current = stats.HP_Max;
    }
}
