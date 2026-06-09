using Autofac;
using Npgsql;
using Projekti.Common.Repository;
using Projekti.Common.Service;
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

                    builder.RegisterType<UserModelService>().As<IUserModelService>();
                    builder.RegisterType<UserModelRepository>().As<IUserModelRepository>();

                }
    }
}
