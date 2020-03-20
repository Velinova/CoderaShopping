using CoderaShopping.Domain;
using FluentNHibernate.Mapping;

namespace CoderaShopping.DataNHibernate.Mappings
{
    public class RatingMap : ClassMap<Rating>
    {
        public RatingMap()
        {
            Schema("dbo");
            Table("Ratings");

            Id(x => x.Id);
            
            Map(x => x.ObjectType)
                .Access.CamelCaseField(Prefix.Underscore)
                .CustomType<RatingObjectType>()
                .Not.Nullable();

            Map(x => x.Value)
                .Column("[Value]")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            Map(x => x.Comment)
                .Access.CamelCaseField(Prefix.Underscore);

            Map(x => x.ObjectId)
                .Column("ObjectId")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            Map(x => x.ShowName)
                .Column("ShowName")
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable();

            References(x => x.User)
                .Column("UserId")
                .Nullable()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
