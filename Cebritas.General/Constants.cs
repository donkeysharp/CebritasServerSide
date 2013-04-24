using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cebritas.General {
    public class Constants {
        public const string SMTP_SERVER = "SmtpServer";
        public const string SMTP_PORT = "SmtpPort";
        public const string SMTP_SENDER = "SmtpSender";
        public const string SMTP_PASSWORD = "SmtpPassword";

        public const string ALERTA_TIPO_TRAFICO = "trafico";
        public const string ALERTA_TIPO_MANIFESTACION = "manifestacion";
        public const string ALERTA_TIPO_DESFILE = "desfile";
        public const string ALERTA_TIPO_BLOQUEO = "bloqueo";
        public const string ALERTA_TIPO_OTRO = "otro";

        public const string ALERTA_TIEMPO_15M = "15m";
        public const string ALERTA_TIEMPO_30M = "30m";
        public const string ALERTA_TIEMPO_1H = "1h";
        public const string ALERTA_TIEMPO_2H = "2h";
        public const string ALERTA_TIEMPO_3HH = "3hh";

        // In order to get alerts, those will be in a 10 kilomenters radius
        public const double ALERTA_KILOMETER_NEAR_RADIUS = 10.0;
        // In order to report an alert, other reported alerts should be in a 300 meters radius
        public const double ALERTA_METERS_REPORT_RADIUS = 100;
    }

    public class Messages {
        public const string OK = "ok";
        public const string UNDEFINED_ERROR = "undefined_error";
        public const string THERE_WAS_A_PROBLEMO_JEFE = "there_was_a_problemo_jefe";

        public const string USER_ALREADY_EXISTS = "user_already_exists";
        public const string USER_DOES_NOT_EXIST = "user_does_not_exist";
        public const string SOME_FIELDS_ARE_NOT_IN_VALID_FORMAT = "some_fields_are_not_in_valid_format";

        public const string ROLE_ALREADY_EXISTS = "role_already_exists";
        public const string ROLE_DOES_NOT_EXIST = "role_does_not_exist";

        public const string ACCESS_TOKEN_DOES_NOT_EXIST = "access_token_does_not_exist";
        public const string ACCESS_TOKEN_ALREADY_EXISTS = "access_token_already_exists";

        public const string PUBLIC_KEY_MUST_BE_ESTABLISHED = "public_key_must_be_established";
        public const string PRIVATE_KEY_MUST_BE_ESTABLISHED = "private_key_must_be_established";
        public const string ERROR_ENCRYPTING_TEXT = "error_encrypting_text";
        public const string ERROR_DECRYPTING_TEXT = "error_decrypting_text";
        public const string ERROR_PARSING_DATA = "error_parsing_data";

        public const string USER_CREDENTIALS_ARE_INVALID = "user_credentials_are_invalid";
        public const string SUCCESSFULLY_LOGGED_OUT = "successfully_logged_out";
        public const string INVALID_SESSION = "invalid_session";

        public const string ALERTA_TIPO_INVALIDO = "alerta_tipo_invalido";
        public const string ALERTA_TIEMPO_ESTIMADO_INVALIDO = "alerta_tiempo_estimado_invalido";
        public const string ALERTA_FORMATO_COORDENADAS_INCORRECTO = "alerta_formato_coordenadas_incorrecto";

        public const string PRECIOS_INVALID_PARAMETER_FORMAT = "precios_invalid_parameter_format";

        public const string CATEGORY_ALREADY_EXISTS = "category_already_exists";
        public const string CATEGORY_NAMES_ARE_EMPTY = "category_names_are_empty";
    }
}