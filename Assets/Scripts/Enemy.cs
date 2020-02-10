using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    Player player;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        player = GameObject.Find("Player").GetComponent<Player>();
        if(transform.position.y <= -5.5f)
        {
            transform.position = new Vector3(Random.Range(-8f,8f), 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            if(player != null)
            {
                player.AddScore(10);
            }
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
