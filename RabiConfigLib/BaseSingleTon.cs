// ******************************************************************
//       /\ /|       @file       BaseSingleTon
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2024-10-24 20:39:45
//    *(__\_\        @Copyright  Copyright (c) 2024, Shadowrabbit
// ******************************************************************

namespace RabiConfigLib
{
    public class BaseSingleTon<T> where T : class, new()
    {
        public static T Instance => Inner.InternalInstance;

        private static class Inner
        {
            internal static readonly T InternalInstance = new T();
        }
    }
}