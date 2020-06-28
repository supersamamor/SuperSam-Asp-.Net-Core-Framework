using ProjectNamePlaceHolder.ConsoleApp.ApiServices;
using ProjectNamePlaceHolder.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace ProjectNamePlaceHolder.ConsoleApp.Services
{
    public class ProcessService
    {
        private MainModulePlaceHolderAPIServices _mainModulePlaceHolderAPIServices { get; set; }
        public ProcessService(MyServiceConfig config)
        {
            _mainModulePlaceHolderAPIServices = new MainModulePlaceHolderAPIServices(new HttpClient()
            {
                BaseAddress = new Uri(config.SubComponentPlaceHolderAPI),
                Timeout = Timeout.InfiniteTimeSpan
            }
            ,config.SubComponentPlaceHolderApiKey, config.SubComponentPlaceHolderApiSecret);         
        }
        public IList<MainModulePlaceHolderModel> GetMainModulePlaceHolderList()
        {
            return _mainModulePlaceHolderAPIServices.GetMainModulePlaceHolderList();
        }
    }
}
