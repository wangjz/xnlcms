using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
namespace COM.SingNo.Common
{
  public  class SafeList <T>:IList<T>
    {
        private readonly object syncRoot = new object();
        private readonly List<T> innerList = new List<T>();
        #region IList<T> 成员

        public int IndexOf(T item)
        {
            lock (syncRoot)
            {
              return  innerList.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (syncRoot)
            {
                innerList.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (syncRoot)
            {
                innerList.RemoveAt(index);
            }
        }

        public T this[int index]
        {
            get
            {
                return innerList[index];
            }
            set
            {
                lock (syncRoot)
                {
                    innerList[index] = value;
                }
            }
        }

        #endregion

        #region ICollection<T> 成员

        public void Add(T item)
        {
            lock (syncRoot)
            {
                innerList.Add(item);
            }
        }

        public void Clear()
        {
            lock (syncRoot)
            {
                innerList.Clear();
            }
        }

        public bool Contains(T item)
        {
            return innerList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (syncRoot)
            {
                innerList.CopyTo(array, arrayIndex);
            }
        }

        public int Count
        {
            get { return innerList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            lock (syncRoot)
            {
               return innerList.Remove(item);
            }
        }

        #endregion

        #region IEnumerable<T> 成员

        public IEnumerator<T> GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        #endregion
    }
}
