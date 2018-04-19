using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PointsManager : Singleton<PointsManager>, IManager<Point> {
	private int _id = 0;

	[SerializeField]
	private List<Point> _points = new List<Point>();
	

	protected PointsManager() {}
	
	public IEnumerable GetAll() { return _points.ToArray(); }

	public Point Get(int id)
	{
		if (_points.Count == 0) throw new NullReferenceException();
		for (var i = 0 ; i < _points.Count; i++)
		{
			if (_points[i].Id == id) 
			 return _points[i]; 
		}
		return null;
	} 

	public void Add(Point point)
	{
		if(point == null) throw new ArgumentNullException(point.name);
		point.Id = _id;
		_id++;
		_points.Add(point);
	}

	public void Remove(int id)
	{
		if (_points.Count == 0) throw new NullReferenceException();
		Point point = Get(id);
		if (point == null) throw new ArgumentOutOfRangeException("Can not find point");
		_points.Remove(point);
		point.DestroySelf();
	}

	public void RemoveAll()
	{
		foreach(var point in _points.ToArray())
		{
			point.DestroySelf();
		}
		_points.Clear();
		_id = 0;
	}
	
}
