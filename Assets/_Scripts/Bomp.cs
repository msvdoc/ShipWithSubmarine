using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomp : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPref;
    [SerializeField] private int _damage = 1;



    void Start()
    {

        Destroy(gameObject, 8f);
    }



    void Update()
    {
        Move();
    }


    private void Move()
    {
        float speedY;

        if (gameObject.CompareTag("Bomb"))
        {
            speedY = -1 * Time.deltaTime;
        }
        else
        {
            speedY = 2 * Time.deltaTime; 
        }

        transform.Translate(0, speedY, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag + "  " + gameObject.tag);

        if(collision.gameObject.CompareTag("Submarine") && gameObject.CompareTag("Bomb"))
        {
            collision.gameObject.GetComponent<Submarine>().GetDamage(_damage);
            Explosion();
        }
        else if (collision.gameObject.CompareTag("Ship") && gameObject.CompareTag("Torpeda"))
        {
            collision.GetComponent<Ship>().GetDamage(_damage);
            Explosion();
        }
    }



    private void Explosion()
    {
        GameObject explosion = Instantiate(_explosionPref, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
        Destroy(gameObject);
    }

}
