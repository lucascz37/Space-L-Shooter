using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int powerUpId;
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                switch (powerUpId)
                {
                    case 0:
                        player.ActivateTripleShoot();
                        break;
                    case 1:
                        player.ActivateSpeedPowerUp();
                        break;
                    case 2:
                        player.ActivateShieldPowerUp();
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
