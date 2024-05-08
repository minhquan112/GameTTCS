using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
     
    [SerializeField] int maxHealth;
    public static int currentHealth;
    public HealthBar healthBar;
    public UnityEvent OnDeath;
    public float safeTime = 6f;
    float _safeTimeCoolDown;
    private void OnEnable() {
        // OnDeath.AddListener(Death);
    }

     private void OnDisable() {
        // OnDeath.RemoveListener(Death);
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateBar(currentHealth,maxHealth);
    }

    public void TakeDame(int damge){
        currentHealth-=damge;
        if(currentHealth<=0){
            currentHealth=0;
           OnDeath.Invoke();
        }
        healthBar.UpdateBar(currentHealth,maxHealth);
    }

    public void Death(){
        // Destroy(gameObject);
    }

    void Update()
    {
        _safeTimeCoolDown-=Time.deltaTime;
      if(currentHealth>=10 && Controller.isMove==true)
        {
        TakeDame(10);
        Controller.isMove=false;
        }
       if(_safeTimeCoolDown<=0 && currentHealth<maxHealth){
            _safeTimeCoolDown=safeTime;
            currentHealth+=1;
            healthBar.UpdateBar(currentHealth,maxHealth);
       }
       
    }
    
}
