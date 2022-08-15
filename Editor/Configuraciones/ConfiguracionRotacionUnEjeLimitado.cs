using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionRotacionUnEjeLimitado : Configuracion<float>
    {
        private static int _cantidadDeVariables = 1;

        private float _valorMinimo, _valorMaximo;

        public ConfiguracionRotacionUnEjeLimitado(float valor, float minimo, float maximo, ITransformacion<float> transformacion)
            : base(valor, transformacion, _cantidadDeVariables)
        {
            _valorMinimo = minimo;
            _valorMaximo = maximo;
        }

        protected override float this[int i]
        {
            get => _valor;
            set
            {
                float limitadoPorMaximo = Mathf.Min(value, _valorMaximo);
                _valor = Mathf.Max(limitadoPorMaximo, _valorMinimo);
            }
        }
    }
}
