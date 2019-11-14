using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization.Service
{
    /// <summary>
    /// Представляет тип для данных, возвращаемых от внешнего сервиса в формате json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class JsonDataResponse<T>
    {
        /// <summary>
        /// Возвращает или устанавливает данные возвращаемые от сервиса
        /// </summary>
        public T Data 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Возвращает или устанавливает возвращаемое сообщение
        /// </summary>
        public string Message
        {
            get;
            set;
        } = "";

        /// <summary>
        /// Возвращает или устанавливает статус ответа
        /// </summary>
        public bool Success
        {
            get;
            set;
        } = false;
    }
}
