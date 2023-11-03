using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol
{
    public string Nombre 
    {
        get;
        private set;
    }

    public Symbol(string nombre)
    {
        Nombre = nombre;
    }
}
