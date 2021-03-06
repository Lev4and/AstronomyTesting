using AstronomyTesting.Events;
using System;
using System.Net;

namespace AstronomyTesting.Response.Service
{
    public static class HttpStatusCodeMessageService
    {
        public static event Action<ShowHttpStatusCodeMessageEventArgs> OnShowHttpStatusCodeMessage;

        public static void GetErrorMessages(HttpStatusCode httpStatusCode)
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode.BadRequest:
                    {
                        OnShowHttpStatusCodeMessage(new ShowHttpStatusCodeMessageEventArgs("Ошибка 400. Причина: запрос не может быть понят сервером."));
                    }
                    break;
                case HttpStatusCode.Conflict:
                    {
                        OnShowHttpStatusCodeMessage(new ShowHttpStatusCodeMessageEventArgs("Ошибка 409. Причина: запрос не может быть выполнен из-за конфликта на сервере."));
                    }
                    break;
                case HttpStatusCode.NotFound:
                    {
                        OnShowHttpStatusCodeMessage(new ShowHttpStatusCodeMessageEventArgs("Ошибка 404. Причина: запрошенный ресурс не существует на сервере."));
                    }
                    break;
            }
        }
    }
}
