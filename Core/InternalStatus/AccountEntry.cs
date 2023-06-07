using System;
using Npgsql;
using Financical_Traker.Core.AccountManagement;
using UserDataSerialization;

namespace Financical_Traker.Core.InternalStatus
{
	public static class AccountEntry
	{
        static string host = "rc1b-hw21s2b87mkoieos.mdb.yandexcloud.net";
        static string port = "6432";
        static string db = "financical-traker-db";
        static string username = "adm";
        static string password = "254677FtDb";
        static string connString = $"Host={host};Port={port};Database={db};Username={username};Password={password};Ssl Mode=Require;TrustServerCertificate=true;";

        public static async Task<bool> isEntered(string mail, string password)
        {
            bool accountExists;
            string name = "";
            string defaultCurrency = "";
            string timezone = "";

            await using (var connection = new NpgsqlConnection(connString))
            {
                await connection.OpenAsync();

                string sql = "SELECT COUNT(*), name, default_currency, timezone FROM users WHERE email = @email AND password = @password GROUP BY name, default_currency, timezone";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("email", mail);
                    command.Parameters.AddWithValue("password", password);

                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            accountExists = reader.GetInt32(0) > 0;
                            if (accountExists)
                            {
                                name = reader.GetString(1);
                                defaultCurrency = reader.GetString(2);
                                timezone = reader.GetString(3);
                            }
                        }
                        else
                        {
                            accountExists = false;
                        }
                    }
                }
            }

            if (accountExists)
            {
                CurrentUserData.CurrentUser = new User(name, password, mail, defaultCurrency, timezone);

                var userData = new DataSerializer<User>("/Users/egorgajkevic/Documents/FPDebug/userData.json");
                userData.Serialize(CurrentUserData.CurrentUser);
            }

            return accountExists;
        }

        public static async void RegisterAccountAsync(string mail, string password)
        {
            await using var connection = new NpgsqlConnection(connString);
            await connection.OpenAsync();

            string sql = "INSERT INTO users (email, name, password, default_currency, timezone) VALUES (@email, '', @password, NULL, NULL)";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("email", mail);
                command.Parameters.AddWithValue("password", password);

                command.ExecuteNonQuery();
            }

            CurrentUserData.CurrentUser = new User(mail, password);
            //var userData = new DataSerializer<User>("userData.json");
            var userData = new DataSerializer<User>("/Users/egorgajkevic/Documents/FPDebug/userData.json");
            userData.Serialize(CurrentUserData.CurrentUser);
        }

        public static async 
        Task
ContinueAccountRegisterAsync(string mail, string name, string default_currency, string timezone)
        {
            await using var connection = new NpgsqlConnection(connString);
            await connection.OpenAsync();

            string updateSql = "UPDATE users SET name = @newName, default_currency = @newCurrency, timezone = @newTimezone WHERE email = @email";

            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, connection))
            {
                updateCommand.Parameters.AddWithValue("newName", name);
                updateCommand.Parameters.AddWithValue("newCurrency", default_currency);
                updateCommand.Parameters.AddWithValue("newTimezone", timezone);
                updateCommand.Parameters.AddWithValue("email", mail);

                updateCommand.ExecuteNonQuery();
            }

            CurrentUserData.CurrentUser.Name = name;
            CurrentUserData.CurrentUser.DefaultCurrency = default_currency;
            CurrentUserData.CurrentUser.UTC_value = timezone;

            //var userData = new DataSerializer<User>("userData.json");
            var userData = new DataSerializer<User>("/Users/egorgajkevic/Documents/FPDebug/userData.json");
            userData.Serialize(CurrentUserData.CurrentUser);
        }

        public static async Task<bool> AccountCheckAsync(string mail)
        {
            await using var connection = new NpgsqlConnection(connString);
            await connection.OpenAsync();

            string selectSql = "SELECT COUNT(*) FROM users WHERE email = @email";

            using (NpgsqlCommand selectCommand = new NpgsqlCommand(selectSql, connection))
            {
                selectCommand.Parameters.AddWithValue("email", mail);

                int count = Convert.ToInt32(selectCommand.ExecuteScalar());

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void UserExit()
        {
            //var userData = new DataSerializer<User>("userData.json");
            var userData = new DataSerializer<User>("/Users/egorgajkevic/Documents/FPDebug/userData.json");
            userData.DeleteFile();
        }
    }
}
