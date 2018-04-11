using System;

namespace WVB.TestUIFramework.SeleniumCustomExceptions
{
    public class NotJavascriptConvertException : Exception
    {
        public NotJavascriptConvertException()
        {
        }

        public NotJavascriptConvertException(string mensagem) : base(mensagem)
        {
        }

        public NotJavascriptConvertException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {
        }
    }
}
