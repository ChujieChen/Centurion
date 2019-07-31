using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState { beforeStart, SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform enemy;
		public int count;
		public float rate;
	}

	public Wave[] waves;
	private int nextWave = 0;

    Hashtable waveArmy;

	public int NextWave
	{
		get { return nextWave + 1; }
	}

	public Transform[] spawnPoints;

	public float timeBetweenWaves = 5f;
	private float waveCountdown;
	public float WaveCountdown
	{
		get { return waveCountdown; }
	}

	private float searchCountdown = 1f;

	private SpawnState state = SpawnState.beforeStart; // first state after entering game

	public SpawnState State
	{
		get { return state; }
	}

    // called by GameStarter to start game
    public void StartCountingDown()
    {
        state = SpawnState.COUNTING;
    }

    void Start()
	{
        waveCountdown = 0;
        waveArmy = new Hashtable();
    }

	void Update()
	{
        if (state == SpawnState.beforeStart) return;

		if (state == SpawnState.WAITING)
		{
			if (!isWaveArmyAlive())
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if (waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
				StartCoroutine( SpawnWave ( waves[nextWave] ) );
			}
		}
		else
		{
            waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
		{
			nextWave = 0;
		}
		else
		{
			nextWave++;
		}
	}

	bool isWaveArmyAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (waveArmy.Count == 0)
			{
				return false;
			}
		}
		return true;
	}
    public void onDeath(GameObject o)
    {
        var ID = o.GetInstanceID();
        if (waveArmy.ContainsKey(ID))
            waveArmy.Remove(ID);
    }

	IEnumerator SpawnWave(Wave _wave)
	{
		state = SpawnState.SPAWNING;

		for (int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds( 1f/_wave.rate );
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(Transform _enemy)
	{
		Transform _sp = spawnPoints[ Random.Range (0, spawnPoints.Length) ];

        Transform temp = Instantiate(_enemy, _sp.position, _sp.rotation);

        GameObject enemy = temp.gameObject;
        waveArmy.Add(enemy.GetInstanceID(), enemy);
        enemy.GetComponent<characterUpdater>().switchToAIFirstTime();

        // Set enemy AI stats
        var stat = enemy.GetComponent<CharacterStats>();

        stat.SetHealth(stat.maxHealth / 2);
        stat.SetAttackPoint(stat.attackPoint / 2);

    }

}
