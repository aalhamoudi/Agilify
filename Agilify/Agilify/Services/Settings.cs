using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Agilify.Services
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;
        public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(Provider);
        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(UserEmail);

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string UserIdKey = "userid";
        private static readonly string UserIdDefault = string.Empty;

        private const string UserEmailKey = "useremail";
        private static readonly string UserEmailDefault = string.Empty;

        private const string UserNameKey = "username";
        private static readonly string UserNameDefault = string.Empty;

        private const string UserImageKey = "userimage";
        private static readonly string UserImageDefault = string.Empty;

        private const string AuthIdKey = "authid";
        private static readonly string AuthIdDefault = string.Empty;

        private const string AuthTokenKey = "authtoken";
        private static readonly string AuthTokenDefault = string.Empty;

        private const string ProviderKey = "provider";
        private static readonly string ProviderDefault = string.Empty;

        #endregion


        public static string GeneralSettings
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault); }
            set { AppSettings.AddOrUpdateValue<string>(SettingsKey, value); }
        }

        public static string AuthToken
        {
            get { return AppSettings.GetValueOrDefault<string>(AuthTokenKey, AuthTokenDefault); }
            set { AppSettings.AddOrUpdateValue<string>(AuthTokenKey, value); }
        }
        public static string AuthId
        {
            get { return AppSettings.GetValueOrDefault<string>(AuthIdKey, AuthIdDefault); }
            set { AppSettings.AddOrUpdateValue<string>(AuthIdKey, value); }
        }

        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault<string>(UserIdKey, UserIdDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserIdKey, value); }
        }

        public static string UserEmail
        {
            get { return AppSettings.GetValueOrDefault<string>(UserEmailKey, UserEmailDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserEmailKey, value); }
        }

        public static string UserName
        {
            get { return AppSettings.GetValueOrDefault<string>(UserNameKey, UserNameDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserNameKey, value); }
        }
        public static string UserImage
        {
            get { return AppSettings.GetValueOrDefault<string>(UserImageKey, UserImageDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserImageKey, value); }
        }

        public static string Provider
        {
            get { return AppSettings.GetValueOrDefault<string>(ProviderKey, ProviderDefault); }
            set { AppSettings.AddOrUpdateValue<string>(ProviderKey, value); }
        }

        public static void Clear()
        {
            AuthToken = string.Empty;
            UserId = string.Empty;
            UserEmail = string.Empty;
            UserName = string.Empty;
            UserImage = string.Empty;
            Provider = string.Empty;

        }
    }
}