using UnityEngine;
using ItIsNotOnlyMe.InverseKinematics;

public class ValorPrueba : IValorPrueba
{
    private Vector2 _posicion;
    private float _rotacion;

    public ValorPrueba(Vector2 posicion, float rotacion = 0f)
    {
        Set(posicion, rotacion);
    }

    public void Set(Vector2 posicion, float rotacion)
    {
        _posicion = posicion;
        _rotacion = rotacion;
    }

    public Vector2 Posicion { get => _posicion; }

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
