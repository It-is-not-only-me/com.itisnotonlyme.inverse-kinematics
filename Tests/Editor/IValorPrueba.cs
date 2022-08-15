using ItIsNotOnlyMe.InverseKinematics;

public interface IValorPrueba : IValor
{
    public IValor Extender(float distancia);

    public IValor Rotar(float angulo);
}
