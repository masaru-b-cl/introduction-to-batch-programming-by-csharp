using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CsvHelper;
using CsvHelper.TypeConversion;

namespace ProductMatchingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var productDictionary = ImportProduct();

            var productSales = ImportSales();

            MatchProduct(productDictionary, productSales);

            var sortedProductSales = SortProductSales(productSales);

            OutputResult(sortedProductSales);
        }

        private static Dictionary<string, Product> ImportProduct()
        {
            // 商品データCSVファイルを読み込むためのStreamReaderオブジェクト作成
            using (var sr = new StreamReader(@".\product.csv", Encoding.UTF8))
            {
                // CSV helperで読み込み
                var csv = new CsvReader(sr);
                var products = csv.GetRecords<Product>();

                // ディクショナリに変換
                return products.ToDictionary(product => product.Code);
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
                return sales.Select(item => new ProductSales
                {
                    ProductCode = item.ProductCode,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    SalesDate = item.SalesDate
                })
                .ToList();
            }
        }

        private static void MatchProduct(Dictionary<string, Product> productDictionary, List<ProductSales> productSales)
        {
            foreach (var item in productSales)
            {
                // 対応する商品情報を取得
                var product = productDictionary[item.ProductCode];

                // 商品情報を商品版倍データに設定
                item.ProductName = product.Name;
                item.ProductCategory = product.Category;
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
