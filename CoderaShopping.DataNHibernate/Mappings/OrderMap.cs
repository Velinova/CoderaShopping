using CoderaShopping.Domain;
using FluentNHibernate.Mapping;

namespace CoderaShopping.DataNHibernate.Mappings
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Schema("dbo");
            Table("Orders");

            Id(x => x.Id);

            Map(x => x.Status)
                .Access.CamelCaseField(Prefix.Underscore)
                .CustomType<OrderStatus>()
                .Not.Nullable();
            Map(x => x.DateOrdered)
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();
            Map(x => x.OrderPrice)
                .Column("Price")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();
            References(x => x.User)
                .Column("UserId")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();
            //HasMany(x => x.Ratings)
            //    .Access.CamelCaseField(Prefix.Underscore)
            //    .Inverse()
            //    .Cascade.AllDeleteOrphan();
            HasMany(x => x.OrderItems)
                .Access.CamelCaseField(Prefix.Underscore)
                .Inverse()
                .Cascade.AllDeleteOrphan();
        }
    }
}
