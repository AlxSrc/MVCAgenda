using MVCAgenda.Core.Logging;
using MVCAgenda.Models.Logging;

namespace MVCAgenda.Factories.Logging
{
    public interface ILoggingFactory
    {
        LogListItemViewModel PrepereLogViewModel(Log log);
    }
}