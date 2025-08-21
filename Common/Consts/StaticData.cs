using backend_se.Data.Models;

namespace backend_se.Common.Consts
{
    public static class StaticData
    {
        public static List<UserModel> Users { get; set; } = new List<UserModel> {
                                                                new() { Id = 1, Name = "Name1", Email = "name1@gmail.com", Username = "username1", Password = "password1", Role = (short)eUserRole.Basic, RegistrationDate = new DateTime(2023, 7, 15, 7, 16, 35), CityId = 1 },
                                                                new() { Id = 2, Name = "Name2", Email = "name2@gmail.com", Username = "username2", Password = "password2", Role = (short)eUserRole.Admin, RegistrationDate = new DateTime(2023, 9, 11, 8, 16, 35) },
                                                                new() { Id = 3, Name = "Name3", Email = "name3@gmail.com", Username = "username3", Password = "password3", Role = (short)eUserRole.Basic, RegistrationDate = new DateTime(2023, 7, 15, 7, 16, 35), CityId = 2 },
                                                                new() { Id = 4, Name = "Name4", Email = "name4@gmail.com", Username = "username4", Password = "password4", Role = (short)eUserRole.Basic, RegistrationDate = new DateTime(2023, 7, 15, 7, 16, 35), CityId = 4 },
                                                                new() { Id = 5, Name = "Name5", Email = "name5@gmail.com", Username = "username5", Password = "password5", Role = (short)eUserRole.Admin, RegistrationDate = new DateTime(2023, 7, 15, 7, 16, 35) }};

