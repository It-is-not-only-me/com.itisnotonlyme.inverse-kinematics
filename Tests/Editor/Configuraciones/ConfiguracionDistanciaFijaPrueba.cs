using ItIsNotOnlyMe.InverseKinematics;

public class ConfiguracionDistanciaFijaPrueba : Configuracion
{
    private static int _cantidadDeVariables = 0;

    private float _longitud;

    public ConfiguracionDistanciaFijaPrueba(float longitud)
        : base(_cantidadDeVariables)
    {
        _longitud = longitud;
    }

    public float Longitud { get => _longitud; }

    protected override float this[int i] { get => throw new System.IndexOutOfRangeException(); set => throw new System.IndexOutOfRangeException(); }

    public override void Perturbar(float perturbacion, int numeroVariable)
    {
        this[numeroVariable] += perturbacion;
    }

    public override IValor Transformar(IValor valor)
    {
        return (valor as IValorPrueba).Extender(_longitud);
    }
}
