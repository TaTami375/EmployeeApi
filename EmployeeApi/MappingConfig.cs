using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Dapper.FluentMap.Dommel.Mapping;
using EmployeeApi.Models;
namespace EmployeeApi
{
    public class MappingConfig
    {
        public static void Configure()
        {
            // Регистрация маппинга
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new EmployeeMap());
                config.AddMap(new DepartmentMap());
                config.AddMap(new PassportMap());
                config.ForDommel();
            });
        }
    }

    public class EmployeeMap : DommelEntityMap<Employee>
    {
        public EmployeeMap()
        {
            Map(e => e.Id).ToColumn("Id");
            Map(e => e.Name).ToColumn("Name");
            Map(e => e.Surname).ToColumn("Surname");
            Map(e => e.Phone).ToColumn("Phone");
            Map(e => e.CompanyId).ToColumn("CompanyId");
            Map(e => e.PassportId).ToColumn("PassportId");
            Map(e => e.DepartmentId).ToColumn("DepartmentId");
        }
    }

    public class DepartmentMap : DommelEntityMap<Department>
    {
        public DepartmentMap()
        {
            Map(e => e.Id).ToColumn("Id");
            Map(e => e.Name).ToColumn("Name");
            Map(e => e.Phone).ToColumn("Phone");
        }
    }

    public class PassportMap : DommelEntityMap<Passport>
    {
        public PassportMap()
        {
            Map(e => e.Id).ToColumn("Id");
            Map(e => e.Type).ToColumn("Type");
            Map(e => e.Number).ToColumn("Number");
        }
    }
}
