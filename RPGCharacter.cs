using UnityEngine;
using System;

[System.Serializable]
public class CharacterStats
{
    public string characterName;

    [Header("Core Stats")]
    public int maxHealth = 100;
    public int attackPower = 20;
    public int defensePower = 10;

    [Header("Energy Settings")]
    public int maxEnergy = 100;
    public int energyPerTurn = 20;

    [Header("Action Costs")]
    public int normalAttackCost = 10;
    public int defendCost = 20;
    public int uniqueSkillCost = 50;
}

public class RPGCharacter : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;

    public int CurrentHealth { get; private set; }
    public int CurrentEnergy { get; private set; }
    public bool IsDefending { get; private set; }

    public event Action<RPGCharacter> OnCharacterDefeated;

    private void Awake()
    {
        CurrentHealth = stats.maxHealth;
        CurrentEnergy = stats.maxEnergy;
    }

    //Energy Management
    public void StartTurn()
    {
        AddEnergy(stats.energyPerTurn);
    }

    private void AddEnergy(int amount)
    {
        CurrentEnergy = Mathf.Min(CurrentEnergy + amount, stats.maxEnergy);
    }

    private bool SpendEnergy(int cost)
    {
        if (CurrentEnergy < cost)
            return false;

        CurrentEnergy -= cost;
        return true;
    }
    //End Energy Management

    //Actions
    public bool NormalAttack(RPGCharacter target)
    {
        if (!SpendEnergy(stats.normalAttackCost))
            return false;

        int damage = stats.attackPower;
        target.TakeDamage(damage);
        return true;
    }

    public bool Defend()
    {
        if (!SpendEnergy(stats.defendCost))
            return false;

        IsDefending = true;
        return true;
    }

    //Template method for unique abilities.
    public virtual bool UseUniqueSkill(RPGCharacter target)
    {
        if (!SpendEnergy(stats.uniqueSkillCost))
            return false;
            
        // target.TakeDamage(stats.attackPower * 2);

        return true;
    }
    //End Actions

    //Damage System
    public void TakeDamage(int incomingDamage)
    {
        int mitigated = incomingDamage - stats.defensePower;

        if (IsDefending)
        {
            mitigated = Mathf.RoundToInt(mitigated * 0.5f);
            IsDefending = false;
        }

        mitigated = Mathf.Max(1, mitigated);

        CurrentHealth -= mitigated;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            OnCharacterDefeated?.Invoke(this);
        }
    }
    //End Damage System
}
