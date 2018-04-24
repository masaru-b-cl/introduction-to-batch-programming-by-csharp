using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Microsoft.Extensions.Logging;

namespace ProductSalesSummaryApp
{
    internal class ProductSalesReader
    {
        public enum GetDataResult
        {
            FileNotFound,
            DataIsEmpty,
            NoError
        }

        private ILogger logger;

        public ProductSalesReader(ILogger logger)
        {
            this.logger = logger;
        }

        public (IEnumerable<ProductSales>, GetDataResult) GetData()
        {
            const string inputFilePath = @".\product_sales.csv";

            if (!File.Exists(inputFilePath))
            {
                // ファイルなし
                logger.LogError($"商品販売データCSVファイル<{inputFilePath}>がありません。");
                return (null, GetDataResult.FileNotFound);
            }

            using (var streamReader = new StreamReader(inputFilePath, Encoding.UTF8))
            {
                var csvReader = new CsvReader(streamReader);
                var productSales = csvReader.GetRecords<ProductSales>().ToArray();

                if (productSales.Any())
                {
                    // データあり
                    return (productSales, GetDataResult.NoError);
                }
                else
                {
                    // データなし
                    logger.LogError($"商品販売データCSVファイル<{inputFilePath}>にデータがありません。");
                    return (null, GetDataResult.DataIsEmpty);
                }
            }
        }
    }
}