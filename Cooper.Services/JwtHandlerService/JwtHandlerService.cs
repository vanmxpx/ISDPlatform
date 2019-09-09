using Cooper.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Text;

namespace Cooper.Services
{
    public class JwtHandlerService : IJwtHandlerService
    {
        private readonly ILogger logger;

        public JwtHandlerService(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets attrubute value from payload
        /// </summary>
        /// <param name="attribute">Attribute name</param>
        /// <param name="token">JWT token value</param>
        /// <returns>Returns value of payload attribute</returns>
        public string GetPayloadAttributeValue(string attribute, string token)
        {
            string value = null;

            try
            {
                string payload = GetPayload(token);

                JToken json = JObject.Parse(payload);

                value = (string)json.SelectToken(attribute);
            }
            catch (JsonReaderException ex)
            {
                logger.Error(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                logger.Error(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return value;
        }

        /// <summary>
        /// Gets payload json from Jwt token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Returns string value denoting payload json</returns>
        private string GetPayload(string token)
        {
            string payload_encoded = Normalize(token.Split('.')[1]);

            byte[] payload_byted = Convert.FromBase64String(payload_encoded);
            string payload = Encoding.ASCII.GetString(payload_byted);

            return payload;
        }

        /// <summary>
        /// Normilizes length of encoded into base64 string so it has a valid length (length % 4 == 0).
        /// </summary>
        /// <param name="encoded"></param>
        /// <returns></returns>
        private string Normalize(string encoded)
        {
            if (encoded.Length % 4 != 0)
            {
                encoded = encoded.PadRight(encoded.Length + (4 - encoded.Length % 4), '=');
            }

            return encoded;
        }
    }
}