        public static List<RefreshTokenModel> RefreshTokens { get; set; } = new List<RefreshTokenModel>();
        public static List<ChatHistoryModel> ChatHistory { get; set; } = new List<ChatHistoryModel>
        {
            new() { Id = 1, Message = "message1", SenderId = 2, ReceiverId = 1, SentTime = new DateTime(2025, 7, 18, 4, 28, 26) },
            new() { Id = 2, Message = "message2", SenderId = 3, ReceiverId = 1, SentTime = new DateTime(2025, 7, 17, 7, 16, 35) },
            new() { Id = 3, Message = "message3", SenderId = 4, ReceiverId = 1, SentTime = new DateTime(2025, 7, 16, 9, 10, 7) },
            new() { Id = 4, Message = "message4", SenderId = 3, ReceiverId = 2, SentTime = new DateTime(2025, 7, 15, 7, 16, 35) }
        };
        public static List<CityModel> Cities { get; set; } = new List<CityModel>
        {
            new() { Id = 1, Name = "Leskovac"},
            new() { Id = 2, Name = "Nis"},
            new() { Id = 3, Name = "Beograd"},
            new() { Id = 4, Name = "Novi Sad"},
        };
        public static List<CurrencyModel> Currencies { get; set; } = new List<CurrencyModel>
        {
            new() {Id = 1, Name = "Euro", DisplayName = "eur"},
            new() {Id = 2, Name = "Serbian Dinar", DisplayName = "rsd"}
        };
        public static List<ProductModel> Products { get; set; } = new List<ProductModel>
        {
            new() { Id = 1, Displayed = true, Name = "iPhone 16 Pro", Description = "iPhone 16 Pro, new", UserId = 1, Condition = (short)eProductCondition.New, Created = new DateTime(2025, 7, 15, 7, 16, 35), Price = 1080.12M, CurrencyId = 1},
            new() { Id = 2, Displayed = true, Name = "Green Hoodie", Description = "Green hoodie, used", UserId = 1, Condition = (short)eProductCondition.Used, Created = new DateTime(2024, 7, 15, 7, 16, 35), Price = 125.00M, CurrencyId = 1},
            new() { Id = 3, Displayed = true, Name = "Samsung Z Flip 6", Description = "Samsung Z Flip 6, new", UserId = 3, Condition = (short)eProductCondition.New, Created = new DateTime(2025, 7, 15, 7, 16, 35), Price = 1500.99M, CurrencyId = 1},
            new() { Id = 4, Displayed = true, Name = "iPhone 7", Description = "Phone is broken, sold for parts", UserId = 4, Condition = (short)eProductCondition.Broken, Created = new DateTime(2025, 7, 15, 7, 16, 35), Price = 25.00M, CurrencyId = 1},
            new() { Id = 5, Displayed = true, Name = "Blue Shirt", Description = "Blue Shirt, new", UserId = 1, Condition = (short)eProductCondition.New, Created = new DateTime(2025, 7, 15, 7, 16, 35), Price = 25.00M, CurrencyId = 1},
        };
        public static List<SpecificationModel> Specifications { get; set; } = new List<SpecificationModel>{
            new(){ Id = 1, Name = "Screen size", IsBool = false },
            new(){ Id = 2, Name = "Color", IsBool = false },
            new(){ Id = 3, Name = "Foldable", IsBool = true },
            new(){ Id = 4, Name = "Size", IsBool = false },
        };
        public static List<CategoryModel> Categories { get; set; } = new List<CategoryModel>
        {
            new(){ Id = 1, Name = "Phone", Description = "Mobile phone"},
            new(){ Id = 2, Name = "Tablet", Description = "Tablet"},
            new(){ Id = 3, Name = "Display", Description = "Display"},
            new(){ Id = 4, Name = "Shirt", Description = "Shirt"},
            new(){ Id = 5, Name = "Hoodie", Description = "Hoodie"},
            new(){ Id = 6, Name = "Jacket", Description = "Jacket"},
        };
        public static List<SpecificationCategoryModel> SpecificationCategories { get; set; } = new List<SpecificationCategoryModel>
        {
            new(){ Id = 1, SpecificationId = 1, CategoryId =  1},
            new(){ Id = 2, SpecificationId = 1, CategoryId =  2},
            new(){ Id = 3, SpecificationId = 1, CategoryId =  3},
            new(){ Id = 4, SpecificationId = 2, CategoryId =  1},
            new(){ Id = 5, SpecificationId = 2, CategoryId =  2},
            new(){ Id = 6, SpecificationId = 2, CategoryId =  4},
            new(){ Id = 7, SpecificationId = 2, CategoryId =  5},
            new(){ Id = 8, SpecificationId = 2, CategoryId =  6},
            new(){ Id = 9, SpecificationId = 3, CategoryId =  1},
            new(){ Id = 10, SpecificationId = 3, CategoryId =  2},
            new(){ Id = 11, SpecificationId = 4, CategoryId =  4},
            new(){ Id = 12, SpecificationId = 4, CategoryId =  5},
            new(){ Id = 13, SpecificationId = 4, CategoryId =  6},
        };
        public static List<ProductSpecificationModel> ProductSpecifications { get; set; } = new List<ProductSpecificationModel>
        {
            new(){ Id = 1, ProductId = 1, SpecificationId = 1, Value = "6.3\""},
            new(){ Id = 2, ProductId = 1, SpecificationId = 2, Value = "Green"},
            new(){ Id = 3, ProductId = 2, SpecificationId = 2, Value = "Green"},
            new(){ Id = 4, ProductId = 3, SpecificationId = 1, Value = "6.7\""},
            new(){ Id = 5, ProductId = 3, SpecificationId = 2, Value = "Black"},
            new(){ Id = 6, ProductId = 3, SpecificationId = 3, Value = "", BoolValue = true},
            new(){ Id = 7, ProductId = 4, SpecificationId = 2, Value = "Red"},
            new(){ Id = 8, ProductId = 4, SpecificationId = 1, Value = "4.7\""},
            new(){ Id = 9, ProductId = 5, SpecificationId = 2, Value = "Blue"},
            new(){ Id = 10, ProductId = 5, SpecificationId = 4, Value = "S"},
            new(){ Id = 11, ProductId = 2, SpecificationId = 4, Value = "M"},
        };
        public static List<ProductCategoryModel> ProductCategories { get; set; } = new List<ProductCategoryModel>
        {
            new(){Id = 1, ProductId = 1, CategoryId = 1},
            new(){Id = 2, ProductId = 2, CategoryId = 5},
            new(){Id = 3, ProductId = 3, CategoryId = 1},
            new(){Id = 4, ProductId = 3, CategoryId = 2},
            new(){Id = 5, ProductId = 4, CategoryId = 1},
            new(){Id = 6, ProductId = 5, CategoryId = 4},
        };
    }
}
