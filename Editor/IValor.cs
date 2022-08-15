using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItIsNotOnlyMe.InverseKinematics
{
    public interface IValor
    {
        public IValor Sumar(IValor valor);

        public IValor Multiplicar(float valor);

        public float Modulo();
    }
}
