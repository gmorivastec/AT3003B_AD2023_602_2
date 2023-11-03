using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// como evitar el copy-paste programming?!
// utilizando herencia :D 

// no lo hemos visto así que voy a copypastear
public class State
{
    public string Nombre 
    {
        get;
        private set;
    }
    
    public Type Behaviour
    {
        get;
        private set;
    }

    // diccionario - 
    // estructura de dato relacional donde un dato se guarda como 
    // en un diccionario real
    // llave -> valor
    private Dictionary<Symbol, State> _transferencia;

    // constructor -
    // método que se llama al momento de la creación de un objeto
    public State(string nombre, Type behaviour)
    {
        Nombre = nombre;
        Behaviour = behaviour;
        _transferencia = new Dictionary<Symbol, State>();
    }

    // métodos para la transferencia 
    // agregar relación
    public void AgregarRelacion(Symbol simbolo, State estado)
    {
        _transferencia.Add(simbolo, estado);
    }

    // aplicar símbolo 
    // ¿qué pasa si yo te mando un estímulo? ¿a dónde te vas?
    public State AplicarSimbolo(Symbol simbolo){
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
