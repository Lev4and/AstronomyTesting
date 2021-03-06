using System;

namespace AstronomyTesting.Response.Service
{
    public static class ConfigRestClients
    {
        public static string Protocol { get; set; }

        public static string Domain { get; set; }

        public static int? Port { get; set; }

        public static string GetAddressServer()
        {
            return $"{Protocol}://{Domain}{(Port != null ? $":{Port}" : "")}/api/";
        }

        public static string GetAddressServer(string controllerName)
        {
            if(controllerName != null ? controllerName.Length == 0 : true)
            {
                throw new ArgumentNullException("controllerName", "Название контроллера не может быть пустым или длиной 0 символов.");
            }

            return $"{Protocol}://{Domain}{(Port != null ? $":{Port}" : "")}/api/{controllerName}/";
        }
    }
}
