using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ship : Character
{
    [SerializeField] private GameObject _bombPref;
    [SerializeField] private TextMeshProUGUI _textLife;
    [SerializeField] private float _intervalAttack = 1f;
    private bool _isAttack = true;
    [SerializeField] private Submarine _submarine;
    


    void Update()
    {
        Move();
        Attack();
    }


    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_isAttack) 
            { 
                Instantiate(_bombPref, transform.position + new Vector3(0, -0.9f, 0), Quaternion.identity);
                _isAttack = false;
                Invoke("AttackEnable", _intervalAttack);
            }
        }
    }


    private void AttackEnable()
    {
        _isAttack = true;
    }



    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
        
        _textLife.text = "Жизни: " + _life;

        if (_life <= 0)
        {
            Invoke("Restart", 1f);
        }
    }


    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void Move()
    {
        float moveX = 0;
        
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1;
        }


        moveX *= Time.deltaTime;
        transform.Translate(moveX, 0, 0);
        
    }
}
