using System.Net.NetworkInformation;
using Microsoft.Data.Sqlite;

namespace Financical_Traker.Core.RateService
{
	public static class RateServiece
	{
        static readonly string url_byn = "https://storage.yandexcloud.net/change-rates/byn_current_exchange_rate.db";
        static readonly string url_usd = "https://storage.yandexcloud.net/change-rates/usd_current_exchange_rate.db";
        static readonly HttpClient client = new();

		public static async Task<decimal> ConvertAsync(decimal value, string currency1, string currency2)
		{
            if (currency1 == "BYN")
            {
                return await ConvertForBynAsync(value, currency2);
            }
			if (currency2 == "BYN")
			{
                return await ConvertToBynAsync(value, currency1);
			}
            else
            {
                return await ConvertDollarBasedAsync(value, currency1, currency2);
            }
		}

		public static async Task<decimal> ConvertToBynAsync(decimal value, string currency)
		{
            string fileName = "byn_current_exchange_rate.db";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);

            if (IsInternetConnection())
            {
                byte[] fileBytes = await client.GetByteArrayAsync(url_byn);

                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Data");
                }

                File.WriteAllBytes(filePath, fileBytes);
            }

            using var connection = new SqliteConnection($"Data Source={filePath}");
            await connection.OpenAsync();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT rate FROM byn_current_exchange_rate WHERE currency = @param";
            command.Parameters.AddWithValue("@param", currency);

            using SqliteDataReader reader = command.ExecuteReader();
            reader.Read();
            decimal rate = reader.GetDecimal(0);

            await connection.CloseAsync();

            return value / rate;
        }

        public static async Task<decimal> ConvertForBynAsync(decimal value, string currency)
        {
            string fileName = "byn_current_exchange_rate.db";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);

            if (IsInternetConnection())
            {
                byte[] fileBytes = await client.GetByteArrayAsync(url_byn);

                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Data");
                }

                File.WriteAllBytes(filePath, fileBytes);
            }

            using var connection = new SqliteConnection($"Data Source={filePath}");
            await connection.OpenAsync();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT rate FROM byn_current_exchange_rate WHERE currency = @param";
            command.Parameters.AddWithValue("@param", currency);

            using SqliteDataReader reader = command.ExecuteReader();
            reader.Read();
            decimal rate = reader.GetDecimal(0);

            await connection.CloseAsync();

            return value * rate;
        }

        public static async Task<decimal> ConvertDollarBasedAsync(decimal value, string currency1, string currency2)
        {
            string fileName = "usd_current_exchange_rate.db";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);

            decimal rate1;
            decimal rate2;

            if (IsInternetConnection())
            {
                byte[] fileBytes = await client.GetByteArrayAsync(url_usd);

                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Data");
                }

                File.WriteAllBytes(filePath, fileBytes);
            }

            using var connection = new SqliteConnection($"Data Source={filePath}");
            await connection.OpenAsync();

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT rate FROM usd_current_exchange_rate WHERE currency = @param";
                command.Parameters.AddWithValue("@param", currency1);
                using SqliteDataReader reader = command.ExecuteReader();
                reader.Read();
                rate1 = reader.GetDecimal(0);
            }

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT rate FROM usd_current_exchange_rate WHERE currency = @param";
                command.Parameters.AddWithValue("@param", currency2);
                using SqliteDataReader reader = command.ExecuteReader();
                reader.Read();
                rate2 = reader.GetDecimal(0);
            }
            await connection.CloseAsync();

            return value / rate1 * rate2;
        }

        public static bool IsInternetConnection()
        {
            try
            {
                using var ping = new Ping();
                var reply = ping.Send("8.8.8.8", 2000);
                return (reply != null && reply.Status == IPStatus.Success);
            }
            catch
            {
                return false;
            }
        }
	}
}

