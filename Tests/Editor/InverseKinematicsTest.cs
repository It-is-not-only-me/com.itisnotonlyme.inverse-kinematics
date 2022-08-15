using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using ItIsNotOnlyMe.InverseKinematics;
using UnityEngine;

public class InverseKinematicsTest
{
    private float _margenDeError = 0.0001f;
    private int _cantidadIteraciones = 10;
    private float _perturbacion = 0.01f;
    private float _multiplicador = 5f;

    private IConfiguracion[] CrearConfiguraciones(IConfiguracion c1, IConfiguracion c2 = null, IConfiguracion c3 = null)
    {
        int cantidadDeConfiguraciones = 1;
        if (c2 != null) cantidadDeConfiguraciones++;
        if (c3 != null) cantidadDeConfiguraciones++;

        IConfiguracion[] configuraciones = new IConfiguracion[cantidadDeConfiguraciones];
        configuraciones[0] = c1;
        if (c2 != null) configuraciones[1] = c2;
        if (c3 != null) configuraciones[2] = c3;

        return configuraciones;
    }

    private bool VectoresIguales(Vector2 primero, Vector2 segundo)
    {
        bool sonIguales = true;
        for (int i = 0; i < 2; i++)
            sonIguales &= primero[i] + _margenDeError > segundo[i] && primero[i] - _margenDeError < segundo[i];
        return sonIguales;
    }

    [Test]
    public void Test01ModeloEnPosicionAcetableNoSeModifica()
    {
        ConfiguracionDistanciaFijaPrueba configuracionLineal = new ConfiguracionDistanciaFijaPrueba(1f);
        INodo nodoFinal = new Nodo(CrearConfiguraciones(configuracionLineal));

        ConfiguracionRotacionPrueba configuracionRotacion = new ConfiguracionRotacionPrueba(0f);
        INodo nodoBase = new Nodo(CrearConfiguraciones(configuracionRotacion), nodoFinal);

        IValor valorInicial = new ValorPrueba(Vector2.zero, 0f);
        IValor valorFinal = new ValorPrueba(Vector2.right, 0f);

        InverseKinematicSolver.Aplicar(nodoBase, valorInicial, valorFinal, _perturbacion, _multiplicador, _cantidadIteraciones, _margenDeError);

        Assert.AreEqual(1f, configuracionLineal.Longitud);
        Assert.AreEqual(0f, configuracionRotacion.Rotacion);

        IValor valorResultado = InverseKinematicSolver.Funcion(nodoBase, valorInicial);
        Assert.IsTrue(VectoresIguales(Vector2.right, (valorResultado as ValorPrueba).Posicion));
    }

    [Test]
    public void Test02ModeloEnPosicionAceptablePeroIntercambiadoDeOrdenNoSeModifica()
    {
        ConfiguracionRotacionPrueba configuracionRotacion = new ConfiguracionRotacionPrueba(0f);
        INodo nodoFinal = new Nodo(CrearConfiguraciones(configuracionRotacion));

        ConfiguracionDistanciaFijaPrueba configuracionLineal = new ConfiguracionDistanciaFijaPrueba(1f);
        INodo nodoBase = new Nodo(CrearConfiguraciones(configuracionLineal), nodoFinal);

        IValor valorInicial = new ValorPrueba(Vector2.zero);
        IValor valorFinal = new ValorPrueba(Vector2.right);

        InverseKinematicSolver.Aplicar(nodoBase, valorInicial, valorFinal, _perturbacion, _multiplicador, _cantidadIteraciones, _margenDeError);

        Assert.AreEqual(1f, configuracionLineal.Longitud);
        Assert.AreEqual(0f, configuracionRotacion.Rotacion);

        IValor valorResultado = InverseKinematicSolver.Funcion(nodoBase, valorInicial);
        Assert.IsTrue(VectoresIguales(Vector2.right, (valorResultado as ValorPrueba).Posicion));
    }

    [Test]
    public void Test03ModeloEnPosicionAceptableConOtraPosicionFinal()
    {
        ConfiguracionDistanciaFijaPrueba configuracionLineal = new ConfiguracionDistanciaFijaPrueba(1f);
        INodo nodoFinal = new Nodo(CrearConfiguraciones(configuracionLineal));

        ConfiguracionRotacionPrueba configuracionRotacion = new ConfiguracionRotacionPrueba(90f);
        INodo nodoBase = new Nodo(CrearConfiguraciones(configuracionRotacion), nodoFinal);

        IValor valorInicial = new ValorPrueba(Vector2.zero, 0f);
        IValor valorFinal = new ValorPrueba(Vector2.up, 0f);

        InverseKinematicSolver.Aplicar(nodoBase, valorInicial, valorFinal, _perturbacion, _multiplicador, _cantidadIteraciones, _margenDeError);

        Assert.AreEqual(1f, configuracionLineal.Longitud);
        Assert.AreEqual(90f, configuracionRotacion.Rotacion);

        IValor valorResultado = InverseKinematicSolver.Funcion(nodoBase, valorInicial);
        Assert.IsTrue(VectoresIguales(Vector2.up, (valorResultado as ValorPrueba).Posicion));
    }

    [Test]
    public void Test04ModeloEnLaMismaConfiguracionLograRotarParaLlegarAPosicionDeseada()
    {
        ConfiguracionDistanciaFijaPrueba configuracionLineal = new ConfiguracionDistanciaFijaPrueba(1f);
        INodo nodoFinal = new Nodo(CrearConfiguraciones(configuracionLineal));

        ConfiguracionRotacionPrueba configuracionRotacion = new ConfiguracionRotacionPrueba(90f);
        INodo nodoBase = new Nodo(CrearConfiguraciones(configuracionRotacion), nodoFinal);

        IValor valorInicial = new ValorPrueba(Vector2.zero);
        IValor valorFinal = new ValorPrueba(Vector2.right);

        InverseKinematicSolver.Aplicar(nodoBase, valorInicial, valorFinal, _perturbacion, _multiplicador, _cantidadIteraciones, _margenDeError);

        Assert.AreEqual(1f, configuracionLineal.Longitud);
        Assert.AreEqual(0f, configuracionRotacion.Rotacion);
    }
}
