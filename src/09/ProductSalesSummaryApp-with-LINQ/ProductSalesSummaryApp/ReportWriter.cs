using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductSalesSummaryApp
{
    internal class ReportWriter
    {
        private TextWriter writer;
        private IEnumerable<ProductSales> productSales;

        public ReportWriter(TextWriter writer, IEnumerable<ProductSales> productSales)
        {
            this.writer = writer;
            this.productSales = productSales;
        }

        public void Run()
        {
            var q = from item in productSales
                    group item by item.ProductCategory into categoryGroup // カテゴリーでグループ化
                    orderby categoryGroup.Key // カテゴリー名でソート
                    select  //カテゴリー単位に集計と明細データのソートを行う
                    (
                        Category: categoryGroup.Key,
                        CategoryAmount: categoryGroup.Sum(item => item.Amount),
                        ProductSales: from item in categoryGroup
                                       orderby item.SalesDate, item.ProductCode
                                       select item
                    );

            WriteTitle();

            foreach (var categoryItem in q)
            {
                WriteCategoryTitle(categoryItem.Category);

                foreach (var item in categoryItem.ProductSales)
                {
                    WriteItem(item);
                }

                WriteCategoryAmount(categoryItem.CategoryAmount);
            }

            WriteTotalAmount(productSales.Sum(item => item.Amount));
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

        private void WriteCategoryAmount(decimal categoryAmount)
        {
            writer.WriteLine("");
            writer.WriteLine("---");
            writer.WriteLine("");
            writer.WriteLine($"商品カテゴリー計: {categoryAmount:#,##0.#} 円");
            writer.WriteLine("");
            writer.WriteLine("---");
        }

        private void WriteTotalAmount(decimal totalAmount)
        {
            writer.WriteLine("");
            writer.WriteLine($"総合計");
            writer.WriteLine("-----");
            writer.WriteLine("");
            writer.WriteLine($"{totalAmount:#,##0.#} 円");
        }
    }
}