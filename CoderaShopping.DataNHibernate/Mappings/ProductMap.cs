using CoderaShopping.Domain;
using FluentNHibernate.Mapping;

namespace CoderaShopping.DataNHibernate.Mappings
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Schema("dbo");
            Table("Products");

            Id(x => x.Id);
            Map(x => x.Name)
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            Map(x => x.Description)
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            Map(x => x.Price)
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            Map(x => x.Quantity)
                .Access.CamelCaseField(Prefix.Underscore);

            References(x => x.Category)
                .Column("CategoryId")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            References(x => x.Manifacturer)
                .Column("ManifacturerId")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            HasMany(x => x.OrderItems)
                .Access.CamelCaseField(Prefix.Underscore)
                .Inverse()
                .Cascade.AllDeleteOrphan();

            //HasMany(x => x.Ratings)
            //   .Access.CamelCaseField(Prefix.Underscore)
            //   .Inverse()
            //   .Cascade.AllDeleteOrphan();
        }
    }
}
