using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS.Pagination
{
    public class GenericPaginationDTO<T> where T : class
    {
        public string PreviousPage { get; set;}

        public List<T> Results { get; set; }

        public string NextPage { get; set; }
    }
}
