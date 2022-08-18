using ItIsNotOnlyMe.InverseKinematics;

public class ConfiguracionRotacionPrueba : Configuracion
{
    private static int _cantidadDeVariables = 1;

    private float _rotacion;

    public ConfiguracionRotacionPrueba(float rotacion) 
        : base(_cantidadDeVariables)
    {
        _rotacion = rotacion;
    }

    public float Rotacion { get => _rotacion; }

    protected override float this[int i] { get => _rotacion; set => _rotacion = value; }

    public override void Perturbar(float perturbacion, int numeroVariable)
    {
        this[numeroVariable] += perturbacion;
    }

    public override IValor Transformar(IValor valor)
    {
        return (valor as IValorPrueba).Rotar(_rotacion);
    }
}
