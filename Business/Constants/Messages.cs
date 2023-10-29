using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string AddedBrand = "Marka eklendi";
        public static string AddedCar = "Araç eklendi";
        public static string AddedColor = "Renk eklendi";
        public static string AddedUser = "Kullanıcı eklendi";
        public static string AddedCustomer = "Müşteri eklendi";
        public static string AddedRental = "Araç başarıyla kiralandı";
        public static string CarNameOrPriceError = "Aracın adı minumum 2 karakter ve günlük ücreti 0TL'den büyük olmalıdır";
        public static string AddedCustomerErrorNotFoundUser = "Müşteri eklemek için ilgili kullanıcı bulunamadı";
        public static string AddedRentalErrorRentedCar = "Kiralanmak istenen araç henüz teslim edilmediği için tekrardan kiralanamaz";
        public static string NoDataOnList = "Listede veri bulunamadı";
        public static string NoDataOnFilter = "Filtreye göre veri bulunamadı";
    }
}
