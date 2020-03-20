using CoderaShopping.Domain;
using FluentNHibernate.Mapping;

namespace CoderaShopping.DataNHibernate.Mappings
{
    public class ManifacturerMap : ClassMap<Manifacturer>
    {
        public ManifacturerMap()
        {
            Schema("dbo");
            Table("Manifacturers");

            Id(x => x.Id)
                .Column("Id")
                .Access.CamelCaseField(Prefix.Underscore);

            Map(x => x.Name)
                .Column("Name")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            Map(x => x.City)
                .Column("City")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            Map(x => x.Country)
                .Column("Country")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            Map(x => x.Address)
                .Column("Address")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            HasMany(x => x.Products)
                .KeyColumn("ManifacturerId")
                .Access.CamelCaseField(Prefix.Underscore)
                .Inverse()
                .Cascade.AllDeleteOrphan();

        }
    }
}
