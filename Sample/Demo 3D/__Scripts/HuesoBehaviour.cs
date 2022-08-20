using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItIsNotOnlyMe.InverseKinematics;
using UnityEditor;

public class HuesoBehaviour : MonoBehaviour
{
    [SerializeField] private Quaternion _direccion;

    [Space(24)]

    [SerializeField] private float _longitud;
    
    private HuesoBehaviour _huesoAnterior;

    private DatoVerificado<Hueso3D> _huesoActual;
    private DatoVerificado<Vector3> _posicion;
    private DatoVerificado<Quaternion> _rotacion;

    public Hueso3D Hueso { get => _huesoActual.Verificado ? _huesoActual.Valor : ObtenerHueso(); }
    public Vector3 Posicion { get => _posicion.Verificado ? _posicion.Valor : ObtenerPosicion(); }
    public Quaternion Rotacion { get => _rotacion.Verificado ? _rotacion.Valor : ObtenerRotacion(); }

    public Vector3 Extension 
    {
        get
        {
            if (!_huesoActual.Verificado)
                ObtenerHueso();
            return _huesoActual.Valor.Extremo();
        }
    }

    private void Awake()
    {
        RecalcularHuesoAnterior();
    }

    [ContextMenu("Recalcular hueso anterior")]
    private void RecalcularHuesoAnterior()
    {
        if (!transform.parent.TryGetComponent<HuesoBehaviour>(out _huesoAnterior))
            _huesoAnterior = null;
        _huesoActual.Desactualizado();
    }

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
            nuevoHueso = new Hueso3D(transform.position, _direccion, _longitud);
        else
            nuevoHueso = new Hueso3D(_huesoAnterior.Hueso, _direccion, _longitud);

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
        return _rotacion.Valor;
    }

    private void OnDrawGizmos()
    {
        Desactualizar();
        Vector3 posicion = Posicion;
        Vector3 extension = Hueso.Extremo();
        Quaternion rotacion = Rotacion;

        DibujarLinea(posicion, extension, rotacion);
    }

    private void DibujarLinea(Vector3 inicio, Vector3 final, Quaternion rotacion)
    {
        rotacion.ToAngleAxis(out float _, out Vector3 direccion);

        Gizmos.DrawLine(inicio, final);
    }
}
