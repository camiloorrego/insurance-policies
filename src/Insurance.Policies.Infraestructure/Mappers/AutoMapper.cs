using AutoMapper;
using Insurance.Policies.Domain.Entities;
using Insurance.Policies.Infraestructure.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Policies.Infraestructure.Mappers
{
   public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<ClientPolicy, ClientPolicyDataModel>().ReverseMap();
            CreateMap<Policy, PolicyDataModel>().ReverseMap();
            CreateMap<PolicyType, PolicyTypeDataModel>().ReverseMap();
            CreateMap<RiskType, RiskTypeDataModel>().ReverseMap();
            CreateMap<Rule, RuleDataModel>().ReverseMap();
            CreateMap<Client, ClientDataModel>().ReverseMap();
        }
    }
}
