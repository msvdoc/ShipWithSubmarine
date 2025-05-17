using UnityEngine;


public class Submarine : Character
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private GameObject _torpeda;
    [SerializeField] private float _intervalTime = 2f;
    private float _curTime = 0f;


    void Start()
    {
        _controller = FindAnyObjectByType<GameController>();
        Debug.Log("Субмарина");
    }


    private void Attack()
    {
        if (_life > 0)
        {
            _curTime += Time.deltaTime;

            if (_curTime > _intervalTime)
            {
                _curTime = 0;
                Instantiate(_torpeda, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }
        }

    }


    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);

        if (_life <= 0)
        {
            _controller.IncraseScore();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            Invoke("Follout", 5f);
        }
    }


    private void Follout()
    {
        _life = 3;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<PolygonCollider2D>().enabled = true;
    }


    void Update()
    {
        Move();
        Attack();
    }


    private void Move()
    {
        if (_life > 0)
        {
            float moveX = _speed * Time.deltaTime;
            transform.Translate(moveX, 0, 0);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            _speed *= -1;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
    }
}
