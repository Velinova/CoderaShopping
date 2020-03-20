using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoderaShopping.Domain
{
    public enum RatingObjectType
    {
        ORDER,
        PRODUCT
    }
    public class Rating
    {
        private readonly Guid _id = default;
        private User _user;
        private Guid _objectId;
        private RatingObjectType _objectType;
        private int _value;
        private string _comment;
        private bool _showName;

        #region Constructors
        public Rating()
        {

        }
        public Rating(User user, Guid objectId, RatingObjectType objectType, int value, string comment, bool showName)
        {
            _user = user;
            _objectId = objectId;
            _objectType = objectType;
            _value = value;
            _comment = comment;
            _showName = showName;

        }
        public Rating(Guid  id, User user, Guid objectId, RatingObjectType objectType, int value, string comment, bool showName)
        {
            _id = id;
            _user = user;
            _objectId = objectId;
            _objectType = objectType;
            _value = value;
            _comment = comment;
            _showName = showName;

        }

        #endregion

            #region Properties
        public virtual Guid  Id
        {
            get { return _id; }
        }
        public virtual User User
        {
            get { return _user; }
            set { _user = value; }
        }
        public virtual Guid ObjectId
        {
            get { return _objectId; }
            set { _objectId = value; }
        }
        public virtual RatingObjectType ObjectType
        {
            get { return _objectType; }
            set { _objectType = value; }
        }
        public virtual int Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        public virtual bool ShowName
        {
            get { return _showName; }
            set { _showName = value; }
        }
        #endregion
    }
}
