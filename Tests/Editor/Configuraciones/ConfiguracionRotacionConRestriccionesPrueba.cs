using ItIsNotOnlyMe.InverseKinematics;
using UnityEngine;

public class ConfiguracionRotacionConRestriccionesPrueba : Configuracion
{
    private static int _cantidadDeVariables = 1;
    private static float _anguloMinimo = 0f, _anguloMaximo = 2 * Mathf.PI;

    private float _rotacion;
    private float _rotacionMinima, _rotacionMaxima;

    public ConfiguracionRotacionConRestriccionesPrueba(float rotacion, float rotacionMinima, float rotacionMaxima)
        : base(_cantidadDeVariables)
    {
        _rotacion = rotacion;
        _rotacionMinima = rotacionMinima;
        _rotacionMaxima = rotacionMaxima;
    }

    public float Rotacion { get => _rotacion; }

    protected override float this[int i] 
    { 
        get => _rotacion;
        set 
        {
            float valorNuevo = MantenerEnRango(value);
            _rotacion = Mathf.Max(_rotacionMinima, Mathf.Min(_rotacionMaxima, valorNuevo));
        }
    }

    private float MantenerEnRango(float valor)
    {
        while (!EnRango(valor))
        {
            if (valor < _anguloMinimo)
                valor += _anguloMaximo;
            if (_anguloMaximo < valor)
                valor -= _anguloMaximo;
        }

        return valor;
    }

    public bool EnRango(float valor)
    {
        return _anguloMinimo < valor && valor < _anguloMaximo;
    }

    public override void Perturbar(float perturbacion, int numeroVariable)
    {
        this[numeroVariable] += perturbacion;
    }

    public override IValor Transformar(IValor valor)
    {
        return (valor as IValorPrueba).Rotar(_rotacion);
    }
}
