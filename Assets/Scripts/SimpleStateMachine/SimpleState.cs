using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleState 
{
    
    public SimpleStateMachine.Estado Nombre 
    {
        get;
        private set;
    }

    // diccionario - 
    // estructura de dato relacional donde un dato se guarda como 
    // en un diccionario real
    // llave -> valor
    private Dictionary<SimpleStateMachine.Simbolo, SimpleState> _transferencia;

    // constructor -
    // método que se llama al momento de la creación de un objeto
    public SimpleState(SimpleStateMachine.Estado nombre)
    {
        Nombre = nombre;
        _transferencia = new Dictionary<SimpleStateMachine.Simbolo, SimpleState>();
    }

    // métodos para la transferencia 
    // agregar relación
    public void AgregarRelacion(SimpleStateMachine.Simbolo simbolo, SimpleState estado)
    {
        _transferencia.Add(simbolo, estado);
    }

    // aplicar símbolo 
    // ¿qué pasa si yo te mando un estímulo? ¿a dónde te vas?
    public SimpleState AplicarSimbolo(SimpleStateMachine.Simbolo simbolo){
        if(_transferencia.ContainsKey(simbolo))
        {
            return _transferencia[simbolo];
        }
        else 
        {
            return this;
        }
    }
}
