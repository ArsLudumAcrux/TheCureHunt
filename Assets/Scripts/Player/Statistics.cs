using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Statistics : MonoBehaviour
{

    [Header("Status Player")]
    public string Nome;
    [SerializeField]
    public float HP_Max = 100;    
    public int Level;
    public float Exp;
    public float ExpAtual;
    public float XPToNextLevel;
    public float XPToNextLevelFixed;
    public int Gold;
    public float strongh;
    public Text LevelText;
    public Image levelup;
    [Header("Level Up")]
    bool MensagemLvl1;
    bool MensagemLvl3;
    bool MensagemLvl4;
    bool MensagemLvl6;
    bool MensagemLvl8;
    bool MensagemLvl10;
    [Space]
    [Header("Game Statistics")]
    public float TempoDeJogo;
   //public int InimigosMortos;
    public float HpPerdido;
   

    [Header("Confg. Coins")]

    private int MoedaExp;
    private int MoedaGold;


    public Text CoinText;
    public Text CoinTextMenu;
    public Text CoinTextShadow;


    //public UnityEngine.UI.Text NameText;
    //public UnityEngine.UI.Text NameTextShadow;


    [Header("Sounds")]
    public AudioClip CoinSound;
    public AudioClip DmgSound;
    public AudioClip RecoverSound;
    public AudioClip LevelUp;
    private AudioSource source;

    [Header("Others")]
    public GameObject ObjectEnemy;
    Enemy_Dmg Damage;
    public Sword espada;
    public DropCoin dropcoin;
    Magic magic;
    Hud_Menu hudmenu;

    Animator anim;
    ExpBar expbar;
    public HealthBar HB;

    private void Start()
    {
        expbar = GameObject.FindGameObjectWithTag("ExpBar").GetComponent<ExpBar>();
        anim = GetComponent<Animator>();
        levelup.gameObject.SetActive(false);

        magic = GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<Magic>();
        hudmenu = GameObject.FindGameObjectWithTag("Area").GetComponent<Hud_Menu>();
        MensagemLvl1 = true;
        MensagemLvl3 = true;
        MensagemLvl4 = true;
        MensagemLvl6 = true;
        MensagemLvl8 = true;
        MensagemLvl10 = true; 


        ExpAtual = 0;
        for (int i = 0; i < espada.Espadas.Length; i++)
        {
            espada.Espadas[i].gameObject.SetActive(false);
            espada.Cadeado[i].gameObject.SetActive(true);
            espada.EspadasBorrada[i].gameObject.SetActive(true);
        }
    }


    void Awake()
    {

        
        //Get and store a reference to the Rigidbody2D component so that we can access it.

        //Nome = PlayerPrefs.GetString("NomeDoPlayer");
        Gold = 0;
        Level = 1;
        ExpAtual = 0;
        LevelText.text = Level.ToString();
        XPToNextLevel = 32;
        XPToNextLevelFixed = 32;
        TempoDeJogo = 0;
        


        PlayerPrefs.SetInt("GoldDoPlayer", Gold);
        PlayerPrefs.SetInt("LevelDoPlayer", Level);

        source = GetComponent<AudioSource>();
        //Damage = FindObjectOfType <Willow_Enemy> ();

    }


    void OnTriggerEnter2D(Collider2D col)
    {
            if (col.gameObject.CompareTag("Coin"))
        {
            int intCoinsText = int.Parse(CoinText.text);
            intCoinsText += 1;
            CoinText.text = intCoinsText.ToString();
            CoinTextMenu.text = intCoinsText.ToString();
            Destroy(col.gameObject);
        }

        
        if (col.gameObject.CompareTag("Enemy"))
        {
            source.PlayOneShot(DmgSound, 5.0f);
        }

    }


        // Update is called once per frame
        void Update () {

        /*Essa variável mostra o tempo de jogo dentro do Inspector: */
        TempoDeJogo = TempoDeJogo + Time.deltaTime;

        if (Level >= 10)
        {
            LevelText.text = "Max";
        }

        if (Level == 1)
        {
            espada.Cadeado[0].gameObject.SetActive(false);
            espada.EspadasBorrada[0].gameObject.SetActive(true);
            espada.Espadas[0].gameObject.SetActive(true);
            if (MensagemLvl1 == true)
            {
                hudmenu.MensagemAoUpar.text = "Você liberou uma nova espada!";
                hudmenu.MensagemAoUparImg.enabled = true;
                hudmenu.StartCoroutine(hudmenu.DisableMensagem());
                MensagemLvl1 = false;

            }

        }
        else if (Level == 3)
        {
            magic.DisableGemaBloq("Fogo");

            if (MensagemLvl3 == true)
            {
                hudmenu.MensagemAoUpar.text = "Você liberou uma magia!";
                hudmenu.MensagemAoUparImg.enabled = true;
                hudmenu.StartCoroutine(hudmenu.DisableMensagem());
                MensagemLvl3 = false;
            }
        }
        else if (Level == 4)
        {
            espada.Cadeado[1].gameObject.SetActive(false);
            espada.EspadasBorrada[1].gameObject.SetActive(true);
            espada.Espadas[1].gameObject.SetActive(true);

            if (MensagemLvl4 == true)
            {
                hudmenu.MensagemAoUpar.text = "Você liberou uma nova espada!";
                hudmenu.MensagemAoUparImg.enabled = true;
                hudmenu.StartCoroutine(hudmenu.DisableMensagem());
                MensagemLvl4 = false;
            }
        }
        else if (Level == 6)
        {
            espada.Cadeado[2].gameObject.SetActive(false);
            espada.EspadasBorrada[2].gameObject.SetActive(true);
            espada.Espadas[2].gameObject.SetActive(true);

            if (MensagemLvl6 == true)
            {
                hudmenu.MensagemAoUpar.text = "Você liberou uma nova espada!";
                hudmenu.MensagemAoUparImg.enabled = true;
                hudmenu.StartCoroutine(hudmenu.DisableMensagem());
                MensagemLvl6 = false;
            }
        }
        else if (Level == 8)
        {
            magic.DisableGemaBloq("Floresta");

            if (MensagemLvl8 == true)
            {
                hudmenu.MensagemAoUpar.text = "Você liberou uma magia!";
                hudmenu.MensagemAoUparImg.enabled = true;
                hudmenu.StartCoroutine(hudmenu.DisableMensagem());
                MensagemLvl8 = false;
            }
        }
        else if (Level == 10)
        {
            expbar.PodeUparNivel = false;
            espada.Cadeado[3].gameObject.SetActive(false);
            espada.EspadasBorrada[3].gameObject.SetActive(true);
            espada.Espadas[3].gameObject.SetActive(true);

            if (MensagemLvl10 == true)
            {
                hudmenu.MensagemAoUpar.text = "Você liberou uma nova espada!";
                hudmenu.MensagemAoUparImg.enabled = true;
                hudmenu.StartCoroutine(hudmenu.DisableMensagem());
                MensagemLvl10 = false;
            }

        }
        


    }
    public void LevelAtual()
    {
        if(Level == 1)
        {
            levelup.gameObject.SetActive(true);
            Invoke("UparLevel", 2f);
            HB.HPRecuperation(); // Metodo para quando aumentar a vida maxima do player, recuperar uma % de vida do arthur
        }
        else if (Level == 2)
        {
            levelup.gameObject.SetActive(true);
            Invoke("UparLevel", 2f);
            HB.HPRecuperation(); // Metodo para quando aumentar a vida maxima do player, recuperar uma % de vida do arthur
        }
        else if (Level == 3)
        {
            levelup.gameObject.SetActive(true);
            Invoke("UparLevel", 2f);
            HB.HPRecuperation(); // Metodo para quando aumentar a vida maxima do player, recuperar uma % de vida do arthur
        }
        else if (Level == 4)
        {
            levelup.gameObject.SetActive(true);
            Invoke("UparLevel", 2f);
            HB.HPRecuperation(); // Metodo para quando aumentar a vida maxima do player, recuperar uma % de vida do arthur
        }
        else if (Level == 5)
        {
            levelup.gameObject.SetActive(true);
            Invoke("UparLevel", 2f);
            HB.HPRecuperation(); // Metodo para quando aumentar a vida maxima do player, recuperar uma % de vida do arthur
        }
        else if (Level == 6)
        {
            levelup.gameObject.SetActive(true);
            Invoke("UparLevel", 2f);
            HB.HPRecuperation(); // Metodo para quando aumentar a vida maxima do player, recuperar uma % de vida do arthur
        }
        else if (Level == 7)
        {
            levelup.gameObject.SetActive(true);
            Invoke("UparLevel", 2f);
            HB.HPRecuperation(); // Metodo para quando aumentar a vida maxima do player, recuperar uma % de vida do arthur
        }
        else if (Level == 8)
        {
            levelup.gameObject.SetActive(true);
            Invoke("UparLevel", 2f);
            HB.HPRecuperation(); // Metodo para quando aumentar a vida maxima do player, recuperar uma % de vida do arthur
        }
        else if (Level == 9)
        {
            levelup.gameObject.SetActive(true);
            Invoke("UparLevel", 2f);
            HB.HPRecuperation(); // Metodo para quando aumentar a vida maxima do player, recuperar uma % de vida do arthur
        }
        else if (Level == 10)
        {
            levelup.gameObject.SetActive(true);
            Invoke("UparLevel", 2f);
            HB.HPRecuperation(); // Metodo para quando aumentar a vida maxima do player, recuperar uma % de vida do arthur
        }

    }
       public void UparLevel()
    {
        levelup.gameObject.SetActive(false);
    }
    
    //public void Experiencia(int xpMin, int xpMax)
    //{
    //    int Exp = Random.Range(xpMin, xpMax);
    //    ExpAtual += Exp;
    //    experiencia_img.fillAmount = ExpAtual / Exp;
    //    print("Experiencia Ganha:"+ Exp);
    //    print("Experiencia atual:"+ ExpAtual);
    //}
}