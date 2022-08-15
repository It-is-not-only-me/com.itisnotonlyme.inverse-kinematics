namespace ItIsNotOnlyMe.InverseKinematics
{
    public interface IConfiguracion
    {
        public void GuardarGradiente(IFuncionMinimizar funcionAMinimizar, float evaluacionAnterior, float perturbacion);

        public void AplicarGradiente(float multiplicador);
    }
}
