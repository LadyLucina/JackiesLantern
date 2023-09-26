using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem 
{
  //initiate variables
  int currentHealth;
  int currentMaxHealth;

  //defineing and establishing propterties for health value
  public int Health
  {
	get
	{
		return currentHealth;
	}
	set
	{
		currentHealth = value;
	}
  }

  public int MaxHealth
  {
	get
	{
		return currentMaxHealth;
	}
	set
	{
		currentMaxHealth = value;
	}
  }

  //methods of damaging health and regenerating health
  public HealthSystem(int health, int maxHealth)
  {
	currentHealth = health;
	currentMaxHealth = maxHealth;
  }

  public void damageHealth(int damageAmount)
  {
		if (currentHealth > 0)
		{
			currentHealth -= damageAmount;
		}
  }

  public void regenHealth(int healAmount)
  {
	if (currentHealth < currentMaxHealth)
	{
		currentHealth += healAmount;
	}
	if (currentHealth > currentMaxHealth)
	{
		currentHealth = currentMaxHealth;
	}
  }
}
