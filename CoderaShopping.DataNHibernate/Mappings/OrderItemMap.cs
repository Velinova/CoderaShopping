using CoderaShopping.Domain;
using FluentNHibernate.Mapping;

namespace CoderaShopping.DataNHibernate.Mappings
{
    public class OrderItemMap : ClassMap<OrderItem>
    {
        public OrderItemMap()
        {
            Schema("dbo");
            Table("OrderItems");

            Id(x => x.Id);

            Map(x => x.Quantity)
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();
            References(x => x.Order)
               .Column("OrderId")
               .Access.CamelCaseField(Prefix.Underscore)
               .Not.Nullable();
            References(x => x.Product)
               .Column("ProductId")
               .Access.CamelCaseField(Prefix.Underscore)
               .Not.Nullable();
            
        }
    }
}
