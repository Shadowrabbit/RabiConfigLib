// ******************************************************************
//       /\ /|       @file       RowHelper
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-07-03 13:50:10
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;

namespace RabiConfigLib
{
    public class RowHelper
    {
        private readonly List<string> _col; //Each column data of the current row
        private int _idx; //Current index

        public RowHelper(List<string> rs)
        {
            _col = rs;
            _idx = 0;
        }

        /// <summary>
        /// Read the next column
        /// </summary>
        /// <returns></returns>
        public string ReadNextCol()
        {
            return _col[_idx++];
        }
    }
}