namespace ItIsNotOnlyMe.InverseKinematics
{
    public interface IConfiguracion
    {
        public void Perturbar(float valor);

        public void GuardarGradiente(float gradiente);

        public void AplicarGradiente(float multiplicador);
    }
}
