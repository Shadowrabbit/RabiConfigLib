// ******************************************************************
//       /\ /|       @file       OrderedDictionary
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-10-24 20:40:24
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RabiConfigLib
{
    public class OrderedDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        public int Count => _dictionary.Count;
        private readonly Dictionary<TKey, TValue> _dictionary = new();
        public List<TKey> Keys { get; } = [];

        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set
            {
                if (!_dictionary.ContainsKey(key))
                {
                    Keys.Add(key);
                }

                _dictionary[key] = value;
            }
        }

        public TValue? this[int index]
        {
            get
            {
                if (Keys.Count <= index)
                {
                    Debug.LogError($"[RabiConfigLib] Index out of bounds. index:{index}");
                    return default;
                }

                var key = Keys[index];
                return _dictionary[key];
            }
        }

        public IEnumerable<TValue> Values
        {
            get { return Keys.Select(key => _dictionary[key]); }
        }

        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        public void Add(TKey key, TValue value)
        {
            if (_dictionary.ContainsKey(key))
            {
                throw new ArgumentException("[RabiConfigLib] An element with the same key already exists in OrderedDictionary.");
            }

            Keys.Add(key);
            _dictionary[key] = value;
        }

        public void Remove(TKey key)
        {
            if (!_dictionary.ContainsKey(key))
            {
                return;
            }

            _dictionary.Remove(key);
            Keys.Remove(key);
        }

        public bool TryAdd(TKey key, TValue value)
        {
            if (_dictionary.ContainsKey(key))
            {
                return false;
            }

            Keys.Add(key);
            _dictionary[key] = value;
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Get the index of a key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetIndex(TKey key)
        {
            return Keys.IndexOf(key);
        }

        public void Clear()
        {
            _dictionary.Clear();
            Keys.Clear();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Keys.Select(key => new KeyValuePair<TKey, TValue>(key, _dictionary[key])).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}