using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using AdvancedInspector;

namespace SharedCode.Types
{
	//[AdvancedInspector]
	public class CountedSet<T>
	{
		//[Inspect]
		Dictionary<T, int> m_Members = new Dictionary<T, int>();

		public CountedSet() { }

		public CountedSet(params T[] members) : this(members as IEnumerable<T>) { }

		public CountedSet(IEnumerable<T> members) : this()
		{
			foreach (T item in members)
				Add(item);
		}


		public void Add(T obj)
		{
			int count = 0;
			m_Members.TryGetValue(obj, out count);
			count++;
			m_Members[obj] = count;
		}

		public void Remove(T obj)
		{
			int count;
			if (false == m_Members.TryGetValue(obj, out count))
				return;
			count--;
			
			if (0 == count)
				m_Members.Remove(obj);
			else
				m_Members[obj] = count;
		}

		//[Inspect]
		public int UniqueCount
		{
			get { return m_Members.Count; }
		}

		//[Inspect]
		public int NonUniqueCount
		{
			get
			{
				int count = 0;

				foreach (int value in m_Members.Values)
					count += value;

				return count;
			}
		}

		public bool Contains(T item)
		{
			return m_Members.ContainsKey(item);
		}

		public int CountFor(T item)
		{
			int count = 0;
			m_Members.TryGetValue(item, out count);
			return count;
		}

		public IEnumerable<KeyValuePair<T, int>> UniqueEnumerable
		{
			get
			{
				return m_Members;
			}
		}
	}
}