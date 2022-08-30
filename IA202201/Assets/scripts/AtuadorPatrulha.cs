using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtuadorPatrulha : MonoBehaviour
{
    public enum tipoDePatrulha { Quadrada, Circular, Rota};

    private Vector3 origem;
    public float distancia;
    private Vector3 limiteSup;
    private Vector3 limiteInf;
    private float tempo = 0;
    public float tempoLimite;
    private float tempoAndando;
    //public float velocidade;
    public float limiteAngulo;
    // Start is called before the first frame update
    void Start()
    {
        origem = transform.position;
        limiteSup = new Vector3(origem.x - distancia, 0, origem.z + distancia);
        limiteInf = new Vector3(origem.x + distancia, 0, origem.z - distancia);
        tempoAndando = Random.Range(1, tempoLimite);
    }

    // Update is called once per frame
    void Update()
    {
        //desenhar a área de patrulha
        Debug.DrawLine(limiteSup, new Vector3(limiteInf.x, 0, limiteSup.z),
            Color.red);
        Debug.DrawLine(limiteSup, new Vector3(limiteSup.x, 0, limiteInf.z),
            Color.red);
        Debug.DrawLine(limiteInf, new Vector3(limiteSup.x, 0, limiteInf.z),
            Color.red);
        Debug.DrawLine(limiteInf, new Vector3(limiteInf.x, 0, limiteSup.z),
            Color.red);

        //Patrulhar();
    }
    public void Patrulhar(tipoDePatrulha tipo, float velocidade)
    {
        tempo += Time.deltaTime;
        if (tempo <= tempoAndando)
        {
            //ir até o caminho escolhido
            transform.Translate(new Vector3(0, 0, velocidade * Time.deltaTime));
        }
        else
        {
            //escolher um caminho
            if (tipo == tipoDePatrulha.Quadrada)
            {
                PatrulhaQuadrada();
            }
            else
            {
                PatrulhaCircular();
            }
            
            tempoAndando = Random.Range(1, tempoLimite);
            tempo = 0;
        }
    }
    private void PatrulhaCircular()
    {

    }
    private void PatrulhaQuadrada()
    {
        if (transform.position.x < limiteSup.x ||
            transform.position.z > limiteSup.z ||
            transform.position.x > limiteInf.x ||
            transform.position.z < limiteInf.z)
        {
            //fora da área de patrulha
            Vector3 direcao = (origem - transform.position).normalized;
            Quaternion giro = Quaternion.LookRotation(direcao);
            giro.x = 0;
            giro.z = 0;
            transform.rotation = giro;
        }
        else
        {
            //dentro da área de patrulha
            float angulo = Random.value * limiteAngulo;
            transform.Rotate(new Vector3(0, angulo, 0));
        }
    }
}
