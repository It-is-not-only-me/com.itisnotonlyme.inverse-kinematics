namespace ItIsNotOnlyMe.InverseKinematics
{
    public interface IConfiguracion
    {
        public IValor Transformar(IValor valor);

        public void GuardarGradiente(IFuncionMinimizar funcionAMinimizar, float evaluacionAnterior, float perturbacion);

        public void AplicarGradiente(float multiplicador);
    }
}
