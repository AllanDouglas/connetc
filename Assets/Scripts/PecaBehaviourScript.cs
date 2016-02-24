﻿using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PecaBehaviourScript : MonoBehaviour
{

    //public enum Tipo { SOMA, SUBTRACAO};
    // eventos
    public delegate void OnMouseClick(PecaBehaviourScript peca);
    public static event OnMouseClick OnPecaClicada;
    [Header("Sprite da peca")]
    public SpriteRenderer spriteRenderer;
    [Header("Identificador único da peca")]
    public int id;
	[Header("Sprite da tranformação do coringa")]
	public Sprite spriteCoringa;
	//[Header("Rotulo mostrando o tipo da peca")]
    //public TextMesh label; // label do tipo
    //[Header("Tipo da peca para contagem")]
    //public Tipo tipo;


    //posicao no tabuleiro 
    private int _x; 
    private int _y;
	// flega o status de coringa
	private bool _coringa = false;

	public bool Coringa{
		get { return _coringa; }
	}

    private Color _cor; // cor do sprite
    private LineRenderer _linha; // linha
	private Sprite _spritePadrao; // sprite padrão da peca
    //encapsulamento da cor
    public Color cor
    {
        get {
            return _cor;
        }        
    }


    public int y
    {
        get
        {
            return _y;
        }
        private set
        {
            _y = value;
        }
    }

    public int x
    {
        get
        {
            return _x;
        }
        private set
        {
            _x = value;
        }
    }

    public void SetPosicao(int x, int y)
    {
		this.x = x;
        this.y = y;
        
        gameObject.name = string.Format("({0},{1})", x, y);        
        _linha.enabled = true;

    }

	public void TransformaEmCoringa(){
		_coringa = true;
		this.spriteRenderer.color = Color.white;
		this.spriteRenderer.sprite = spriteCoringa;
		this._linha.SetColors (Color.gray,Color.gray);
	}



    void Awake()
    {
        //seta a camada 
        gameObject.layer = LayerMask.NameToLayer("Pecas");
        //adiciona component de linha
        _linha = gameObject.AddComponent<LineRenderer>();
    }
    //sai do tabuleiro
    public void Sair()
    {
        // x = -1;
        // y = -1;
        _linha.SetVertexCount(0);
        _linha.enabled = false;
        gameObject.name = "desativada"; // deve ser removido para o metodo da classe Peca

		// quando a peca sai do tabuleiro ela deve voltar ao normal
		this._coringa = false;
		this.spriteRenderer.sprite = _spritePadrao;

		gameObject.SetActive(false);
    
    }

	private void Desativar(){
		gameObject.SetActive(false);
	}

	// Use this for initialization
    void Start()
    {
        //configura a camada da peca
        this.spriteRenderer.sortingLayerName = "Pecas";
        //pega a cor do sprite
        this._cor = this.spriteRenderer.color;
		this._spritePadrao = this.spriteRenderer.sprite;

        _linha.SetVertexCount(2);
        _linha.material = new Material(Shader.Find("Sprites/Default"));
        _linha.SetWidth(0.2f, 0.2f);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
