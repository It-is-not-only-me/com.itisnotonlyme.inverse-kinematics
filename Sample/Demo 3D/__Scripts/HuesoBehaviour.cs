using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItIsNotOnlyMe.InverseKinematics;
using UnityEditor;

public class HuesoBehaviour : MonoBehaviour
{
    [SerializeField] float _longitud;
    [SerializeField] HuesoBehaviour _huesoAnterior;

    private DatoVerificado<Hueso3D> _huesoActual;
    private DatoVerificado<Vector3> _posicion;
    private DatoVerificado<Quaternion> _rotacion;

    public Hueso3D Hueso { get => _huesoActual.Verificado ? _huesoActual.Valor : ObtenerHueso(); }
    public Vector3 Posicion { get => _posicion.Verificado ? _posicion.Valor : ObtenerPosicion(); }
    public Quaternion Rotacion { get => _rotacion.Verificado ? _rotacion.Valor : ObtenerRotacion(); }

    private void Desactualizar()
    {
        _huesoActual.Desactualizado();
        _posicion.Desactualizado();
        _rotacion.Desactualizado();
    }

    private Hueso3D ObtenerHueso()
    {
        Hueso3D nuevoHueso;
        if (_huesoAnterior == null)
            nuevoHueso = new Hueso3D(transform.position, transform.rotation, _longitud);
        else
            nuevoHueso = new Hueso3D(_huesoAnterior.Hueso, transform.rotation, _longitud);

        _huesoActual.Valor = nuevoHueso;        
        return _huesoActual.Valor;
    }

    private Vector3 ObtenerPosicion()
    {
        _posicion.Valor = Hueso.Posicion;
        transform.position = _posicion.Valor;
        return _posicion.Valor;
    }

    private Quaternion ObtenerRotacion()
    {
        _rotacion.Valor = Hueso.Rotacion;
        transform.rotation = _rotacion.Valor;
        return _rotacion.Valor;
    }

    private void OnDrawGizmos()
    {
        Desactualizar();
        Vector3 posicion = Posicion;
        Vector3 extension = Hueso.Extremo();

        Gizmos.DrawSphere(posicion, 0.5f);
        Gizmos.DrawSphere(extension, 0.5f);

        Gizmos.DrawLine(posicion, extension);
    }
}
