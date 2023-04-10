using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class CatalogueDTO
    {
        public int Id { get; set; }
        public string ProductDescription { get; set; }
        public string Image { get; set; }
        public int Points { get; set; }
    }
}
