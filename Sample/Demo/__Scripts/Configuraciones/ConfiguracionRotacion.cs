﻿using System;
using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public class ConfiguracionRotacion : Configuracion
    {
        private static int _cantidadDeVariables = 3;

        private Vector3 _direccion;

        public ConfiguracionRotacion(Vector3 direccionInicial) 
            : base(_cantidadDeVariables)
        {
            _direccion = direccionInicial;
        }

        protected override float this[int i] { get => _direccion[i]; set => _direccion[i] = value; }

        public override void Perturbar(float perturbacion, int numeroVariable)
        {
            this[numeroVariable] += perturbacion * 360;
        }

        public override IValor Transformar(IValor valor)
        {
            return (valor as IValorVector).Rotar(_direccion);
        }
    }
}
