using System;

namespace ProductMatchingApp
{
    class ProductSales
    {
        /// <summary>
        /// 商品コード
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品カテゴリー
        /// </summary>
        public string ProductCategory { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 販売日
        /// </summary>
        public DateTime SalesDate { get; set; }
    }
}
