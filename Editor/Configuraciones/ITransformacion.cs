namespace ItIsNotOnlyMe.InverseKinematics
{
    public interface ITransformacion<TTipo>
    {
        public void ActualizarEstado(TTipo valor);

        public IValor Transformar(IValor valor);
    }
}
