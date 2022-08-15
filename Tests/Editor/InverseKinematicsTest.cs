using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ItIsNotOnlyMe.InverseKinematics;

public class ValorPrueba : IValor
{
    private Vector3 _posicion;

    public ValorPrueba(Vector3 posicion)
    {
        _posicion = posicion;
    }

    public float Modulo() => _posicion.magnitude;

    public IValor Multiplicar(float valor) => new ValorPrueba(_posicion * valor);

    public IValor Sumar(IValor valor)
    {
        ValorPrueba valorPrueba = valor as ValorPrueba;
        return new ValorPrueba(_posicion + valorPrueba._posicion);
    }
}

public class InverseKinematicsTest
{
    [Test]
    public void Test01()
    {
 
    }
}
