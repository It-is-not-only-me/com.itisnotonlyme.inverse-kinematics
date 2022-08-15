namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionRotacionUnEje : Configuracion<float>
    {
        private static int _cantidadDeVariables = 1;

        public ConfiguracionRotacionUnEje(float valor, ITransformacion<float> transformacion) 
            : base(valor, transformacion, _cantidadDeVariables)
        {
        }

        protected override float this[int i] { get => _valor; set => _valor = value; }
    }
}
