using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Hud_Menu : MonoBehaviour
{
    Animator anim;
    string Borrada;
    [Header("Paneis")]
    public GameObject PanelMenu;
    public GameObject PanelEspadas;
    public GameObject PanelConfig;
    public GameObject PanelInv;
    public GameObject PanelMagia;
    public GameObject PanelTutorial;
    [Header("Botoes")]
    public GameObject BordaEspadas;
    public GameObject BordaConfig;
    public GameObject BordaInv;
    public GameObject BordaMagia;
    // variavel para pausar o jogo //
    public bool paused;
    [Header("Equipe a espada")]
    public Image TextArm;
    [Header("Placas")]
    public GameObject[] Placas;
    public string borrada;
    public string nomeSwrd;


    [Header("Inventario")]
    public GameObject uiprefab;
    public RectTransform ScrollContent;
    public List<UIPotionPrefs> ListaItens;
    public PlayerScript player;
    public Text descriptiontext;
    public Text naopodeusar;

    [Space(50)]
    public AudioSource audiosourceplayer;
    public bool pausemusic;

    Sword sword;

    

    void Start()
    {
        sword = GameObject.FindGameObjectWithTag("Player").GetComponent<Sword>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        audiosourceplayer = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        anim = GetComponent<Animator>();
        PanelMenu.SetActive(false);
        TextArm.gameObject.SetActive(false);

        PanelTutorial.SetActive(true);

        BordaConfig.SetActive(false);
        BordaEspadas.SetActive(true);
        BordaInv.SetActive(false);
        BordaMagia.SetActive(false);

        PanelConfig.SetActive(false);
        PanelEspadas.SetActive(true);
        PanelInv.SetActive(false);
        PanelMagia.SetActive(false);

    }
    public void Update()
    {
        

        Cursor.visible = paused;

        if (Input.GetKeyDown(KeyCode.Tab) || (Input.GetKeyDown(KeyCode.M)))
        {
            // Invoke("AbrirFecharMenu", 0.2f);
            AbrirFecharMenu();
        }

        //if (paused)
        //{
        //   Time.timeScale = 0;
        //} else
        //{
        //    Time.timeScale = 1;
        //}

        if (Input.anyKeyDown && PanelTutorial.activeInHierarchy)
        {
            PanelTutorial.SetActive(false);
        }

    }

    public void UpdateListItens()
    {
        descriptiontext.text = "";
        ClearItemList();
        for (int i = 0; i < player.potions.Count; i++)
        {
            GameObject tempItem = Instantiate(uiprefab, ScrollContent);
            tempItem.GetComponent<UIPotionPrefs>().SetupPotion(player.potions[i]);
            ListaItens.Add(tempItem.GetComponent<UIPotionPrefs>());
            tempItem.GetComponent<UIPotionPrefs>().player = player;
            tempItem.GetComponent<UIPotionPrefs>().hudmenu = this;
        }
    }
    public void ClearItemList()
    {
        for (int i = 0; i < ListaItens.Count; i++)
        {
            Destroy(ListaItens[i].gameObject);   
        }
        descriptiontext.text = "";
        ListaItens.Clear();
    }


    public void BtnSword(string borrada)
    {
        borrada = nomeSwrd;
        StartCoroutine(Animacao());
    }
    public void ArmaEquipada()
    {
        TextArm.gameObject.SetActive(true);
        CancelInvoke("ImageDisable");
        Invoke("ImageDisable", 1.5f);
    }
    void ImageDisable()
    {
        TextArm.gameObject.SetActive(false);
    } 
    public void botao(string name)
    {
        if (name == "Resume")
        {
            //Invoke("FecharMenu", 0.5f);
            FecharMenu();
        }
        if(name == "Espadas")
        {
            BordaConfig.SetActive(false);
            BordaEspadas.SetActive(true);
            BordaInv.SetActive(false);
            BordaMagia.SetActive(false);

            PanelConfig.SetActive(false);
            PanelEspadas.SetActive(true);
            PanelInv.SetActive(false);
            PanelMagia.SetActive(false);

            descriptiontext.text = "";
            ClearItemList();
        }
        if (name == "Config")
        {
            BordaConfig.SetActive(true);
            BordaEspadas.SetActive(false);
            BordaInv.SetActive(false);
            BordaMagia.SetActive(false);

            PanelConfig.SetActive(true);
            PanelEspadas.SetActive(false);
            PanelInv.SetActive(false);
            PanelMagia.SetActive(false);

            descriptiontext.text = "";
            ClearItemList();
        }
        if (name == "Inv")
        {
            BordaConfig.SetActive(false);
            BordaEspadas.SetActive(false);
            BordaInv.SetActive(true);
            BordaMagia.SetActive(false);

            PanelConfig.SetActive(false);
            PanelEspadas.SetActive(false);
            PanelInv.SetActive(true);
            PanelMagia.SetActive(false);

            descriptiontext.text = "";
            UpdateListItens();

        }
        if (name == "Magia")
        {
            BordaConfig.SetActive(false);
            BordaEspadas.SetActive(false);
            BordaInv.SetActive(false);
            BordaMagia.SetActive(true);

            PanelConfig.SetActive(false);
            PanelEspadas.SetActive(false);
            PanelInv.SetActive(false);
            PanelMagia.SetActive(true);

            descriptiontext.text = "";
            ClearItemList();
        }
    } 
    void AbrirFecharMenu()
    {
        PanelMenu.SetActive(!PanelMenu.activeInHierarchy);
        paused = !paused;
        float pause = paused ? 0 : 1;
        Time.timeScale = pause;
        pausemusic = !pausemusic;
        if (pausemusic == true) {
            audiosourceplayer.Pause();
        }
        else if(pausemusic == false){
            audiosourceplayer.UnPause();
        }
        //Cursor.visible = !Cursor.visible;

    }
    void FecharMenu()
    {
        PanelMenu.SetActive(false);
        paused = false;
        float pause = paused ? 0 : 1;
        Time.timeScale = pause;
        pausemusic = !pausemusic;
        if (pausemusic == true)
        {
            audiosourceplayer.Pause();
        }
        else if (pausemusic == false)
        {
            audiosourceplayer.UnPause();
        }

        for (int i = 0; i < sword.BordaEspada.Length; i++)
        {
            sword.BordaEspada[i].gameObject.SetActive(false);
        }
        //Time.timeScale = 1;
        //Cursor.visible = false;
    }

    public void NaoPodeUsar()
    {
        descriptiontext.gameObject.SetActive(false);
        naopodeusar.text = "Desculpe, isso não é possivel!";
        StartCoroutine(apagartexto());
    }
    IEnumerator apagartexto()
    {
        yield return new WaitForSecondsRealtime(2f);
        naopodeusar.text = "";
        descriptiontext.gameObject.SetActive(true);
    }

    IEnumerator Animacao()
    {
        
        yield return new WaitForSecondsRealtime(0.1f);
        anim.SetBool("EspadaBorrada_FadeOut", true);
        anim.SetBool("EspadaBorrada_Esconder", false);

        yield return new WaitForSecondsRealtime(2f);
        anim.SetBool("EspadaBorrada_FadeOut", false);
        anim.SetBool("EspadaBorrada_Esconder", true);


        //if (Boiola == "Sword2")
        //{
        //    anim.Play("Espada_Borrada2_FadeOut");
        //    yield return new WaitForSecondsRealtime(2f);
        //    anim.Play("Espada_Borrada2_Esconder");
        //}
        //if (Boiola == "Sword3")
        //{
        //    anim.Play("Espada_Borrada3_FadeOut");
        //    yield return new WaitForSecondsRealtime(2f);
        //    anim.Play("Espada_Borrada3_Esconder");
        //}
        //if (Boiola == "Sword4")
        //{
        //    anim.Play("Espada_Borrada4_FadeOut");
        //    yield return new WaitForSecondsRealtime(2f);
        //    anim.Play("Espada_Borrada4_Esconder");
        //}
    }
}
