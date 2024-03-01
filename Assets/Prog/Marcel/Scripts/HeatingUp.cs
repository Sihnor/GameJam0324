using UnityEngine;

namespace Prog.Marcel.Scripts
{
    public interface IHeatingUp
    {
        void HeatingUp(float distance, float heatValue = 1.0f);
    }
}