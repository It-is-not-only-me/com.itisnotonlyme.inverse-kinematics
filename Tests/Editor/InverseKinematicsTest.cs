using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ItIsNotOnlyMe.InverseKinematics;

public interface IValorPrueba : IValor
{
    public IValor Extender(float distancia);

    public IValor Rotar(float angulo);
}

public class ValorPrueba : IValorPrueba
{
    private Vector2 _posicion;
    private float _rotacion;

    public ValorPrueba(Vector2 posicion, float rotacion)
    {
        Set(posicion, rotacion);
    }

    public void Set(Vector2 posicion, float rotacion)
    {
        _posicion = posicion;
        _rotacion = rotacion;
    }

    public float Modulo() => _posicion.magnitude;

    public IValor Multiplicar(float valor) => new ValorPrueba(_posicion * valor, _rotacion);

    public IValor Sumar(IValor valor)
    {
        ValorPrueba valorPrueba = valor as ValorPrueba;
        return new ValorPrueba(_posicion + valorPrueba._posicion, _rotacion + valorPrueba._rotacion);
    }

    public IValor Extender(float distancia)
    {
        float radianes = _rotacion * Mathf.Deg2Rad;
        Vector2 direccion = new Vector2(Mathf.Cos(radianes), Mathf.Sin(radianes));

        return new ValorPrueba(_posicion + direccion * distancia, _rotacion);
    }

    public IValor Rotar(float angulo)
    {
        return new ValorPrueba(_posicion, _rotacion + angulo);
    }
}

public class TransformacionRotacionPrueba : ITransformacion<float>
{
    private float _rotacion;

    public void ActualizarEstado(float rotacion)
    {
        _rotacion = rotacion;
    }

    public IValor Transformar(IValor valor)
    {
        IValorPrueba valorPrueba = valor as IValorPrueba;
        return valorPrueba.Rotar(_rotacion);
    }
}

public class TransformacionExtenderPrueba : ITransformacion<float>
{
    private float _distancia;

    public void ActualizarEstado(float distancia)
    {
        _distancia = distancia;
    }

    public IValor Transformar(IValor valor)
    {
        IValorPrueba valorPrueba = valor as IValorPrueba;
        return valorPrueba.Extender(_distancia);
    }
}

public class InverseKinematicsTest
{
    [Test]
    public void Test01()
    {
 
    }
}
