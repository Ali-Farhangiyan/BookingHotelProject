using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pagination
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }

        public int TotalPage { get; set; }

        public bool HasNextPage => PageIndex < TotalPage;
        public bool HasPrevPage => PageIndex > 1;

        public PaginatedList(List<T> listOfObjects, int count, int pageSize, int pageIndex)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Floor((double)count / pageSize);
            this.AddRange(listOfObjects);
        }

        public static PaginatedList<T> Create(List<T> model, int pageSize, int pageIndex)
        {
            int count = model.Count;
            var takeModel = model.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(takeModel, count, pageSize, pageIndex);
        }
    }
}
