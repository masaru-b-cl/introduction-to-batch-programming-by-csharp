using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductSalesSummaryApp
{
    internal class ReportWriter
    {
        private class ProductSalesSummary
        {
            public decimal TotalAmount = 0m;
            public decimal CategoryAmount = 0m;
        }

        private TextWriter writer;
        private IEnumerable<ProductSales> productSales;

        public ReportWriter(TextWriter writer, IEnumerable<ProductSales> productSales)
        {
            this.writer = writer;
            this.productSales = productSales;
        }

        public void Run()
        {
            var orderedProductSales = SortProductSales(productSales);

            var summary = new ProductSalesSummary();

            var newProductCategory = "";
            var oldProductCategory = "";

            WriteTitle();

            foreach (var item in orderedProductSales)
            {
                newProductCategory = item.ProductCategory;

                if (newProductCategory != oldProductCategory)
                {
                    // 商品カテゴリーが変わった
                    if (!string.IsNullOrEmpty(oldProductCategory))
                    {
                        // 1件目でなければカテゴリー別金額合計を出力
                        WriteCategoryAmount(summary);
                    }

                    // 集計値を初期化
                    summary.CategoryAmount = 0;

                    // キーを更新
                    oldProductCategory = newProductCategory;

                    WriteCategoryTitle(oldProductCategory);
                }

                WriteItem(item);

                summary.CategoryAmount += item.Amount;
                summary.TotalAmount += item.Amount;
            }

            // ループ終了後も最後の商品カテゴリーの集計値を出力
            WriteCategoryAmount(summary);

            WriteTotalAmount(summary);
        }

        private IEnumerable<ProductSales> SortProductSales(IEnumerable<ProductSales> productSales)
        {
            return from item in productSales
                   orderby item.ProductCategory, item.SalesDate, item.ProductCode
                   select item;
        }

        private void WriteTitle()
        {
            writer.WriteLine("商品販売データ集計結果");
            writer.WriteLine("=====");
        }

        private void WriteItem(ProductSales item)
        {
            writer.WriteLine($"- {item.SalesDate:yyyy/MM/dd} :" +
                $" <{item.ProductCode} {item.ProductName}>" +
                $" {item.UnitPrice:#,###.#} 円 * {item.Quantity:#,###.#} 個 = {item.Amount:#,###.#} 円");
        }

        private void WriteCategoryTitle(string productCategory)
        {
            writer.WriteLine("");
            writer.WriteLine($"商品カテゴリー: {productCategory}");
            writer.WriteLine("-----");
            writer.WriteLine("");
        }

        private void WriteCategoryAmount(ProductSalesSummary summary)
        {
            writer.WriteLine("");
            writer.WriteLine("---");
            writer.WriteLine("");
            writer.WriteLine($"商品カテゴリー計: {summary.CategoryAmount:#,##0.#} 円");
            writer.WriteLine("");
            writer.WriteLine("---");
        }

        private void WriteTotalAmount(ProductSalesSummary summary)
        {
            writer.WriteLine("");
            writer.WriteLine($"総合計");
            writer.WriteLine("-----");
            writer.WriteLine("");
            writer.WriteLine($"{summary.TotalAmount:#,##0.#} 円");
        }
    }
}