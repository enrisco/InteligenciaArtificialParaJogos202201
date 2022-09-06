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
    // tipos de distância
    //public float distancia1;
    //public float distancia2;
    //public float distancia3;
    //public Transform alvo;

    //Rota
    public List<GameObject> pontosParaAndar;
    public List<GameObject> pontosPassados;

    void Start()
    {
        origem = transform.position;
        limiteSup = new Vector3(origem.x - distancia, 0, origem.z + distancia);
        limiteInf = new Vector3(origem.x + distancia, 0, origem.z - distancia);
        tempoAndando = Random.Range(1, tempoLimite);

        GameObject[] pontos = GameObject.FindGameObjectsWithTag("waypoint");
        foreach (var item in pontos)
        {
            pontosParaAndar.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {


        //Patrulhar();
    }
    public void Patrulhar(tipoDePatrulha tipo, float velocidade)
    {
        if (tipo == tipoDePatrulha.Rota)
        {
            PatrulhaRota(velocidade);
        }
        else
        {
            if (tipo == tipoDePatrulha.Quadrada)
            {
                //desenhar a área de patrulha quadrada
                Debug.DrawLine(limiteSup, new Vector3(limiteInf.x, 0, limiteSup.z),
                    Color.red);
                Debug.DrawLine(limiteSup, new Vector3(limiteSup.x, 0, limiteInf.z),
                    Color.red);
                Debug.DrawLine(limiteInf, new Vector3(limiteSup.x, 0, limiteInf.z),
                    Color.red);
                Debug.DrawLine(limiteInf, new Vector3(limiteInf.x, 0, limiteSup.z),
                    Color.red);
            }

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
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawSphere(origem, distancia);
    //}
    private void PatrulhaCircular()
    {
     
    }
    private void PatrulhaRota(float velocidade)
    {
        #region como definir a distância de 2 Vector 
        /*
        distancia1 = Vector3.Distance(transform.position,
            alvo.position);
        distancia2 = (transform.position - alvo.position).magnitude;
        Vector3 direcao = (alvo.position - transform.position);
        distancia3 = Mathf.Sqrt((direcao.x * direcao.x) + (direcao.y * direcao.y) +
            (direcao.z * direcao.z));
        */
        #endregion

        //mostrar os pontos de passagem
        Vector3 pontoAtual = transform.position;
        int cont = 0;
        while (cont < pontosParaAndar.Count)
        {
            Debug.DrawLine(pontoAtual, pontosParaAndar[cont].transform.position,
                Color.blue);
            pontoAtual = pontosParaAndar[cont].transform.position;
            cont++;
        }
        

        Vector3 direcao = (pontosParaAndar[0].transform.position - 
            transform.position);
        float distancia = Vector3.Distance(pontosParaAndar[0].transform.position, 
            transform.position);
        if (distancia > 0.5f)
        {
            //chega mais próximo
            //rotacionar e se aproximar
            transform.rotation = Quaternion.LookRotation(direcao);
            transform.Translate(new Vector3(0, 0, velocidade * Time.deltaTime));
        }
        else
        {
            // troca de ponto
            pontosPassados.Add(pontosParaAndar[0]);
            pontosParaAndar.RemoveAt(0);
            if (pontosParaAndar.Count == 0)
            {
                foreach (var item in pontosPassados)
                {
                    pontosParaAndar.Add(item);
                }
                pontosPassados.Clear();
            }
        }

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
