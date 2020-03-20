namespace CoderaShopping.Business
{
    public static class ExceptionMessages
    {
        public static class CategoryException
        {
            public const string ALREADY_EXISTS = "The category already exists";
            public const string NOT_FOUND = "The category couldn't be found";
            public const string CHANGE_STATUS_ISDEFAULT = "Can't change to Default because this category isn't active";
            public const string DISABLED = "Category is disabled, choose another category";
            public const string REFERENCED_ITEMS = "There are products referenced to this category";
            public const string DISELECT_DEFAULT = "This is default category";

        }
        public static class ProductException
        {
            public const string ALREADY_EXISTS = "The product already exists";
            public const string NOT_FOUND = "The product couldn't be found";
        }
        public static class UserException
        {
            public const string NOT_FOUND = "The user couldn't be found";
            public const string EMAIL_TAKEN = "The email is taken by another account";
            public const string USERNAME_TAKEN = "The user name is taken by another account";
        }
        public static class OrderException
        {
            public const string NOT_FOUND = "The order couldn't be found";
        }
        public static class OrderItemException
        {
            public const string NOT_FOUND = "The order item couldn't be found";
            public const string OUT_OF_STOCK = "The order is rejected because one of the ordered products are out of stock";
        }
        public static class ManifacturerException
        {
            public const string ALREADY_EXISTS = "The manifacturer already exists";
            public const string NOT_FOUND = "The order manifacturer couldn't be found";
        }
    }
}