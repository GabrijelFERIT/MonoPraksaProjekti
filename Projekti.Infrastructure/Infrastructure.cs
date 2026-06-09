using Autofac;
using AutoMapper;
using Npgsql;
using Projekti.Common.Repository;
using Projekti.Common.Service;
using Projekti.Model;
using Projekti.Repository;
using Projekti.Service;
using System.Data;
using System.Data.Common;

namespace Projekti.Infrastructure
{
    public class Infrastructure : Module
    {
                protected override void Load(ContainerBuilder builder)
                {
                    string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=OsijekPraksa123.";

                    builder.Register(c => new Npgsql.NpgsqlConnection(connectionString)).As<NpgsqlConnection>().InstancePerLifetimeScope();

                    builder.RegisterType<UserModelService>().As<IUserModelService>().InstancePerLifetimeScope();
                    builder.RegisterType<UserModelRepository>().As<IUserModelRepository>().InstancePerDependency();
                    builder.RegisterType<AutoMapperProfile>().AsSelf().SingleInstance();
            

        }
    }



    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile() {

            CreateMap<UserModel, UserModelDto>();
        
            CreateMap<UserModelDto, UserModel>();

            CreateMap<RegistrationDto, UserModel>();

            CreateMap<UserModel, RegistrationDto>();

        }

    }
}
