using System;
using System.Windows.Controls;

namespace Example.Desktop.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);

        Page GetPage(string key);
    }
}
