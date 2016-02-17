using KEVIN.MVC.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentData;
using KEVIN.MVC.MODEL.DTO;

namespace KEVIN.MVC.SERVICE
{
    public class BaseService : IBaseService
    {
        /// <summary>
        /// 默认数据库连接
        /// </summary>
        private string connstr = "ConnectionString";

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public IDbContext db;

        public BaseService()
        {
            //忽略实体与数据库字段不匹配的异常
            db = DBContext().IgnoreIfAutoMapFails(true);
        }

        /// <summary>
        /// 数据库上下文，默认连接ConnectionString
        /// </summary>
        /// <returns></returns>
        public IDbContext DBContext()
        {
            return new DbContext().ConnectionStringName(connstr, new SqlServerProvider()).IgnoreIfAutoMapFails(true);
        }

        /// <summary>
        /// 数据库上下文,指定连接
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public IDbContext DBContext(string conn)
        {
            return new DbContext().ConnectionStringName(conn, new SqlServerProvider()).IgnoreIfAutoMapFails(true);
        }

        /// <summary>
        /// 通用分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="PageInfo"></param>
        /// <returns></returns>
        public PageInfo<T> GetPageInfo<T>(PageInfo<T> PageInfo)
        {
            try
            {
                string sqlcount = string.Format("select count(1) from {0} ", PageInfo.TableName);
                if (!string.IsNullOrEmpty(PageInfo.Where)) { sqlcount += string.Format("where {0}", PageInfo.Where); }
                PageInfo.TotalCount = db.Sql(sqlcount).QuerySingle<int>();
                if (PageInfo.PageSize <= 0) { PageInfo.PageSize = 10; }
                PageInfo.TotalPage = (PageInfo.TotalCount % PageInfo.PageSize == 0) ? PageInfo.TotalCount / PageInfo.PageSize : PageInfo.TotalPage = PageInfo.TotalCount / PageInfo.PageSize + 1;

                //基本查询
                ISelectBuilder<T> building = db.Select<T>(PageInfo.Fields).From(PageInfo.TableName);

                //条件
                if (!string.IsNullOrEmpty(PageInfo.Where))
                {
                    building = building.Where(PageInfo.Where);
                }

                //分组
                if (!string.IsNullOrEmpty(PageInfo.GroupBy))
                {
                    building = building.GroupBy(PageInfo.GroupBy);
                }

                //过滤
                if (!string.IsNullOrEmpty(PageInfo.Having))
                {
                    building = building.Having(PageInfo.Having);
                }

                //排序
                if (!string.IsNullOrEmpty(PageInfo.OrderBy))
                {
                    building = building.OrderBy(PageInfo.OrderBy);
                }

                //分页
                PageInfo.List = building.Paging(PageInfo.PageIndex, PageInfo.PageSize).QueryMany();
                return PageInfo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
