using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleStateMachine : MonoBehaviour
{

    // enum - enumeración 


    // GRAN DESVENTAJA DE ESTA IMPLEMENTACIÓN
    // código no es reutilizable - está intimamente ligado a la definición de la enumeración estados y
    // la enumeración símbolos    
    public enum Estado {
        HAMBRIENTO,
        FELIZ,
        ENOJADO,
        DORMIDO  
    }

    // 2. símbolos 
    public enum Simbolo {
        MOLESTAR,
        PREMIO,
        ALIMENTAR
    }

    private SimpleState _estadoActual;
    private SimpleState _hambriento, _feliz, _enojado, _dormido;

    // Start is called before the first frame update
    void Start()
    {
        // 1. estados
        _hambriento = new SimpleState(Estado.HAMBRIENTO);
        _feliz = new SimpleState(Estado.FELIZ);
        _enojado = new SimpleState(Estado.ENOJADO);
        _dormido = new SimpleState(Estado.DORMIDO);

        // 3. función de transferencia
        _hambriento.AgregarRelacion(Simbolo.ALIMENTAR, _feliz);

        _feliz.AgregarRelacion(Simbolo.MOLESTAR, _enojado);
        _feliz.AgregarRelacion(Simbolo.ALIMENTAR, _dormido);

        _enojado.AgregarRelacion(Simbolo.PREMIO, _feliz);

        _dormido.AgregarRelacion(Simbolo.MOLESTAR, _enojado);
        _dormido.AgregarRelacion(Simbolo.PREMIO, _hambriento);


        // 4. estado inicial 
        _estadoActual = _feliz;
        EjecutarLogicaPorEstado();
    }

    // Update is called once per frame
    void Update()
    {
        // ¿cómo determinar que un estímulo fue aplicado?
        // ahorita con teclazos
        // corutinas que estén revisando variables específicas (vida, distancia, etc)

        if(Input.GetKeyUp(KeyCode.M))
        {
            CambiarEstado(Simbolo.MOLESTAR);
        }

        if(Input.GetKeyUp(KeyCode.P))
        {
            CambiarEstado(Simbolo.PREMIO);
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            CambiarEstado(Simbolo.ALIMENTAR);
        }
    }

    private void CambiarEstado(Simbolo simbolo)
    {

        _estadoActual = _estadoActual.AplicarSimbolo(simbolo);
        EjecutarLogicaPorEstado();
    }

    private void EjecutarLogicaPorEstado()
    {
        // implementación super básica de estados - usando un switch
        // ahorita vamos a imprimir en update pero puede iniciar una corutina 
        // habilitar / deshabilitar un script
        switch(_estadoActual.Nombre) {

            case Estado.DORMIDO:
                // aquí puedes:
                // 1. terminar viejas corutinas y empezar nuevas
                // 2. deshabilitar viejos componentes y habilitar nuevos
                // 3. destruir viejos componentes y agregar nuevos 
                // 4. simplemente correr código aquí directamente
                // gameObject.AddComponent(typeof(DormidoBehaviour));
                print("DORMIDO");
                break;
            case Estado.ENOJADO:

                print("ENOJADO");
                break;
            case Estado.FELIZ:

                print("FELIZ");
                break;
            case Estado.HAMBRIENTO:
                print("HAMBRIENTO");
                break;
        }
    }
}
