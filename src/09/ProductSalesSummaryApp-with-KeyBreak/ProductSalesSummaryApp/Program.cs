using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CsvHelper;
using CsvHelper.TypeConversion;

using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace ProductSalesSummaryApp
{
    class Program
    {
        private readonly static ILogger logger = new LoggerFactory()
                .AddNLog()
                .CreateLogger<Program>();

        static int Main(string[] args)
        {
            // 集約例外ハンドラーの設定
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                logger.LogCritical(ex, "想定外のエラーが発生しました。管理者に連絡してください。");
            };

            // 商品販売データ取得
            var productSalesReader = new ProductSalesReader(logger);
            var (productSales, getDataResult) = productSalesReader.GetData();
            if (getDataResult != ProductSalesReader.GetDataResult.NoError)
            {
                return 1;
            }

            // レポート出力
            var writer = Console.Out;
            var reportWriter = new ReportWriter(writer, productSales);
            reportWriter.Run();

            return 0;
        }
    }
}
