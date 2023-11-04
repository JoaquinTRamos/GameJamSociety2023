using System;
using UnityEngine;

namespace Guns
{
    public interface IGun
    {
        void Shoot(Vector2 p_dir);
        void Throw();
    }
}