using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItIsNotOnlyMe.InverseKinematics;

public class ValorVector : IValorVector
{
    private Vector3 _posicion;
    private Quaternion _rotacion;

    public ValorVector(Vector3 posicion, Quaternion rotacion)
    {
        _posicion = posicion;
        _rotacion = rotacion.normalized;
    }

    public float Modulo() => _posicion.magnitude;

    public IValor Multiplicar(float valor) => new ValorVector(_posicion * valor, _rotacion);

    public IValor Sumar(IValor valor) => new ValorVector(_posicion + (valor as ValorVector)._posicion, _rotacion * (valor as ValorVector)._rotacion);

    public IValor Rotar(Quaternion rotar) => new ValorVector(_posicion, (_rotacion * rotar));

    public IValor Extender(float distancia)
    {
        Vector3 direccion = new Vector3(_rotacion.y, _rotacion.z, _rotacion.w);
        return new ValorVector(_posicion + direccion * distancia, _rotacion);
    }
}
