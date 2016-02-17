using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEVIN.MVC.MODEL.DTO
{
    /// <summary>
    /// 数据分页实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageInfo<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// 数据表，支持多表
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 聚合
        /// </summary>
        public string GroupBy { get; set; }

        /// <summary>
        /// 筛选
        /// </summary>
        public string Having { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public string Where { get; set; }
        /// <summary>
        /// 查询字段
        /// </summary>
        public string Fields { get; set; }
        /// <summary>
        /// 查询结果记录集
        /// </summary>
        public IEnumerable<T> List { get; set; }
    }
}
