using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public event Action OnDie;
    public event Action<int, int> OnChangeHealth;
    public event Action<int> OnTakeDamage;

    public void Initialize(int p_maxHealth)
    {
        maxHealth = p_maxHealth;
        currentHealth = maxHealth;
    }

    public void ChangeMaxHealth(int p_newValue)
    {
        maxHealth = p_newValue;
    }

    public void AddMaxHealth(int p_newValue)
    {
        maxHealth += p_newValue;
    }
    public void RemoveMaxHealth(int p_newValue)
    {
        maxHealth += p_newValue;
    }

    public void TakeDamage(int p_damage)
    {
        print("Taking damage");
        currentHealth -= p_damage;

        OnTakeDamage?.Invoke(p_damage);
        OnChangeHealth?.Invoke(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            print("dying");
            OnDie?.Invoke();
        }
    }

    public void Heal(int p_healAmount)
    {
        if (currentHealth >= maxHealth)
            return;

        currentHealth += p_healAmount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        OnChangeHealth?.Invoke(maxHealth, currentHealth);
    }

    public void ClampCurrentHealth()
    {
        if (!(currentHealth >= maxHealth))
            return;

        currentHealth = maxHealth;
        OnChangeHealth?.Invoke(maxHealth, currentHealth);
    }

    public void RestoreMaxHealth()
    {
        currentHealth = maxHealth;
        OnChangeHealth?.Invoke(maxHealth, currentHealth);
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}