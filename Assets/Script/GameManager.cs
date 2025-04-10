using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [SerializeField] private Slider objBarraVida;
    [SerializeField] private Slider objBarraVelocidade;
    private int qtdPreencherBarras = 0;

    // Start is called before the first frame update
    void Start()
    {
        instancia = this;
        qtdPreencherBarras = 1;
    }

    // Update is called once per frame
    void Update()
    {
        PreencherBarras();
    }

    public void PreencherBarras()
    {
        if (objBarraVida.value < 100 && objBarraVelocidade.value < 100 && qtdPreencherBarras > 0)
        {
            objBarraVida.value++;
            objBarraVelocidade.value++;
        }
        else qtdPreencherBarras = 0;
    }

    public void PreencherBarraVida(int v) => BarraVida(v);
    public void DiminuirBarraVida(int v) => BarraVida(v);
    public void PreencherBarraVelocidade(int vc) => BarraVelocidade(vc);
    public void DiminuirBarraVelocidade(int vc) => BarraVelocidade(vc);

    private void BarraVida(int valeu)
    {
        objBarraVida.value = valeu;
    }
    private void BarraVelocidade(int valeu)
    {
        objBarraVelocidade.value = valeu;
    }
}
