using System;
using System.Diagnostics;

namespace ProjectManagement.Models
{
    public static class General
    {
        public static Response<T> GenerateResponse<T>(bool success, string message, T data)
        {
            return new Response<T> { Success = success, Message = message, Data = data };
        }

        public static string getCurrentNamespaceAndMethod()
        {
            StackTrace stack = new StackTrace();
            var method = stack.GetFrame(1).GetMethod();
            var methodName = method.Name;
            var methodNamespace = method.DeclaringType.FullName;
            return String.Format("{0}.{1}", methodNamespace, methodName);
        }
    }
}
