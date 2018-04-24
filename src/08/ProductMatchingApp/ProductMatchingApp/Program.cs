using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using CsvHelper;
using CsvHelper.TypeConversion;

// ログ出力ライブラリの名前空間をインポート
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace ProductMatchingApp
{
    class Program
    {
        // ロガー作成
        private readonly static ILogger logger = new LoggerFactory()
                .AddNLog()
                .CreateLogger<Program>();

        enum Result
        {
            Success,
            Failure
        }

        static int Main(string[] args)
        {
            // 集約例外ハンドラーの設定
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                logger.LogCritical(ex, "想定外のエラーが発生しました。管理者に連絡してください。");
            };

            var (productDictionary, importProductResult) = ImportProduct();
            if (importProductResult != Result.Success)
            {
                return 1;
            }

            var productSales = ImportSales();

            var matchProductResult = MatchProduct(productDictionary, productSales);
            if (matchProductResult != Result.Success)
            {
                return 1;
            }

            var sortedProductSales = SortProductSales(productSales);

            OutputResult(sortedProductSales);

            // 正常終了
            return 0;
        }

        private static (Dictionary<string, Product>, Result) ImportProduct()
        {
            const string productFilePath = @".\product.csv";

            // 商品デVータCSVファイルがなければエラー
            if (!File.Exists(productFilePath))
            {
                // エラー内容をログに出力する
                logger.LogError($"商品データCSVファイル<{productFilePath}>がありません。");
                return (null, Result.Failure);
            }

            // 商品データCSVファイルを読み込むためのStreamReaderオブジェクト作成
            using (var sr = new StreamReader(productFilePath, Encoding.UTF8))
            {
                // CSV helperで読み込み
                var csv = new CsvReader(sr);
                var products = csv.GetRecords<Product>();

                // ディクショナリに変換
                var dic = products.ToDictionary(product => product.Code);

                return (dic, Result.Success);
            }
        }

        private static List<ProductSales> ImportSales()
        {
            // 販売データCSVファイルを読み込むためのStreamReaderオブジェクト作成
            using (var sr = new StreamReader(@".\sales.csv", Encoding.UTF8))
            {
                // CSV helperで読み込み
                var csv = new CsvReader(sr);

                // 販売データを読み込む
                var sales = csv.GetRecords<Sales>();

                // 商品販売情報のリストに変換
                var productSalesList = sales.Select(item => new ProductSales
                {
                    ProductCode = item.ProductCode,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    SalesDate = item.SalesDate
                })
                .ToList();

                return productSalesList;
            }
        }

        private static Result MatchProduct(Dictionary<string, Product> productDictionary, List<ProductSales> productSales)
        {
            bool hasError = false;

            // インデックス付きで列挙
            foreach (var (item, i) in productSales.Select((x, i) => (x, i)))
            {
                // 対応する商品情報を取得
                if (productDictionary.TryGetValue(item.ProductCode, out var product))
                {
                    // 商品情報を商品版倍データに設定
                    item.ProductName = product.Name;
                    item.ProductCategory = product.Category;
                }
                else
                {
                    // 商品が見つからなければエラー
                    logger.LogError($"販売データ {i + 1} 行目 | 商品コード<{item.ProductCode}>に該当する商品が見つかりませんでした。");
                    hasError = true;
                }
            }

            if (hasError)
            {
                return Result.Failure;
            }
            else
            {
                return Result.Success;
            }
        }

        private static IEnumerable<ProductSales> SortProductSales(List<ProductSales> productSales)
        {
            // 販売日、商品カテゴリー、商品コード順に並び替える
            return from item in productSales
                   orderby item.SalesDate,
                           item.ProductCategory,
                           item.ProductCode
                   select item;
        }

        private static void OutputResult(IEnumerable<ProductSales> productSales)
        {
            using (var sw = new StreamWriter(@".\result.csv", false, Encoding.UTF8))
            {
                // CSV helperで書き出し
                var csv = new CsvWriter(sw);

                // 日付をyyyy/MM/dd形式で出力するよう設定する
                csv.Configuration.TypeConverterOptionsCache.AddOptions<DateTime>(
                    new TypeConverterOptions
                    {
                        Formats = new[] { "yyyy/MM/dd" }
                    }
                );

                // ヘッダー出力
                csv.WriteHeader<ProductSales>();
                csv.NextRecord();   // 次のレコードへ

                // 全データ出力
                csv.WriteRecords(productSales);
            }
        }
    }
}
