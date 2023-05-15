using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
	static T _inst = null;
	public static T Inst{
		get
		{
			if(_inst == null)
			{
				_inst = FindAnyObjectByType<T>();
				if (_inst == null)
				{
					GameObject obj = new GameObject();
					obj.name = typeof(T).ToString();
					_inst = obj.AddComponent<T>();
				}
			}
			return _inst;
		}
	}
	protected void Initialized()
{
	if (_inst == null)
	{
		_inst = this as T;
	}
	else
	{
		Destroy(this);
	}
}
}
