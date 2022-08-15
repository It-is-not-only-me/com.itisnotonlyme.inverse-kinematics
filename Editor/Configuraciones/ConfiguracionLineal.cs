namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionLineal : Configuracion<float>
    {
        private static int _cantidadDeVariables = 1;

        public ConfiguracionLineal(float distancia, ITransformacion<float> transformacion) 
            : base(distancia, transformacion, _cantidadDeVariables)
        {
        }

        protected override float this[int i] { get => _valor; set => _valor = value; }
    }
}
