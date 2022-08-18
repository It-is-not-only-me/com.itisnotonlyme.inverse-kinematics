using System;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionLongitudFija : Configuracion
    {
        private static int _cantidadDeVariables = 0;

        private float _longitud;

        public ConfiguracionLongitudFija(float longitud)
            : base(_cantidadDeVariables)
        {
            _longitud = longitud;
        }

        protected override float this[int i] { get => throw new ArgumentOutOfRangeException(); set => throw new ArgumentOutOfRangeException(); }

        public override void Perturbar(float perturbacion, int numeroVariable)
        {
            this[numeroVariable] += perturbacion;
        }

        public override IValor Transformar(IValor valor)
        {
            return (valor as IValorVector).Extender(_longitud);
        }
    }
}
