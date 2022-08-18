using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionRotacionUnEje : Configuracion
    {
        private static int _cantidadDeVariables = 1;

        private float _rotacion;
        private Vector3 _ejeDeRotacion, _inicioDelAngulo;

        public ConfiguracionRotacionUnEje(float rotacion, Vector3 ejeDeRotacion, Vector3 inicioDelAngulo) 
            : base(_cantidadDeVariables)
        {
            _rotacion = rotacion;
            _ejeDeRotacion = ejeDeRotacion;
            _inicioDelAngulo = inicioDelAngulo;
        }

        protected override float this[int i] { get => _rotacion; set => _rotacion = value; }

        public override void Perturbar(float perturbacion, int numeroVariable)
        {
            this[numeroVariable] += perturbacion * 360;
        }

        public override IValor Transformar(IValor valor)
        {
            Vector3 direccion = Quaternion.AngleAxis(_rotacion, _ejeDeRotacion) * _inicioDelAngulo;
            return (valor as IValorVector).Rotar(direccion);
        }
    }
}
