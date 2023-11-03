using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class StateMachine : MonoBehaviour
{

    // SI TE INTERESA CHECAR ALGO MÁS SOFISTICADO TODAVÍA:
    // https://www.gamedeveloper.com/programming/behavior-trees-for-ai-how-they-work#close-modal

    [SerializeField]
    private Transform _objetivo;

    [SerializeField]
    private float _distanciaMinima;

    [SerializeField]
    private float _vida;

    [SerializeField]
    private float _vidaMinima;

    // estados
    private State _feliz, _enojado, _dormido, _hambriento;
    
    // símbolos (lenguaje)
    private Symbol _molestar, _premiar, _alimentar;
    private State _estadoActual;

    private MonoBehaviour _comportamientoActual;

    void Awake() 
    {
        Assert.IsNotNull(_objetivo, "OBJETIVO NO PUEDE SER NULO EN STATE MACHINE");
    }

    // Start is called before the first frame update
    void Start()
    {
        _feliz = new State("feliz", typeof(FelizBehaviour));
        _enojado = new State("enojado", typeof(EnojadoBehaviour));
        _dormido = new State("dormido", typeof(DormidoBehaviour));
        _hambriento = new State("hambriento", typeof(HambrientoBehaviour));

        _molestar = new Symbol("molestar");
        _premiar = new Symbol("premiar");
        _alimentar = new Symbol("alimentar");

        // función de transferencia 
        _feliz.AgregarRelacion(_molestar, _enojado);
        _feliz.AgregarRelacion(_alimentar, _dormido);

        _enojado.AgregarRelacion(_premiar, _feliz);

        _dormido.AgregarRelacion(_molestar, _enojado);
        _dormido.AgregarRelacion(_premiar, _hambriento);

        _hambriento.AgregarRelacion(_alimentar, _feliz);

        // estado inicial
        _estadoActual = _feliz;
        ActualizarComportamiento();

        StartCoroutine(ChecarDistanciaConObjetivo());
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyUp(KeyCode.M))
        {
            ActualizarEstado(_molestar);
        }
*/
        if(Input.GetKeyUp(KeyCode.P))
        {
            PerderVida();
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            ActualizarEstado(_alimentar);
        }
    }

    private void ActualizarEstado(Symbol simbolo) 
    {
        State nuevoEstado = _estadoActual.AplicarSimbolo(simbolo);

        // importante: si es el mismo estado no hacer nada
        if(nuevoEstado != _estadoActual)
        {
            _estadoActual = nuevoEstado;
            ActualizarComportamiento();
        }
    }

    private void ActualizarComportamiento()
    {
        // AQUÍ es donde vamos a cambiar la lógica
        
        // borrar comportamiento anterior
        if(_comportamientoActual != null)
        {
            Destroy(_comportamientoActual);
        }

        // agregar nuevo comportamiento
        // (Esto es nuevo!)
        _comportamientoActual = gameObject.AddComponent(_estadoActual.Behaviour) as MonoBehaviour;
    }

    // checar en intervalos regulares distancia con objetivo
    IEnumerator ChecarDistanciaConObjetivo() 
    {
        while(true)
        {
            float distance = Vector3.Distance(transform.position, _objetivo.position);
            if(distance < _distanciaMinima)
            {
                ActualizarEstado(_molestar);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    // perder vida con teclazo
    void PerderVida()
    {
        _vida -= 10;
        if(_vida < _vidaMinima)
        {
            ActualizarEstado(_premiar);
        }
    }
}
