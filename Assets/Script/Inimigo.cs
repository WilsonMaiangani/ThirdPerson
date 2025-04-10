using UnityEngine;
using UnityEngine.UI;

public class Inimigo : MonoBehaviour
{
    [SerializeField] private int vidaMaxima = 0;
    [SerializeField] private int vida;
    [SerializeField] private Slider barraVida;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        vida = vidaMaxima;
        barraVida.maxValue = vidaMaxima;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PreencherBarraVida(int v) => BarraVida(v);
    public void DiminuirBarraVida(int v) => BarraVida(v);

    public void CausarDanoInimigo(int dano)
    {
        vida -= dano;
        DiminuirBarraVida(vida);

        if (vida <= 0) Morrer();
    }

    private void Morrer()
    {
        animator.SetTrigger("Morreu");
        Destroy(gameObject, 0.5f);
    }

    private void BarraVida(int valeu)
    {
        barraVida.value = valeu;
    }
}
