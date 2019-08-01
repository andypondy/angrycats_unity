using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    protected int initialHealth = 0;
    protected int currentHealth = 0;
    protected Transform bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
    }

    public void reset(int health) {
        if (health > 0) {
            this.initialHealth = health;
        }
        this.setIndicator(1f);
        this.currentHealth = this.initialHealth;
    }

    public void setHealth(int health) {
        this.initialHealth = health;
    }

    public void takeDamage(int damageValue) {
        this.currentHealth -= damageValue;
        if (this.currentHealth < 0) {
            this.currentHealth = 0;
        }
        float diff = ((float)this.currentHealth/(float)this.initialHealth);
        this.setIndicator(diff);
    }

    protected virtual void setIndicator(float diff) {
        Debug.Log("Health.cs - should override");
        
    }
}
