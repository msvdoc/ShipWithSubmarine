using UnityEngine;


public abstract class Character : MonoBehaviour
{
    [SerializeField] protected int _life = 3;
    protected GameController _controller;


    public virtual void GetDamage(int damage)
    {
        _life -= damage;

/*        if (_life <= 0)
        {
            _controller.IncraseScore();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            Invoke("Follout", 5f);
        }*/
    }
}
