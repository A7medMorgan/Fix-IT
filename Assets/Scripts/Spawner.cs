using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Range(2, 4)] public int time_spawan;
    [Header("must have 2 intial point L,R")]
    public Transform Left;
    public Transform Right;
    public GameObject Prefab;
    [SerializeField]bool do_spawn;
    private float _leftx, _rightx;
    private int _time_count;
    // Start is called before the first frame update
    void Start()
    {
        _leftx = Left.position.x;
        _rightx = Right.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (do_spawn)
        {
            _time_count += 1;
            if (Random.Range(1, time_spawan) * (1f / Time.fixedDeltaTime) < _time_count)
            {
                Instantiate(Prefab, new Vector3(Random.Range(_leftx, _rightx), Left.position.y, 0), Quaternion.identity);
                _time_count = 0;
            }
        }
    }
    public void spawn(bool check)
    {
        do_spawn = check;
    }
}
