using System.Collections.Generic;


namespace ProyectoVentas.Helpers
{
    public static class DateNowJuarez
    {
        public static DateOnly HoyJuarez()
        {
            var zone = TimeZoneInfo.FindSystemTimeZoneById("America/Ciudad_Juarez");
            var juarez = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);
            return DateOnly.FromDateTime(juarez);
        }
    }
}









// namespace ProyectoVentas.Helpers
// {
//     public static class DateNowJuarez
//     {
//         // =========================
//         // FECHA ACTUAL DE CIUDAD JUAREZ
//         // =========================


//                     public static DateOnly ToJuarezDate(DateTime date)
//             {
//                 var zone = TimeZoneInfo.FindSystemTimeZoneById("America/Ciudad_Juarez");
//                 var juarez = TimeZoneInfo.ConvertTimeFromUtc(date.ToUniversalTime(), zone);
//                 return DateOnly.FromDateTime(juarez);
//             }


//             private static DateOnly HoyJuarez()
//             {
//                 var zone = TimeZoneInfo.FindSystemTimeZoneById("America/Ciudad_Juarez");
//                 var juarez = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);
//                 return DateOnly.FromDateTime(juarez);
//             }
      
//     }


    
// }



