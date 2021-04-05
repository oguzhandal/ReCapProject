using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string Added = "Ürün başarılı bir şekilde eklendi.";
        public static string CarNameInvalid = "Ürün ismi geçersiz";
        public static string Deleted = "Ürün başarıyla silindi";
        public static string Updated="Ürün başarıyla güncellendi";
        public static string Listed = "Ürünler Listelendi";
        public static string SortByBrandId = "Marka Id'sine göre listelendi";
        public static string SortByColorId = "Renklere göre listelendi";
        public static string CarDetails = "Araç detayları listelendi";

        public static string AddedCarImage = "Araba için yüklenilen resim başarıyla eklendi.";
        public static string DeletedCarImage = "Arabanın resmi başarıyla silindi.";
        public static string UpdatedCarImage = "Araba için yüklenilen resim başarıyla güncellendi.";
        public static string FailedCarImageAdd = "Bir araba 5'ten fazla resme sahip olamaz.";

        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered = "Kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Parola hatası";
        public static string UserAlreadyExists = "Kullanıcı mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";
        internal static string SuccessfulLogin = "Başarılı giriş";
    }
}
