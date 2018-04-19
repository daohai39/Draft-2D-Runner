using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : Singleton<WallManager>, IManager<Wall> {
	private int _id = 0;

	[SerializeField]
	private List<Wall> _walls = new List<Wall>();

	public IEnumerable GetAll() { return _walls.ToArray(); }

	public Wall Get(int id)
	{
		if (_walls.Count == 0) throw new NullReferenceException();
		foreach (var wall in _walls)
		{
			if (wall.Id == id) 
				return wall;
		}
		return null;
	}

	public void Add(Wall wall)
	{
		if (wall == null) throw new ArgumentNullException(wall.name);
		wall.Id = _id;
		_id++;
		_walls.Add(wall);
	}

	public void Remove(int id)
	{
		if (_walls.Count == 0) throw new NullReferenceException();
		Wall wall = Get(id);
		if (wall == null) throw new ArgumentOutOfRangeException("Can't find wall");
		_walls.Remove(wall);
		wall.DestroySelf();
	}

	public void RemoveAll()
	{
		foreach (var wall in _walls.ToArray())
		{
			wall.DestroySelf();
		}
		_walls.Clear();
		_id = 0;
	}
}
