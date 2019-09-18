using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public delegate void CallBack();

    public event CallBack _canDeploy;
    public event CallBack _timerCallBack;

    public bool bombDeployCheck = true;

    [SerializeField] private float bombTimer;
    [SerializeField] private float particleTimer;
    [SerializeField] private float raycastLength;
    [SerializeField] private GameObject particles;

    private float maxBombTimer;
    private bool particlesOn = false;

    private RaycastHit[] hit;
    private Vector3[] directions;
    private Collider collision;
    private AudioSource audioSource;

    private void Awake()
    {
        _canDeploy = CanDeploy;
        _timerCallBack = ResetTime;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        hit = new RaycastHit[directions.Length];
        collision = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();

        collision.enabled = false;
        maxBombTimer = bombTimer;
    }

    private void Update()
    {
        bombTimer = Timer(bombTimer);

        if (bombTimer < 0)
        {
            Explode();
        }
        else if (bombTimer < 2.5f)
        {
            collision.enabled = true;
        }
            
    }

    private float Timer(float _timer)
    {
        return _timer -= Time.deltaTime;
    }

    public void Deployed()
    {
        if(_canDeploy != null)
        {
            _canDeploy.Invoke();
        }
    }

    private void Explode()
    {
        _canDeploy.Invoke();
        _timerCallBack.Invoke();

        particles.SetActive(true);
        particles.transform.position = transform.position;

        for (int i = 0; i < directions.Length; i++)
        {
            if (Physics.Raycast(transform.position, directions[i], out hit[i], raycastLength))
            {
                Debug.DrawRay(transform.position, directions[i], Color.red);

                try
                {
                    hit[i].collider.GetComponent<IDamagable>().Damage();
                    hit[i].collider.GetComponent<DestructableWall>().waitForDestroy = particleTimer;
                }
                catch
                {

                }

            }
        }
        collision.enabled = false;
        gameObject.SetActive(false);
    }

    private void ResetTime()
    {
        bombTimer = maxBombTimer;
        particles.SetActive(false);
    }

    private void CanDeploy()
    {
        bombDeployCheck = !bombDeployCheck;
    }
}
