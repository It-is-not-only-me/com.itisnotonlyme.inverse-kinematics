using System;
using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionRotacion : Configuracion
    {
        private static int _cantidadDeVariables = 3;
        private static int _desfaseVariable = 1;

        private Quaternion _direccion;


        public ConfiguracionRotacion(Quaternion direccionInicial) 
            : base(_cantidadDeVariables)
        {
            _direccion = direccionInicial;
        }

        protected override float this[int i] 
        { 
            get => _direccion[i + _desfaseVariable]; 
            set
            {
                _direccion[i + _desfaseVariable] = value;
                _direccion.Normalize();
            }  
        }

        public override void Perturbar(float perturbacion, int numeroVariable)
        {
            this[numeroVariable + _desfaseVariable] += perturbacion;
            _direccion.Normalize();
        }

        public override IValor Transformar(IValor valor)
        {
            return (valor as IValorVector).Rotar(_direccion);
        }
    }
}
