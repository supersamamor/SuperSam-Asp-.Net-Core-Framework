using ProjectNamePlaceHolder.Application;
using ProjectNamePlaceHolder.Web.Extensions;
using ProjectNamePlaceHolder.Web.Models;
using System.Collections.Generic;

namespace ProjectNamePlaceHolder.Web.Pages.MainModulePlaceHolder
{
    public static class MainModulePlaceHolderHtmlElementVariables
    {     
        public const string MainModulePlaceHolderForm = "formMainModulePlaceHolderForm";
        public const string MainModulePlaceHolderFormPromptContainer = "promptMainModulePlaceHolderForm";
        public const string MainModulePlaceHolderListingsPromptContainer = "promptMainModulePlaceHolderListings";
        
        public static readonly FormModal MainModulePlaceHolderModal = new FormModal("MainModulePlaceHolderModal", 700);
        public static readonly PageHandler ShowCreate = new PageHandler("ShowCreate", Resource.LabelAddMainModulePlaceHolder);
        public static readonly PageHandler ShowEdit = new PageHandler("ShowEdit", Resource.LabelEditMainModulePlaceHolder, new List<string> { "id" });
        public static readonly PageHandler ShowView = new PageHandler("ShowView", Resource.LabelDetailsMainModulePlaceHolder, new List<string> { "id" });
        public static readonly PageHandler ShowDelete = new PageHandler("ShowDelete", Resource.LabelDeleteMainModulePlaceHolder, new List<string> { "id" });
        public static readonly PageHandler Save = new PageHandler(name: "Save", withPromptConfirmation: true);
        public static readonly PageHandler Update = new PageHandler(name: "Update", withPromptConfirmation: true);
        public static readonly PageHandler Delete = new PageHandler(name: "Delete", withPromptConfirmation: true);
    }
}

