using CoderaShopping.Domain;
using FluentNHibernate.Mapping;

namespace CoderaShopping.DataNHibernate.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Schema("dbo");
            Table("Categories");

            Id(x => x.Id)
                .Column("Id")
                .Access.CamelCaseField(Prefix.Underscore);

            Map(x => x.Name)
                .Column("Name")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            Map(x => x.Status)
                .Column("Status")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            Map(x => x.IsDefault)
                .Column("IsDefault")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            HasMany(x => x.Products)
                .KeyColumn("CategoryId")
                .Access.CamelCaseField(Prefix.Underscore)
                .Inverse()
                .Cascade.AllDeleteOrphan();

        }
    }
}
