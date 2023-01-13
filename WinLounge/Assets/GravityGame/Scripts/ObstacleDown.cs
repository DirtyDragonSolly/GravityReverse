using UnityEngine;

public class ObstacleDown : MonoBehaviour
{
    public enum WayPoinState
    {
        Positive, Negative
    }

    Rigidbody2D _rb;
    float forceMove = 5f;

    public Vector2 _wayPoint;
    public WayPoinState _wayPointState;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GetNewWaypoint();
    }

    private void Update()
    {
        if (_wayPointState == WayPoinState.Negative)
        {
            if (transform.position.x <= _wayPoint.x)
                GetNewWaypoint();
        }
        if (_wayPointState == WayPoinState.Positive)
        {
            if (transform.position.x >= _wayPoint.x)
                GetNewWaypoint();
        }
		if (transform.position.x >= 10 || transform.position.x <= -10)
			GetNewWaypoint();
        if (_rb.velocity.x != 0)
            _rb.AddForce(new Vector2(-_rb.velocity.x, 0), ForceMode2D.Impulse);
	}

	private void FixedUpdate()
	{
		if (_wayPointState == WayPoinState.Negative)
		{
			transform.Translate(-forceMove * Time.fixedDeltaTime, 0, 0);
		}
		if (_wayPointState == WayPoinState.Positive)
		{
			transform.Translate(forceMove * Time.fixedDeltaTime, 0, 0);
		}
	}

	void GetNewWaypoint()
    {
        if (transform.position.x > 0)
        {
			forceMove = Random.Range(3, 8);
			_wayPointState = WayPoinState.Negative;
            _wayPoint = new Vector2(Random.Range(-9.5f, -4.5f), transform.position.y); 
        }

        else if (transform.position.x < 0)
        {
			forceMove = Random.Range(3, 8);
			_wayPointState = WayPoinState.Positive;
			_wayPoint = new Vector2(Random.Range(4.5f, 9.5f), transform.position.y);
        }
    }

    public void Respawn()
    {
        var random = Random.Range(1,3);
        if (random == 1)        
            transform.position = new Vector3(Random.Range(-9.5f, -4.5f), transform.position.y, 0);            
        
        else if(random == 2)   
			transform.position = new Vector3(Random.Range(4.5f, 9.5f), transform.position.y, 0);
		
		GetNewWaypoint();
	}
}
