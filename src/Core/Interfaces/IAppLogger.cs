using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces;

/// <summary>
/// Logging is a fundamental standard for any application. This is a simple logger construct I'll use in my application
/// to demonstrate basic functionality. The various log levels must be defined by the business and development team to 
/// ensure consistent understanding.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAppLogger<T>
{
    void Information(string message, params object[] args);
    void Warning(string message, params object[] args);
    void Error(Exception exception, string message, params object[] args);
}
