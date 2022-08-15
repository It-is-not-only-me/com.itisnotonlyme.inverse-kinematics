using UnityEngine;
using ItIsNotOnlyMe.InverseKinematics;

public interface IValorVector : IValor
{
    public IValor Extender(float distancia);

    public IValor Rotar(Vector3 rotar);
}
