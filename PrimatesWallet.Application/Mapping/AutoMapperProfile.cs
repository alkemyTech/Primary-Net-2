using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CatalogueDTO, Catalogue>();
            CreateMap<FixedTermDeposit, FixedTermDepositRequestDTO>();
            CreateMap<Core.Models.Transaction, TransactionDto>();
        }
    }
}