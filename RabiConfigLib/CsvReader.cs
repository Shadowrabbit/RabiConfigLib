// ******************************************************************
//       /\ /|       @file       CsvReader
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-07-03 13:49:17
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RabiConfigLib
{
    public class CsvReader
    {
        private readonly OrderedDictionary<string, List<string>?> _key2RowData = new(); //key,field data of row
        private readonly List<string> _fieldNameList = new(); //field name of row

        /// <summary>
        /// Load data file
        /// </summary>
        /// <param name="text">config</param>
        public void LoadText(string text)
        {
            try
            {
                var rowTextArray = text.Split('\n');
                _fieldNameList.AddRange(rowTextArray[0].Split('|').Select(t => t.Trim()));
                foreach (var t in rowTextArray)
                {
                    //To remove data separators with no data and ignore them
                    if (string.IsNullOrWhiteSpace(t.Trim('|')))
                    {
                        continue;
                    }

                    ParseLine(t);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[RabiConfigLib] {e}");
                throw;
            }
        }

        /// <summary>
        /// append data file
        /// </summary>
        /// <param name="text">config</param>
        public void AppendText(string text)
        {
            try
            {
                var rowTextArray = text.Split('\n');
                foreach (var t in rowTextArray)
                {
                    //To remove data separators with no data and ignore them
                    if (string.IsNullOrWhiteSpace(t.Trim('|')))
                    {
                        continue;
                    }

                    ParseLine(t);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[RabiConfigLib] {e}");
                throw;
            }
        }

        /// <summary>
        /// get the col index of a filed name in sheet
        /// </summary>
        /// <param name="filedName"></param>
        /// <returns></returns>
        public int GetColIndex(string filedName)
        {
            for (var index = 0; index < _fieldNameList.Count; index++)
            {
                var fieldName = _fieldNameList[index];
                if (fieldName.Equals(filedName))
                {
                    return index;
                }
            }

            throw new Exception($"[RabiConfigLib] Can't find field name. filedName:{filedName}");
        }

        /// <summary>
        /// remove the whole row
        /// </summary>
        /// <param name="key"></param>
        public void RemoveRowData(string key)
        {
            if (!_key2RowData.ContainsKey(key))
            {
                throw new Exception($"[RabiConfigLib] Failed to remove row data. key:{key}");
            }

            _key2RowData.Remove(key);
        }

        /// <summary>
        /// find field values in a row
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string>? FindColValueList(string key)
        {
            return !_key2RowData.ContainsKey(key) ? null : _key2RowData[key];
        }

        /// <summary>
        /// Get all source configs
        /// </summary>
        /// <returns></returns>
        public OrderedDictionary<string, List<string>?> GetDataMap()
        {
            return _key2RowData;
        }

        public void Clear()
        {
            _key2RowData.Clear();
            _fieldNameList.Clear();
        }

        /// <summary>
        /// Parse the current row
        /// </summary>
        /// <param name="rowText"></param>
        private void ParseLine(string rowText)
        {
            var colValue = rowText.Split('|');
            //Data of each column in the current row
            var colTextList = colValue.Select(t => t.Trim()).ToList();
            // Ensure at least [key, value] by padding missing columns with string.Empty
            // Also align to header width if there is a known header length
            var expectedCount = Math.Max(_fieldNameList.Count == 0 ? 2 : _fieldNameList.Count, 2);
            if (colTextList.Count < expectedCount)
            {
                colTextList.AddRange(Enumerable.Repeat(string.Empty, expectedCount - colTextList.Count));
            }

            if (colTextList.Count <= 1)
            {
                Debug.LogError($"[RabiConfigLib] Wrong config data. rowText:{rowText}");
                return;
            }

            var key = colTextList[0];
            if (_key2RowData.TryAdd(key, colTextList))
            {
                return;
            }

            Debug.LogError($"[RabiConfigLib] Repeatedly add row data. key:{key}");
        }
    }
}