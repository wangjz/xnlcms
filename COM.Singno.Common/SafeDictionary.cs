using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
namespace COM.SingNo.Common
{
    public class SafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly object syncRoot = new object();
        private readonly Dictionary<TKey, TValue> innerD = new Dictionary<TKey, TValue>();
        #region IDictionary<TKey,TValue> Members

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IDictionary`2"></see> is read-only.</exception>
        /// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.</exception>
        /// <exception cref="T:System.ArgumentNullException">key is null.</exception>
        public void Add(TKey key, TValue value)
        {
            lock (syncRoot)
            {
                innerD.Add(key, value);
            }
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2"></see> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.</param>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2"></see> contains an element with the key; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">key is null.</exception>
        public bool ContainsKey(TKey key)
        {
            lock (syncRoot)
            {
                return innerD.ContainsKey(key);
            }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"></see> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
        /// </summary>
        /// <value></value>
        /// <returns>An <see cref="T:System.Collections.Generic.ICollection`1"></see> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"></see>.</returns>
        public ICollection<TKey> Keys
        {
            get
            {
                lock (syncRoot)
                {
                    return innerD.Keys;
                }
            }
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if key was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IDictionary`2"></see> is read-only.</exception>
        /// <exception cref="T:System.ArgumentNullException">key is null.</exception>
        public bool Remove(TKey key)
        {
            lock (syncRoot)
            {
                return innerD.Remove(key);
            }
        }

        /// <summary>
        /// Tries the get value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (syncRoot)
            {
                return innerD.TryGetValue(key, out value);
            }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"></see> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
        /// </summary>
        /// <value></value>
        /// <returns>An <see cref="T:System.Collections.Generic.ICollection`1"></see> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"></see>.</returns>
        public ICollection<TValue> Values
        {
            get
            {
                lock (syncRoot)
                {
                    return innerD.Values;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="TValue"/> with the specified key.
        /// </summary>
        /// <value></value>
        public TValue this[TKey key]
        {
            get { return innerD[key]; }
            set
            {
                lock (syncRoot)
                {

                    innerD[key] = value;
                }
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            lock (syncRoot)
            {
                ((ICollection<KeyValuePair<TKey, TValue>>)innerD).Add(item);
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only. </exception>
        public void Clear()
        {
            lock (syncRoot)
            {
                innerD.Clear();
            }
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        /// true if item is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
        /// </returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)innerD).Contains(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        /// <exception cref="T:System.ArgumentNullException">array is null.</exception>
        /// <exception cref="T:System.ArgumentException">array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"></see> is greater than the available space from arrayIndex to the end of the destination array.-or-Type T cannot be cast automatically to the type of the destination array.</exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            lock (syncRoot)
            {
                ((ICollection<KeyValuePair<TKey, TValue>>)innerD).CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <value></value>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</returns>
        public int Count
        {
            get { return innerD.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only; otherwise, false.</returns>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        /// true if item was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if item is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            lock (syncRoot)
            {
                return ((ICollection<KeyValuePair<TKey, TValue>>)innerD).Remove(item);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)innerD).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)innerD).GetEnumerator();
        }

        #endregion
    }
}
