using UnityEngine;
using ItIsNotOnlyMe.InverseKinematics;

public class ConfiguracionMovimientoLinealLimitadoPrueba : Configuracion
{
    private static int _cantidadDeVariables = 1;

    private float _distancia;
    private float _distanciaMinima, _distanciaMaxima;

    public ConfiguracionMovimientoLinealLimitadoPrueba(float distancia, float distanciaMinima, float distanciaMaxima) 
        : base(_cantidadDeVariables)
    {
        _distancia = distancia;
        _distanciaMinima = distanciaMinima;
        _distanciaMaxima = distanciaMaxima;
    }

    public float Distancia { get => _distancia; }

    protected override float this[int i] { get => _distancia; set => _distancia = Mathf.Max(_distanciaMinima, Mathf.Min(_distanciaMaxima, value)); }

    public override IValor Transformar(IValor valor)
    {
        return (valor as IValorPrueba).Extender(_distancia);
    }
}
