using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class  Spawn_item : MonoBehaviour
{
    #region Fields	
    public bool spawnItems = true;
    [SerializeField] private float _minSpawnTime = 6;
    [SerializeField] private float _maxSpawnTime = 10;
    [SerializeField] private List<Items> _spawnList;
    [SerializeField] private Transform player;
    private float _nextSpawnTime;
    private float _cronoTime = 0;
    #endregion

    #region Unity Callbacks

    void Start()
    {
        ResetTime();
    }

    void Update()
    {
        if (spawnItems == true)
        { 
        _cronoTime += Time.deltaTime;
        if (_cronoTime > _nextSpawnTime)
        {
            SpawnItem();
            ResetTime();
        }
        }
    }

    #region Private Methods
    private void ResetTime()
    {
        _cronoTime = 0;
        _nextSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
    }

    private void SpawnItem()
    {
        //Random object from a list
        int index = Random.Range(0, _spawnList.Count);

        //Esto selecciona una posicion aleatoria siguiendo al jugador
        float xPos = player.position.x + Random.Range(-10f, 10f);
        float yPos = Random.Range(player.position.y - 1f, player.position.y + 3f);
        Vector2 itemPosition = new Vector2(xPos, yPos);

        //Revisar que no haya colision con el suelo o el jugador antes de instanciar el objeto y destruirlo
        Collider2D hitCollider = Physics2D.OverlapCircle(itemPosition, 0.5f, LayerMask.GetMask("Ground", "Player"));
        if (hitCollider != null)
        {
           return;
        }

        //Instantiation
        Items newItem = Instantiate(_spawnList[index], itemPosition, Quaternion.identity);

        //Dificulty Progression
        if (_maxSpawnTime > _minSpawnTime)
            _maxSpawnTime -= 0.1f;
    }
    #endregion

    #endregion

}
