using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using ItIsNotOnlyMe.InverseKinematics;
using UnityEngine;

public class InverseKinematicsTest
{
    private static float _errorAceptable = 0.05f;

    private float _margenDeError = 0.01f;
    private int _cantidadIteraciones = 1000;
    private float _perturbacion = 0.0001f;
    private float _multiplicador = 0.1f;

    private InverseKinematics _inverseKinematics;

    public InverseKinematicsTest()
    {
        _inverseKinematics = new InverseKinematics(_perturbacion, _multiplicador, _cantidadIteraciones, _margenDeError);
    }

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
            sonIguales &= ValoresIguales(primero[i], segundo[i]);
        return sonIguales;
    }

    private bool ValoresIguales(float primero, float segundo)
    {
        return primero + _errorAceptable > segundo && primero - _errorAceptable < segundo;
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

        _inverseKinematics.Aplicar(nodoBase, valorInicial, valorFinal);

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

        _inverseKinematics.Aplicar(nodoBase, valorInicial, valorFinal);

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

        ConfiguracionRotacionPrueba configuracionRotacion = new ConfiguracionRotacionPrueba(Mathf.PI / 2);
        INodo nodoBase = new Nodo(CrearConfiguraciones(configuracionRotacion), nodoFinal);

        IValor valorInicial = new ValorPrueba(Vector2.zero, 0f);
        IValor valorFinal = new ValorPrueba(Vector2.up, 0f);

        _inverseKinematics.Aplicar(nodoBase, valorInicial, valorFinal);

        Assert.AreEqual(1f, configuracionLineal.Longitud);
        Assert.AreEqual(Mathf.PI / 2, configuracionRotacion.Rotacion);

        IValor valorResultado = InverseKinematicSolver.Funcion(nodoBase, valorInicial);
        Assert.IsTrue(VectoresIguales(Vector2.up, (valorResultado as ValorPrueba).Posicion));
    }

    [Test]
    public void Test04ModeloEnLaMismaConfiguracionLograExtenderParaLlegarAPosicionDeseada()
    {
        ConfiguracionDistanciaFijaPrueba configuracionLineal = new ConfiguracionDistanciaFijaPrueba(0.5f);
        INodo nodoFinal = new Nodo(CrearConfiguraciones(configuracionLineal));

        ConfiguracionMovimientoLinealLimitadoPrueba configuracionMovimientoLineal = new ConfiguracionMovimientoLinealLimitadoPrueba(0.25f, 0f, 2f);
        INodo nodoBase = new Nodo(CrearConfiguraciones(configuracionMovimientoLineal), nodoFinal);

        IValor valorInicial = new ValorPrueba(Vector2.zero);
        IValor valorFinal = new ValorPrueba(Vector2.right);

        _inverseKinematics.Aplicar(nodoBase, valorInicial, valorFinal);

        Assert.AreEqual(0.5f, configuracionLineal.Longitud);
        Assert.IsTrue(ValoresIguales(0.5f, configuracionMovimientoLineal.Distancia));

        IValor valorResultado = InverseKinematicSolver.Funcion(nodoBase, valorInicial);
        Assert.IsTrue(VectoresIguales(Vector2.right, (valorResultado as ValorPrueba).Posicion));
    }

    [Test]
    public void Test05ModeloEnLaMismaConfiguracionLograRotarParaLlegarAPosicionDeseada()
    {
        ConfiguracionDistanciaFijaPrueba configuracionLineal = new ConfiguracionDistanciaFijaPrueba(1f);
        INodo nodoFinal = new Nodo(CrearConfiguraciones(configuracionLineal));

        ConfiguracionRotacionPrueba configuracionRotacion = new ConfiguracionRotacionPrueba(Mathf.PI / 2);
        INodo nodoBase = new Nodo(CrearConfiguraciones(configuracionRotacion), nodoFinal);

        IValor valorInicial = new ValorPrueba(Vector2.zero);
        IValor valorFinal = new ValorPrueba(Vector2.right);

        _inverseKinematics.Aplicar(nodoBase, valorInicial, valorFinal);

        Assert.AreEqual(1f, configuracionLineal.Longitud);
        Assert.IsTrue(ValoresIguales(0f, configuracionRotacion.Rotacion));

        IValor valorResultado = InverseKinematicSolver.Funcion(nodoBase, valorInicial);
        Assert.IsTrue(VectoresIguales(Vector2.right, (valorResultado as ValorPrueba).Posicion));
    }

    [Test]
    public void Test06ModeloConDosVariablesLLegaAPosicionDeseada()
    {
        IConfiguracion configuracionLineal = new ConfiguracionMovimientoLinealLimitadoPrueba(0.5f, 0f, 2f);
        INodo nodoFinal = new Nodo(CrearConfiguraciones(configuracionLineal));

        IConfiguracion configuracionRotacion = new ConfiguracionRotacionPrueba(Mathf.PI / 2);
        INodo nodoBase = new Nodo(CrearConfiguraciones(configuracionRotacion), nodoFinal);

        IValor valorInicial = new ValorPrueba(Vector2.zero);
        IValor valorFinal = new ValorPrueba(Vector2.right);

        int cantidadDeIteraciones = _inverseKinematics.Aplicar(nodoBase, valorInicial, valorFinal);
        Debug.Log(cantidadDeIteraciones);

        IValor valorResultado = InverseKinematicSolver.Funcion(nodoBase, valorInicial);
        Assert.IsTrue(VectoresIguales(Vector2.right, (valorResultado as ValorPrueba).Posicion));
    }

    [Test]
    public void Test07ModeloConCuatroVariablesLlegaAPosicionDeseada()
    {
        IConfiguracion configuracionLineal1 = new ConfiguracionMovimientoLinealLimitadoPrueba(1f, 0.75f, 2f);
        INodo nodoFinal = new Nodo(CrearConfiguraciones(configuracionLineal1));

        IConfiguracion configuracionRotacion1 = new ConfiguracionRotacionPrueba(-Mathf.PI / 2);
        INodo nodoIntermedio1 = new Nodo(CrearConfiguraciones(configuracionRotacion1), nodoFinal);

        IConfiguracion configuracionLineal2 = new ConfiguracionMovimientoLinealLimitadoPrueba(1f, 0.75f, 2f);
        INodo nodoIntermedio2 = new Nodo(CrearConfiguraciones(configuracionLineal2), nodoIntermedio1);

        IConfiguracion configuracionRotacion2 = new ConfiguracionRotacionPrueba(Mathf.PI / 2);
        INodo nodoBase = new Nodo(CrearConfiguraciones(configuracionRotacion2), nodoIntermedio2);

        IValor valorInicial = new ValorPrueba(Vector2.zero);
        IValor valorFinal = new ValorPrueba(Vector2.right);

        _inverseKinematics.Aplicar(nodoBase, valorInicial, valorFinal);

        IValor valorResultado = InverseKinematicSolver.Funcion(nodoBase, valorInicial);
        Assert.IsTrue(VectoresIguales(Vector2.right, (valorResultado as ValorPrueba).Posicion));
    }

    [Test]
    public void Test08ModeloConDosVariablesPeroDosNodosFijosLlegaAPosicionDeseada()
    {
        IConfiguracion configuracionLineal1 = new ConfiguracionDistanciaFijaPrueba(1f);
        INodo nodoFinal = new Nodo(CrearConfiguraciones(configuracionLineal1));

        IConfiguracion configuracionRotacion1 = new ConfiguracionRotacionPrueba(-Mathf.PI / 2);
        INodo nodoIntermedio1 = new Nodo(CrearConfiguraciones(configuracionRotacion1), nodoFinal);

        IConfiguracion configuracionLineal2 = new ConfiguracionDistanciaFijaPrueba(1f);
        INodo nodoIntermedio2 = new Nodo(CrearConfiguraciones(configuracionLineal2), nodoIntermedio1);

        IConfiguracion configuracionRotacion2 = new ConfiguracionRotacionPrueba(Mathf.PI / 2);
        INodo nodoBase = new Nodo(CrearConfiguraciones(configuracionRotacion2), nodoIntermedio2);

        IValor valorInicial = new ValorPrueba(Vector2.zero);
        IValor valorFinal = new ValorPrueba(Vector2.right);

        _inverseKinematics.Aplicar(nodoBase, valorInicial, valorFinal);

        IValor valorResultado = InverseKinematicSolver.Funcion(nodoBase, valorInicial);
        Debug.Log((valorResultado as ValorPrueba).Posicion);
    }

    [Test]
    public void Test09ModeloConDiezVariablesPeroDiezNodosFijosLlegaAPosicionDeseada()
    {
        INodo nodoAnterior = null;
        for (int i = 0; i < 10; i++)
        {
            IConfiguracion configuracionLineal = new ConfiguracionDistanciaFijaPrueba(1f);
            INodo nodoFinal = new Nodo(CrearConfiguraciones(configuracionLineal), nodoAnterior);

            float direccion = (i % 2 == 0) ? 1 : -1;
            IConfiguracion configuracionRotacion = new ConfiguracionRotacionPrueba((Mathf.PI / 2) * direccion);
            nodoAnterior = new Nodo(CrearConfiguraciones(configuracionRotacion), nodoFinal);
        }

        IValor valorInicial = new ValorPrueba(Vector2.zero);
        IValor valorFinal = new ValorPrueba(Vector2.right);

        _inverseKinematics.Aplicar(nodoAnterior, valorInicial, valorFinal);

        IValor valorResultado = InverseKinematicSolver.Funcion(nodoAnterior, valorInicial);
        Assert.IsTrue(VectoresIguales(Vector2.right, (valorResultado as ValorPrueba).Posicion));
    }

    [Test]
    public void Test10ModeloConVeinteVariablesConRestriccionesLlegaAPosicionDeseada()
    {
        INodo nodoAnterior = null;
        float anguloMinimo = Mathf.PI / 16;
        float anguloMaximo = (2 * Mathf.PI) - anguloMinimo;

        for (int i = 0; i < 10; i++)
        {
            IConfiguracion configuracionLineal = new ConfiguracionMovimientoLinealLimitadoPrueba(1f, 0.5f, 2f);
            INodo nodoFinal = new Nodo(CrearConfiguraciones(configuracionLineal), nodoAnterior);

            float direccion = (i % 2 == 0) ? 1 : -1;
            IConfiguracion configuracionRotacion = new ConfiguracionRotacionConRestriccionesPrueba((Mathf.PI / 2) * direccion, anguloMinimo, anguloMaximo);
            nodoAnterior = new Nodo(CrearConfiguraciones(configuracionRotacion), nodoFinal);
        }

        IValor valorInicial = new ValorPrueba(Vector2.zero);
        IValor valorFinal = new ValorPrueba(Vector2.right);

        _inverseKinematics.Aplicar(nodoAnterior, valorInicial, valorFinal);

        IValor valorResultado = InverseKinematicSolver.Funcion(nodoAnterior, valorInicial);
        Assert.IsTrue(VectoresIguales(Vector2.right, (valorResultado as ValorPrueba).Posicion));
    }

    [Test]
    public void Test11ModeloConCienVariablesConRestriccionesLlegaAPosicionDeseada()
    {
        INodo nodoAnterior = null;
        for (int i = 0; i < 100; i++)
        {
            IConfiguracion configuracionLineal = new ConfiguracionMovimientoLinealLimitadoPrueba(1.25f, 0.01f, 2f);
            nodoAnterior = new Nodo(CrearConfiguraciones(configuracionLineal), nodoAnterior);
        }

        IValor valorInicial = new ValorPrueba(Vector2.zero);
        IValor valorFinal = new ValorPrueba(Vector2.right);

        _inverseKinematics.Aplicar(nodoAnterior, valorInicial, valorFinal);

        IValor valorResultado = InverseKinematicSolver.Funcion(nodoAnterior, valorInicial);
        Assert.IsTrue(VectoresIguales(Vector2.right, (valorResultado as ValorPrueba).Posicion));
    }
}
