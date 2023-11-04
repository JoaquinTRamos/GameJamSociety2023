using System.Numerics;

interface IDamageable
{
    void Damage(int damage, Element element, Vector3 direction);
}
