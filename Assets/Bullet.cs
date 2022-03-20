using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public int bulletDamage = 1;
    public int headshot = 0;
    public static List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count != 0)
        {
            //Debug.Log(damageCd);

            
        }
    }
    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player" && enemies.Contains(col.gameObject) == false)
        {
            col.gameObject.GetComponent<Health>().takeDamage(bulletDamage+headshot);
            //col.gameObject.GetComponent<EnemyCombatScript>().takedamageoverTime(fireballtimeDamage, dmgforSeconds, 1f);
            Destroy(gameObject);
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            enemies.Remove(col.gameObject);
        }
    }
    IEnumerator damageEnemies()
    {
        foreach (GameObject gameObject in enemies)
        {
            gameObject.GetComponent<Health>().takeDamage(bulletDamage);
        }
        Debug.Log(enemies.Count);
        
        Debug.Log("damaged");
        yield return null;       
    }
}
