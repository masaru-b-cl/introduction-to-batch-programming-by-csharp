﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new[] { 5, 9, 3, 1, 2, 8, 4, 7, 6 };

            // 確定位置を先頭から末尾の手前まで順に移動させる
            for (var fixedIndex = 0; fixedIndex < source.Length - 1; fixedIndex++)
            {
                // 末尾から確定位置まで繰り返す
                for (var i = source.Length - 1; i > fixedIndex; i--)
                {
                    // 比較対象値を取得
                    var right = source[i];
                    var left = source[i - 1];

                    if (left > right)
                    {
                        // 小さいほうを左側にする
                        source[i] = left;
                        source[i - 1] = right;
                    }
                }
            }

            foreach (var item in source)
            {
                Console.WriteLine(item);
            }
        }
    }
}
