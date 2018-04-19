using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>, IManager<Enemy>{
    private int _id = 0;
    [SerializeField]
    private List<Enemy> _enemies = new List<Enemy>();

    public IEnumerable GetAll() { return _enemies.ToArray(); }

    public Enemy Get(int id)
    {
		if (_enemies.Count == 0) throw new NullReferenceException();
        foreach( var enemy in _enemies)
        {
            if (enemy.Id == id) return enemy;
        }
        return null;
    }

    public void Add(Enemy enemy)
    {
		if (enemy == null) throw new ArgumentNullException(enemy.name);
        enemy.Id = _id;
        _id++;
        _enemies.Add(enemy);
    }
    
    public void Remove(int id)
    {
		if (_enemies.Count == 0) throw new NullReferenceException();
        Enemy enemy = Get(id);
        if(enemy == null) throw new ArgumentOutOfRangeException("Can't find Enemy");
         _enemies.Remove(enemy);
        enemy.DestroySelf();
    }

    public void RemoveAll()
    {
        foreach(var enemy in _enemies.ToArray())
        {
            enemy.DestroySelf();
        }
        _enemies.Clear();
        _id = 0;
    }

}