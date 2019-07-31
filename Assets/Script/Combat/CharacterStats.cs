using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    static int killedCountForEnv = 0;
    static int killedCountForPPl = 0;

    public int maxHealth;
	public int attackPoint;
    public int armor;
    public float rotationSpeed;
    public float moveSpeed;
    public float attackRange;
    public int currentHealth;

    public AudioClip[] hurtSound;
    public AudioClip[] deathSound;



    public void SetHealth(int newHealth)
    {
        maxHealth = newHealth;
        currentHealth = newHealth;
    }

    public void SetAttackPoint(int newAttackPoint)
    {
        attackPoint = newAttackPoint;
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }


    public virtual void getDamage(int damage, bool isEnv = true)
    {
        int thisDamage = damage - armor;
        Mathf.Clamp(thisDamage, 0, int.MaxValue);
        currentHealth -= thisDamage;

        var audioSource = GetComponent<AudioSource>();
        if (currentHealth <= 0)
        {
            var oldTag = string.Copy(this.tag);
            if (oldTag == "Player")
            {
                GameObject obj = GameObject.FindGameObjectWithTag("pauseMenu");
                var pauseMenu = obj.GetComponent<MainPauseMenuToggle>();
                pauseMenu.showPauseMenu();
                UnityEngine.UI.Text textComponent = pauseMenu.transform.GetChild(0).GetChild(2).transform.gameObject.GetComponent<UnityEngine.UI.Text>();
                textComponent.text = "You lost";

                //menu fix
                Time.timeScale = 1f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                if (isEnv)
                {
                    killedCountForEnv++;
                    Debug.Log("env kill" + killedCountForEnv);
                }
                else
                {
                    killedCountForPPl++;
                    Debug.Log("ppl kill" + killedCountForPPl);
                }
            }
            GetComponent<characterUpdater>().setDeath();
            //temp fix maybe
            if (oldTag == "Player")
                this.tag = "Player";
            int clipNumber = Random.Range(0, deathSound.Length);
            if(clipNumber != deathSound.Length)
                audioSource.PlayOneShot(deathSound[clipNumber]);
        }
        else
        {
            int clipNumber = Random.Range(0, hurtSound.Length);
            if (clipNumber != hurtSound.Length)
                audioSource.PlayOneShot(hurtSound[clipNumber]);
        }
        GameObject blood = Instantiate(Resources.Load("Prefabs/SceneController/BloodSprayFX") as GameObject);
        blood.transform.SetParent(this.transform, false);
        Vector3 newPos = blood.transform.position;
        newPos.y += 1f;
        blood.transform.position = newPos;
        Quaternion newQua = blood.transform.rotation;
        newQua *= Quaternion.Euler(Vector3.up * 180);
        newQua *= Quaternion.Euler(-Vector3.right * 20);
        blood.transform.rotation = newQua;
    }
    public void attackEnemy(GameObject enemy)
    {
        enemy.GetComponent<CharacterStats>().getDamage(attackPoint, false);
    }
    public void addHP(int hp)
	{
		this.currentHealth += hp;
		if (this.currentHealth > this.maxHealth)
			this.currentHealth = this.maxHealth;
	}

}
