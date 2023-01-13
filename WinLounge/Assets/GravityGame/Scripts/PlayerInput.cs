using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    Rigidbody2D _playerRB;

	float _maxVelocityNegative = -13f;
	float _maxVelocityPositive = 13f;
	float _jumpForce = 13f;
    bool _isGrounded;
    float _checkRadius = 0.52f;
    public LayerMask whatIsGround;

    Vector3 _positionForRespawn;

    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _positionForRespawn = transform.position;
    }

    void Update()
    {
        if (_playerRB.velocity.y > _maxVelocityPositive)
        {
            if(_playerRB.gravityScale > 0)
                _playerRB.AddForce(new Vector2(0, -1f), ForceMode2D.Impulse);

            if(_playerRB.velocity.y < _maxVelocityNegative)
                _playerRB.AddForce(new Vector2(0, 1f), ForceMode2D.Impulse);
        }
        
        _isGrounded = Physics2D.OverlapCircle(transform.position, _checkRadius, whatIsGround);        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Obstacle")
            Respawn();

        if (collision.gameObject.layer == whatIsGround)
            _playerRB.AddForce(new Vector2(0, -_playerRB.velocity.y), ForceMode2D.Impulse);
	}
	public void ReverseGravity()
    {
        if (_isGrounded)
        {
            _playerRB.gravityScale *= -1;
            
            if (_playerRB.gravityScale > 0)
                _playerRB.AddForce(new Vector2(0, -_jumpForce), ForceMode2D.Impulse);

            if (_playerRB.gravityScale < 0)
                _playerRB.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }

    public void Respawn()
    {
        transform.position = _positionForRespawn;
        if (_playerRB.velocity.y != 0)
            _playerRB.AddForce(new Vector2(0, -_playerRB.velocity.y), ForceMode2D.Impulse);
        
        if (_playerRB.gravityScale < 0)
            _playerRB.gravityScale *= -1;

        FindObjectOfType<ObstacleDown>().Respawn();
        FindObjectOfType<ObstacleUp>().Respawn();
        FindObjectOfType<ScoreUI>().Respawn();
    }
}
