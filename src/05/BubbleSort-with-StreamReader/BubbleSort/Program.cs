using System;
using System.Collections.Generic;
using System.IO;    // Encodingクラスを使うために追加
using System.Linq;
using System.Text;

namespace BubbleSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = ReadLines()        // 標準入力から1行ずつ読み取る
                .Select(x => int.Parse(x))  // 配列の要素をint型に変換
                .ToArray();                 // 変換結果を配列にする

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
        
        private static IEnumerable<string> ReadLines()
        {
            using (var streamReader = new StreamReader(@".\source.txt", Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    // データがなくなるまで、順次標準入力の内容を1行ずつ返す
                    yield return line;
                }
            }
        }
    }
}
