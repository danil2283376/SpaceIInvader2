using UnityEngine;

public class Invaders : MonoBehaviour
{
    public delegate void StateWinHadler();
    public static event StateWinHadler WinHandler;

    [SerializeField] private GameObject[] _prefabInvaders;
    [SerializeField] public GameObject ParrentTrash;

    [SerializeField] private int _rows = 0;
    [SerializeField] private int _cols = 0;

    [SerializeField] private float _speedInvader = 0f;
    [SerializeField] private float _spaceForInvaders = 1.0f;
    [SerializeField] private float _stepsMoveRow = 1f;

    [SerializeField] private float _timeShoot = 2f;
    [SerializeField] private float _speedBullet = 0f;

    private Vector3 _directionMove = Vector2.right;
    private GameObject[,] _invaiders;
    private int _countInvaders;
    private int _countKill = 0;
    private int _countAlive = 0;

    private readonly int _layerBulletEnemy = 7;

    public int CountKill
    {
        get
        {
            return _countKill;
        }
        set
        {
            if (value <= _countInvaders)
                _countInvaders = value;
        }
    }

    public int CountAlive
    {
        get
        {
            return _countAlive;
        }
        set
        {
            if (_countAlive - value >= 0)
                _countAlive = value;
        }
    }

    private void Start()
    {
        _countInvaders = _rows * _cols;
        _invaiders = new GameObject[_cols, _rows];
        _countAlive = _countInvaders;

        if (_prefabInvaders.Length == 0)
            throw new System.Exception("Префабы ботов не загружены");
        SpawnInvaders();
        InvokeRepeating(nameof(Shoot), _timeShoot, _timeShoot);
    }

    private void Update()
    {
        MovingInvaders();
        CheckWinsPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _layerBulletEnemy)
            Destroy(collision.gameObject);
    }

    private void SpawnInvaders()
    {
        for (int row = 0; row < this._rows; row++)
        {
            float width = _spaceForInvaders * (this._rows - 1);
            float height = _spaceForInvaders * (this._cols - 1);
            Vector2 centerScreen = new Vector2(-width / 2, -height / 2);
            Vector2 posRow = new Vector2(0f, centerScreen.x + (row * _spaceForInvaders));

            for (int col = 0; col < this._cols; col++)
            {
                GameObject invader = Instantiate(_prefabInvaders[row], this.transform);
                _invaiders[col, row] = invader;
                invader.GetComponent<Invader>().Invaders = this;
                invader.GetComponent<Invader>().SpeedBullet = this._speedBullet;
                invader.GetComponent<Invader>().ParrentTrash = this.ParrentTrash;
                Vector3 position = posRow;
                position.x += centerScreen.y + (col * _spaceForInvaders);
                invader.transform.localPosition = position;
            }
        }
    }

    private void MovingInvaders()
    {
        //Debug.Log("Lol");
        this.transform.position += _directionMove * _speedInvader * Time.deltaTime;

        Vector3 leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform invader = this.transform.GetChild(i).gameObject.transform;
            if (!invader.gameObject.activeInHierarchy)
                continue;
            if (_directionMove == Vector3.right && invader.position.x >= rightBorder.x)
                MoveRow();
            if (_directionMove == -Vector3.right && invader.position.x <= leftBorder.x)
                MoveRow();
        }
    }

    private void MoveRow()
    {
        this.transform.position = new Vector3(
            this.transform.position.x,
                this.transform.position.y - _stepsMoveRow,
                    this.transform.position.z);
        this._directionMove *= -1f;
    }

    private void Shoot()
    {
        Invader invader;
        int countAliveInvader = _countInvaders - _countKill;

        //bool _playerFind = false;
        //for (int i = 0; i < _countInvaders; i++)
        //{
        //    //if (!transform.GetChild(i).gameObject.activeInHierarchy)
        //    //    continue;
        //    //invader = transform.GetChild(i).gameObject.GetComponent<Invader>();
        //    if (invader.FindRayCastPlayer())
        //    {
        //        invader.Shoot();
        //        _playerFind = true;
        //    }
        //}
        invader = ImportantObjectFind();
        if (invader == null)
        {
            RandomInvaderShoot();
        }
        else
        {
            invader.Shoot();
        }
    }

    private Invader ImportantObjectFind()
    {
        Invader invader = null;
        for (int row = 0; row < this._rows; row++)
        {
            for (int col = 0; col < this._cols; col++)
            {
                if (!_invaiders[col, row].gameObject.activeInHierarchy)
                    continue;
                invader = _invaiders[col, row].GetComponent<Invader>();
                if (invader.FindRayCastPlayer())
                    return invader;
            }
        }
        if (invader == null)
        {
            for (int row = 0; row < this._rows; row++)
            {
                for (int col = 0; col < this._cols; col++)
                {
                    if (!_invaiders[col, row].gameObject.activeInHierarchy)
                        continue;
                    invader = _invaiders[col, row].GetComponent<Invader>();
                    if (invader.FindRayCastBunker())
                        return invader;
                }
            }
        }
        return null;
    }

    private void RandomInvaderShoot()
    {
        Invader invader;
        for (int col = 0; col < this._cols; col++)
        {
            for (int row = 0; row < this._rows; row++)
            {
                if (Random.value < (1.0f / (float)this._countAlive))
                {
                    invader = _invaiders[col, row].GetComponent<Invader>();
                    invader.Shoot();
                    return;
                }

            }
        }
    }

    private void CheckWinsPlayer() 
    {
        Debug.Log(_countAlive);
        if (_countAlive <= 0)
            PlayerWin();
    }

    private void PlayerWin() 
    {
        Debug.Log("Я тут");
        WinHandler();
        gameObject.SetActive(false);
    }
}