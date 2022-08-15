namespace ItIsNotOnlyMe.InverseKinematics
{
    public interface ITransformacion<TTipo>
    {
        public void ActualizarEstado(TTipo tipo);

        public IValor Transformar(IValor valor);
    }
}
