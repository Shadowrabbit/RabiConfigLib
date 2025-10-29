// ******************************************************************
//       /\ /|       @file       CsvUtility
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-07-03 13:49:45
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RabiConfigLib
{
    public static class CsvUtility
    {
        /// <summary>
        /// Convert string to integer
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(string str)
        {
            if (str.Equals(string.Empty))
            {
                return 0;
            }

            try
            {
                return int.Parse(str);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[RabiConfigLib] {ex}");
                return 0;
            }
        }

        /// <summary>
        /// Convert string to float; note that the string represents a per mille (‰) value, and only two decimal places should be retained
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ToFloat(string str)
        {
            if (str.Equals(string.Empty))
            {
                return 0;
            }

            try
            {
                return Convert.ToSingle(str);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[RabiConfigLib] {ex}");
                return 0;
            }
        }

        /// <summary>
        /// Convert to boolean
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBool(string str)
        {
            if (str.Equals(string.Empty))
            {
                return false;
            }

            try
            {
                return bool.Parse(str);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[RabiConfigLib] {ex}");
                return false;
            }
        }

        public static string ToString(string str)
        {
            return str;
        }

        /// <summary>
        /// Convert to Vector3Int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Vector2Int ToVector2Int(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return Vector2Int.zero;
            }

            if (str.Equals("None"))
            {
                return Vector2Int.zero;
            }

            var vecStr = str.Split('-');
            if (vecStr.Length == 2)
            {
                return new Vector2Int(ToInt(vecStr[0]), ToInt(vecStr[1]));
            }

            Debug.LogError($"[RabiConfigLib] Unable to convert to Vector2Int str:{str}");
            return Vector2Int.zero;
        }

        /// <summary>
        /// Convert to Vector3Int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Vector3Int ToVector3Int(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return Vector3Int.zero;
            }

            if (str.Equals("None"))
            {
                return Vector3Int.zero;
            }

            var vecStr = str.Split('-');
            if (vecStr.Length == 3) return new Vector3Int(ToInt(vecStr[0]), ToInt(vecStr[1]), ToInt(vecStr[2]));
            Debug.LogError($"[RabiConfigLib] Unable to convert to Vector3Int str:{str}");
            return Vector3Int.zero;
        }

        /// <summary>
        /// Convert to Vector3
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return Vector3Int.zero;
            }

            if (str.Equals("None"))
            {
                return Vector3.zero;
            }

            var vecStr = str.Split('-');
            if (vecStr.Length == 3) return new Vector3(ToFloat(vecStr[0]), ToFloat(vecStr[1]), ToFloat(vecStr[2]));
            Debug.LogError($"[RabiConfigLib] Unable to convert to Vector3 str:{str}");
            return Vector3.zero;
        }

        /// <summary>
        /// Convert to generic dictionary
        /// </summary>
        /// <param name="str"></param>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <returns></returns>
        public static Dictionary<TK, TV> ToDictionary<TK, TV>(string str)
        {
            if (str.Equals(string.Empty))
            {
                return null!;
            }

            var dict = new Dictionary<TK, TV>();
            var kvPairsStrArray = str.Split(',');
            foreach (var kvPairStr in kvPairsStrArray)
            {
                var kvPair = kvPairStr.Split(':');
                if (kvPair.Length != 2)
                {
                    Debug.LogError($"[RabiConfigLib] Failed to parse dictionary:{str}");
                    continue;
                }

                var k = ToT<TK>(kvPair[0]);
                var v = ToT<TV>(kvPair[1]);
                if (!dict.ContainsKey(k))
                {
                    dict.Add(k, v);
                }
            }

            return dict;
        }

        /// <summary>
        /// Convert to generic hash set
        /// </summary>
        /// <param name="str"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static HashSet<T> ToHashset<T>(string str)
        {
            if (str.Equals(string.Empty))
            {
                return null!;
            }

            var hashSet = new HashSet<T>();
            var strValueArray = str.Split(',');
            foreach (var strValue in strValueArray)
            {
                var k = ToT<T>(strValue);
                hashSet.Add(k);
            }

            return hashSet;
        }

        /// <summary>
        /// Convert to generic dynamic array
        /// </summary>
        /// <param name="str"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> ToList<T>(string str)
        {
            if (str.Equals(string.Empty))
            {
                return null!;
            }

            var valueStrArray = str.Split(',');
            return valueStrArray.Select(ToT<T>).ToList();
        }

        /// <summary>
        /// Generic parsing
        /// </summary>
        /// <param name="readNextCol"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToT<T>(this string readNextCol)
        {
            object value = null!;
            if (typeof(T) == typeof(int))
            {
                value = ToInt(readNextCol);
            }
            else if (typeof(T) == typeof(float))
            {
                value = ToFloat(readNextCol);
            }
            else if (typeof(T) == typeof(string))
            {
                value = ToString(readNextCol);
            }
            else if (typeof(T) == typeof(bool))
            {
                value = ToBool(readNextCol);
            }
            else if (typeof(T) == typeof(Vector2Int))
            {
                value = ToVector2Int(readNextCol);
            }

            return (T)value;
        }
    }
}