using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using CoderaShopping.Domain;
using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Internal;

namespace CoderaShopping.Business.Models
{
    public class TopManufacturersViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public class TopCustomersViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public class OrdersByStatusLastSixMonths
    {
        public Dictionary<int, List<int>> Dictionary { get; set; } 
    }
}
