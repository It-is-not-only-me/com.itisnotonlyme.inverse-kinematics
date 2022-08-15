using ItIsNotOnlyMe.InverseKinematics;

public class ConfiguracionRotacionConPreferenciaPrueba : Configuracion
{
    private static int _cantidadDeVariables = 1;
    private static float _anguloMinimo, _anguloMaximo;

    private float _rotacion;
    private float _anguloPreferencia, _pesoDeInfluencia;

    public ConfiguracionRotacionConPreferenciaPrueba(float rotacion, float anguloPreferencia, float pesoDeInfluencia)
        : base(_cantidadDeVariables)
    {
        _rotacion = rotacion;

        _anguloPreferencia = anguloPreferencia;
        _pesoDeInfluencia = pesoDeInfluencia;
    }

    public float Rotacion { get => _rotacion; }

    protected override float this[int i] 
    { 
        get => _rotacion;
        set
        {
            float valorNuevo = MantenerEnRango(value);

            float distanciaAlObjetivoPositivo = _anguloMaximo - valorNuevo + _anguloPreferencia;
            float distanciaAlObjetivoNegativo = valorNuevo - _anguloPreferencia;

            float direccion = (distanciaAlObjetivoPositivo < distanciaAlObjetivoNegativo) ? 1f : -1f;

            _rotacion = MantenerEnRango((valorNuevo + _anguloPreferencia * _pesoDeInfluencia * direccion) / (1 + _pesoDeInfluencia));
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

    private bool EnRango(float valor)
    {
        return _anguloMinimo < valor && valor < _anguloMaximo;
    }

    public override IValor Transformar(IValor valor)
    {
        return (valor as IValorPrueba).Rotar(_rotacion);
    }
}
